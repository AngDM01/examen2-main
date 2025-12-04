using CoffeeMachine.Domain.MoneyModels;

namespace CoffeeMachine.Application.Services.CoffeeMoneyChange
{
  public interface ICoffeMachineChangeService
  {
    List<MoneyStockData> GetMoneyChangeStock();
  }
}
