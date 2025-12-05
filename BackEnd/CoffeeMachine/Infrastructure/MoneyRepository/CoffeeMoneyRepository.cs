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
      return changeStock;
    }

    public void SetMoneyChangeStock(int coinValue, int newStock)
    {
      if (!changeStock.ContainsKey(coinValue))
        throw new ArgumentException($"No existen monedas de '{coinValue}' para cambio.");

      changeStock[coinValue] = newStock;
    }
  }
}
