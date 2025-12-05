using CoffeeMachine.Application.Services.CoffeeMachine;
using CoffeeMachine.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeMachine.Api.Controllers
{
  public class CoffeeMachineController : Controller
  {
    [HttpGet("getCoffees")]
    public ActionResult<Dictionary<string, int>> GetCoffeePrices()
    {
      return StatusCode(500, "Aún no implementado");
    }

    [HttpGet("getCoffeePricesInCents")]
    public ActionResult<Dictionary<string, int>> GetCoffeePricesInCents()
    {
      return StatusCode(500, "Aún no implementado");
    }

    [HttpPost("buyCoffee")]
    public ActionResult<string> BuyCoffee([FromBody] OrderCoffeeRequest request)
    {
      /*
      if (request.Order == null || request.Order.Count == 0)
          return BadRequest("Ordem vacia.");

      if (request.Payment.TotalAmount <= 0)
          return BadRequest("Dinero insuficiente ");

      try
      {
          var costoTotal = request.Order.Sum(o => _db.keyValues2.First(c => c.Key == o.Key).Value * o.Value);

          if (request.Payment.TotalAmount < costoTotal)
          { 
              return BadRequest("Dinero insuficiente ");
          }


          foreach (var cafe in request.Order)
          {
              var selected = _db.keyValues.First(c => c.Key == cafe.Key).Key;
              if (cafe.Value > _db.keyValues[selected])
              {
                  return $"No hay suficientes {selected} en la máquina.";
              }
              _db.keyValues[selected] -= cafe.Value;
          }

          var change = request.Payment.TotalAmount - costoTotal;
          String result = $"Su vuelto es de: {change} colones. Desglose:";

          foreach (var coin in _db.keyValues3.Keys.OrderByDescending(c => c))
          {
              var count = Math.Min(change / coin, _db.keyValues3[coin]);
              if (count > 0)
              {
                  result +=  $" {count} moneda de {coin},  ";              
                  change -= coin * count;
              }
          }


          if (change > 0)
          {
              return StatusCode(500, "No hay suficiente cambio en la máquina.");
          }

          return Ok(result);
      }
      catch (ArgumentException ex)
      {
          return BadRequest(ex.Message);
      }
      catch (Exception ex)
      {
          return StatusCode(500, ex.Message);
      }
      */

      return StatusCode(500, "Aún no implementado");
    }
  }
}
