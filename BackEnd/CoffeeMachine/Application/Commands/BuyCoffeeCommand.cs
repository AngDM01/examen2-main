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

      try
      {
        MoneyChangeData moneyChangeData = new MoneyChangeData();

        moneyChangeData.TotalChange = change;

        var coinsStock = _CoffeeMoneyRepository.GetChangeStock();

        // Falta ver bien esta lógica
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
          return StatusCode(500, "No hay suficiente cambio en la máquina.");
        }

        return Ok(result);
      }
      catch (Exception ex)
      {

      }
      return new MoneyChangeData
      {
        TotalChange = 0,
        ChangeBreakdown = new List<MoneyStockData>()
      };
    }
  }
}
