using CoffeeMachine.Domain.CoffeeModels;
using CoffeeMachine.Domain.Models;
using CoffeeMachine.Domain.MoneyModels;

namespace CoffeeMachine.Application.Services.CoffeeMachine
{
  public interface ICoffeMachineService
  {
    List<CoffeeStockData> GetCoffees();

    List<CoffeePriceData> GetCoffeesPrices();

    MoneyChangeData BuyCoffee(OrderCoffeeRequest request);
  }
}
