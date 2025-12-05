using CoffeeMachine.Application.Commands;
using CoffeeMachine.Domain.CoffeeModels;
using CoffeeMachine.Domain.Models;
using CoffeeMachine.Domain.MoneyModels;
using CoffeeMachine.Infrastructure.CoffeeRepository;
using CoffeeMachine.Infrastructure.MoneyRepository;

namespace CoffeeMachine.Application.Services.CoffeeMachine
{
  public class CoffeMachineService : ICoffeMachineService
  {
    private readonly CoffeeMachineRepository _CoffeeMachineRepo;
    private readonly IBuyCoffeeCommand _BuyCoffeeCommand;

    public CoffeMachineService(CoffeeMachineRepository coffeeMachineRepo, IBuyCoffeeCommand buyCoffeeCommand)
    {
      this._CoffeeMachineRepo = coffeeMachineRepo;
      this._BuyCoffeeCommand = buyCoffeeCommand;
    }

    public List<CoffeeStockData> GetCoffees()
    {
      Dictionary<string, int> coffeeStockDictionary = this._CoffeeMachineRepo.GetCoffeesAndItsStock();

      List<CoffeeStockData> coffeeStock = new List<CoffeeStockData>();

      // Convertir el diccionario en su clase correspondiente
      foreach (var coffee in coffeeStockDictionary)
      {
        coffeeStock.Add(new CoffeeStockData
        {
          CoffeeName = coffee.Key,
          CoffeeStock = coffee.Value
        });
      }

      return coffeeStock;
    }

    public List<CoffeePriceData> GetCoffeesPrices()
    {
      Dictionary<string, int> coffeePriceDictionary = this._CoffeeMachineRepo.GetCoffeesAndItsStock();

      List<CoffeePriceData> coffeePrice = new List<CoffeePriceData>();

      // Convertir el diccionario en su clase correspondiente
      foreach (var coffee in coffeePriceDictionary)
      {
        coffeePrice.Add(new CoffeePriceData
        {
          CoffeeName = coffee.Key,
          CoffeePrice = coffee.Value
        });
      }

      return coffeePrice;
    }

    public MoneyChangeData BuyCoffee(OrderCoffeeRequest request)
    {
      if (request.Order == null || request.Order.Count == 0)
          throw new ArgumentException("Orden vacia.");

      if (request.Payment.TotalAmount <= 0)
        throw new ArgumentException("Dinero insuficiente.");

      List<CoffeeStockData> coffeesStocks = this.GetCoffees();
      List<CoffeePriceData> coffeesPrices = this.GetCoffeesPrices();

      /* 
       * Convertir cada elemento de la orden en una clase con toda la información
       * necesaria para realizar la compra.
      */
      var selectedCoffees = request.Order.Select(order => new CoffeeOrderItem
      {
        CoffeeName = order.Key,
        Amount = order.Value,
        Price = coffeesPrices.First(x => x.CoffeeName == order.Key).CoffeePrice,
        ActualStock = coffeesStocks.First(x => x.CoffeeName == order.Key).CoffeeStock
      }).ToList();

      int totalCost = selectedCoffees.Sum(c => c.Price * c.Amount);

      if (request.Payment.TotalAmount < totalCost)
        throw new ArgumentException("Dinero insuficiente. Faltan: " + (totalCost - request.Payment.TotalAmount) + ".");

      foreach (var coffee in selectedCoffees)
      {
        if (coffee.Amount > coffee.ActualStock)
        {
          throw new InvalidOperationException($"No hay suficientes {coffee.CoffeeName} en la máquina.");
        }
      }

      // Si llega acá, es porque se cumple todo para poder realizar la compra
      try
      {
        return this._BuyCoffeeCommand.Execute(selectedCoffees, request.Payment);
      }
      catch (Exception ex)
      {
        throw;
      }
    }
  }
}
