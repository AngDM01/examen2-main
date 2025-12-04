using CoffeeMachine.Controllers;

namespace CoffeeMachine.Models
{
  public class OrderCoffeeRequest
  {
    public Dictionary<string, int> Order { get; set; }

    public Payment Payment { get; set; } = new();
  }
}
