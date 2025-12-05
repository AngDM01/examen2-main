  namespace CoffeeMachine.Domain.CoffeeModels
{
  public class CoffeeOrderItem
  {
    public string CoffeeName {  get; set; }

    public int Amount { get; set; }

    public int Price { get; set; }

    public int ActualStock { get; set; }
  }
}
