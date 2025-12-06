namespace CoffeeMachine.Infrastructure.CoffeeRepository
{
  public class CoffeeMachineRepository
  {
    private Dictionary<string, int> coffeesStock = new Dictionary<string, int>
    {
      { "Americano", 10 },
      { "Cappuccino", 8 },
      { "Lates", 10 },
      { "Mocaccino", 15}
    };

    private Dictionary<string, int> coffeesPrice = new Dictionary<string, int>
    {
      { "Americano", 950 },
      { "Cappuccino", 1200 },
      { "Lates", 1350 },
      { "Mocaccino", 1500}
    };

    public Dictionary<string, int> GetCoffeesAndItsStock()
    {
      return new Dictionary<string, int>(coffeesStock);
    }

    public Dictionary<string, int> GetCoffeesAndItsPrice()
    {
      return new Dictionary<string, int>(coffeesPrice);
    }

    public void UpdateCoffeeStock(string coffeeName, int soldStock)
    {
      if (!coffeesStock.ContainsKey(coffeeName))
        throw new ArgumentException($"El café '{coffeeName}' no está en stock.");

      if (coffeesStock[coffeeName] - soldStock < 0)
        throw new ArgumentException($"No hay suficiente stock para '{coffeeName}'");

      coffeesStock[coffeeName] -= soldStock;
    }
  }
}
