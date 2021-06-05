using Microsoft.AspNetCore.Mvc;

namespace tcc_back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public ActionResult<bool> IsAlive([FromQuery] string uf)
        {
            return Ok(true);
        }
    }
}