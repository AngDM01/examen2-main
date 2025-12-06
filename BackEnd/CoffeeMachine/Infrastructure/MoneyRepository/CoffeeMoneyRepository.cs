namespace CoffeeMachine.Infrastructure.MoneyRepository
{
  public class CoffeeMoneyRepository
  {
    private Dictionary<int, int> changeStock = new Dictionary<int, int>
    {
      { 500, 20 },
      { 100, 30 },
      { 50, 50 },
      { 25, 25}
    };

    public Dictionary<int, int> GetChangeStock()
    {
      return new Dictionary<int, int>(changeStock);
    }

    public void UpdateMoneyChangeStock(int coinValue, int quantityCahnge)
    {
      if (!changeStock.ContainsKey(coinValue))
        throw new ArgumentException($"No existen monedas de '{coinValue}' para cambio.");

      if (changeStock[coinValue] - quantityCahnge < 0)
        throw new ArgumentException($"No hay suficiente stock de '{coinValue}'");

      changeStock[coinValue] -= quantityCahnge;
    }
  }
}
