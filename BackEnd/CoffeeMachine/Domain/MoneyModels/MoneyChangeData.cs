namespace CoffeeMachine.Domain.MoneyModels
{
  public class MoneyChangeData
  {
    public int TotalChange { get; set; }

    public List<MoneyStockData> ChangeBreakdown { get; set; } = new();
  }
}
