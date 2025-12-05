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
      return StatusCode(500, "Aún no implementado");
    }
  }
}
