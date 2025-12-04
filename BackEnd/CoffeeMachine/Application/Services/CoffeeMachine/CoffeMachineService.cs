using CoffeeMachine.Domain.CoffeeModels;
using CoffeeMachine.Domain.Models;
using CoffeeMachine.Domain.MoneyModels;

namespace CoffeeMachine.Application.Services.CoffeeMachine
{
  public class CoffeMachineService : ICoffeMachineService
  {
    public List<CoffeeStockData> GetCoffees()
    {
      throw new NotImplementedException();
    }

    public List<CoffeePriceData> GetCoffeesPrices()
    {
      throw new NotImplementedException();
    }

    public MoneyChangeData BuyCoffee(OrderCoffeeRequest request)
    {
      throw new NotImplementedException();
    }
  }
}
