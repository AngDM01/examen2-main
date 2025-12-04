using CoffeeMachine.Domain.MoneyModels;

namespace CoffeeMachine.Application.Commands
{
  public interface IBuyCoffeeCommand
  {
    MoneyChangeData Execute();
  }
}
