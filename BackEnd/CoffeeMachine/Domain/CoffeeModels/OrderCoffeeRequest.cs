using CoffeeMachine.Domain.MoneyModels;

namespace CoffeeMachine.Domain.Models
{
  public class OrderCoffeeRequest
  {
    public Dictionary<string, int> Order { get; set; }

    public Payment Payment { get; set; } = new();
  }
}
