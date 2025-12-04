namespace CoffeeMachine.Models
{
  public class MoneyChangeData
  {
    public int TotalChange { get; set; }

    public List<MoneyStockData> ChangeBreakdown { get; set; } = new();
  }
}
