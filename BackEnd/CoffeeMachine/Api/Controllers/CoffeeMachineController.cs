using CoffeeMachine.Application.Services.CoffeeMachine;
using CoffeeMachine.Domain.CoffeeModels;
using CoffeeMachine.Domain.Models;
using CoffeeMachine.Domain.MoneyModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CoffeeMachine.Api.Controllers
{
  [ApiController]
  [Route("Api/Coffee")]
  public class CoffeeMachineController : Controller
  {
    private readonly ICoffeMachineService _CoffeMachineService;

    public CoffeeMachineController(ICoffeMachineService coffeMachineService)
    {
      this._CoffeMachineService = coffeMachineService;
    }

    [HttpGet("Stocks")]
    public ActionResult<List<CoffeeStockData>> GetCoffeesStock()
    {
      List<CoffeeStockData> coffeeStocks = this._CoffeMachineService.GetCoffees();

      if (coffeeStocks == null || coffeeStocks.Count == 0)
        return NotFound("No se encontró un catálogo de café");

      return Ok(coffeeStocks);
    }

    [HttpGet("Prices")]
    public ActionResult<List<CoffeePriceData>> GetCoffeePricesInCents()
    {
      List<CoffeePriceData> coffeePrices = this._CoffeMachineService.GetCoffeesPrices();

      if (coffeePrices == null || coffeePrices.Count == 0)
        return NotFound("No se encontró un catálogo de cafés con precios");

      return Ok(coffeePrices);
    }

    [HttpPost("Buy")]
    public ActionResult<MoneyChangeData> BuyCoffee([FromBody] OrderCoffeeRequest request)
    {
      try
      {
        MoneyChangeData changeData = this._CoffeMachineService.BuyCoffee(request);

        return Ok(changeData);
      } catch (ArgumentException ex)
      {
        return BadRequest(ex.Message);
      } catch (InvalidOperationException ex)
      {
        return BadRequest(ex.Message);
      } catch (Exception ex)
      {
        return StatusCode(500, $"Error inesperado: {ex.Message}");
      }
    }
  }
}
