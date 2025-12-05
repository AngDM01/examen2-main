using CoffeeMachine.Domain.CoffeeModels;
using CoffeeMachine.Domain.Models;
using CoffeeMachine.Domain.MoneyModels;

namespace CoffeeMachine.Application.Commands
{
  public interface IBuyCoffeeCommand
  {
    MoneyChangeData Execute(List<CoffeeOrderItem> coffeesRequest, Payment payment);
  }
}
