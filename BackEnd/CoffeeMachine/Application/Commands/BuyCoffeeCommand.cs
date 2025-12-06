using CoffeeMachine.Domain.CoffeeModels;
using CoffeeMachine.Domain.Models;
using CoffeeMachine.Domain.MoneyModels;
using CoffeeMachine.Infrastructure.CoffeeRepository;
using CoffeeMachine.Infrastructure.MoneyRepository;

namespace CoffeeMachine.Application.Commands
{
  public class BuyCoffeeCommand : IBuyCoffeeCommand
  {
    private readonly CoffeeMachineRepository _CoffeeMachineRepo;
    private readonly CoffeeMoneyRepository _CoffeeMoneyRepository;

    public BuyCoffeeCommand(CoffeeMachineRepository coffeeMachineRepo, CoffeeMoneyRepository coffeeMoneyRepository)
    {
      _CoffeeMachineRepo = coffeeMachineRepo;
      _CoffeeMoneyRepository =coffeeMoneyRepository;
    }

    public MoneyChangeData Execute(List<CoffeeOrderItem> coffeesRequest, Payment payment)
    {
      int totalCost = coffeesRequest.Sum(c => c.Price * c.Amount);

      int change = payment.TotalAmount - totalCost;

      if (change < 0)
        throw new ArgumentException("Cambio negativo, operación no válida");

      try
      {
        MoneyChangeData moneyChangeData = new MoneyChangeData();

        moneyChangeData.TotalChange = change;

        var coinsStock = _CoffeeMoneyRepository.GetChangeStock();

        foreach (var coin in coinsStock.Keys.OrderByDescending(c => c))
        {
          var count = Math.Min(change / coin, coinsStock[coin]);
          if (count > 0)
          {
            moneyChangeData.ChangeBreakdown.Add(new MoneyStockData
            {
              CoinValue = coin,
              CoinStock = count,
            });

            change -= coin * count;
          }
        }


        if (change > 0)
        {
          throw new Exception("No hay suficiente cambio en la máquina.");
        }

        setNewMoneyChangeStock(moneyChangeData);

        setNewCoffeeStock(coffeesRequest);

        return moneyChangeData;
      }
      catch (Exception ex)
      {
        throw new Exception("Algo salió mal al realizar la compra. " + ex.Message);
      }
    }

    private void setNewMoneyChangeStock(MoneyChangeData moneyChange)
    {
      foreach (var coin in moneyChange.ChangeBreakdown)
      {
        this._CoffeeMoneyRepository.UpdateMoneyChangeStock(coin.CoinValue, coin.CoinStock);
      }
    }

    private void setNewCoffeeStock(List<CoffeeOrderItem> coffeesRequest)
    {
      foreach (var coffee in coffeesRequest)
      {
        this._CoffeeMachineRepo.UpdateCoffeeStock(coffee.CoffeeName, coffee.Amount);
      }
    }
  }
}
