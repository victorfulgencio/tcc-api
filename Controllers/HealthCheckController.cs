using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using tcc_back.Services;

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