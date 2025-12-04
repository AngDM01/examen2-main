namespace CoffeeMachine.Domain.MoneyModels
{
  public class Payment
  {
    public int TotalAmount { get; set; }

    public List<int> Coins { get; set; } = new();

    public List<int> Bills { get; set; } = new();
  }
}
