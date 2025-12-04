using CoffeeMachine.Domain.MoneyModels;
using CoffeeMachine.Infrastructure.CoffeeRepository;

namespace CoffeeMachine.Application.Commands
{
  public class BuyCoffeeCommand : IBuyCoffeeCommand
  {
    private readonly CoffeeMachineRepository _coffeeMachineRepo;

    public BuyCoffeeCommand(CoffeeMachineRepository coffeeMachineRepo)
    {
      _coffeeMachineRepo = coffeeMachineRepo;
    }

    public MoneyChangeData Execute()
    {
      return new MoneyChangeData
      {
        TotalChange = 0,
        ChangeBreakdown = new List<MoneyStockData>()
      };
    }
  }
}
