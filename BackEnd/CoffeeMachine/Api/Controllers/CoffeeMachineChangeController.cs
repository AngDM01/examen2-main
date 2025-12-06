using CoffeeMachine.Application.Services.CoffeeMoneyChange;
using CoffeeMachine.Domain.MoneyModels;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeMachine.Api.Controllers
{
  [ApiController]
  [Route("Api/Money")]
  public class CoffeeMachineChangeController : Controller
  {

    private readonly ICoffeMachineChangeService _CoffeMachineChangeService;

    public CoffeeMachineChangeController(ICoffeMachineChangeService coffeMachineChangeService)
    {
      this._CoffeMachineChangeService = coffeMachineChangeService;
    }

    [HttpGet("ChangeStock")]
    public ActionResult<List<MoneyStockData>> GetChangeStock()
    {
      List<MoneyStockData> moneyStocks = this._CoffeMachineChangeService.GetMoneyChangeStock();

      return Ok(moneyStocks);
    }
  }
}
