using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using tcc_back.Services;
using tcc_back.SystemExceptions;

namespace tcc_back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoberturaController : ControllerBase
    {
        private readonly ICoberturaService _service;
        public CoberturaController(ICoberturaService service)
        {
            _service = service;
        }

        [HttpGet("cities")]
        public ActionResult<IEnumerable<String>> GetCities([FromQuery] string uf)
        {
            try
            {
                return Ok(_service.GetCitiesFromState(uf));
            }
            catch (AppException)
            {
                return BadRequest("Parametro inválido");
            }
        }
    }
}