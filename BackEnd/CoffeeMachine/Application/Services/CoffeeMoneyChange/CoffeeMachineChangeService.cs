using CoffeeMachine.Domain.MoneyModels;
using CoffeeMachine.Infrastructure.MoneyRepository;

namespace CoffeeMachine.Application.Services.CoffeeMoneyChange
{
  public class CoffeeMachineChangeService : ICoffeMachineChangeService
  {
    private readonly CoffeeMoneyRepository _CoffeeMoneyRepo;

    public CoffeeMachineChangeService(CoffeeMoneyRepository coffeeMoneyRepo)
    {
      this._CoffeeMoneyRepo = coffeeMoneyRepo;
    }

    public List<MoneyStockData> GetMoneyChangeStock()
    {
      Dictionary<int, int> moneyChangeStockDictionary = this._CoffeeMoneyRepo.GetChangeStock();

      List<MoneyStockData> moneyChangeStock = new List<MoneyStockData>();

      // Convertir el diccionario en su clase correspondiente
      foreach (var money in moneyChangeStockDictionary)
      {
        moneyChangeStock.Add(new MoneyStockData
        {
          CoinValue = money.Key,
          CoinStock = money.Value
        });
      }

      return moneyChangeStock;
    }
  }
}
