using Microsoft.AspNetCore.Mvc;

namespace CoffeeMachine.Api.Controllers
{
  public class CoffeeMachineChangeController : Controller
  {
    [HttpGet("getQuantity")]
    public ActionResult<Dictionary<string, int>> GetQuantity()
    {
      return StatusCode(500, "Aún no implementado");
    }
  }
}
