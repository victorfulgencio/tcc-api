using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tcc_back.Dtos;
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
        public ActionResult<IEnumerable<string>> GetCities([FromQuery] string uf)
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

        [HttpGet("areas")]
        public ActionResult<IEnumerable<string>> GetAreas([FromQuery] string uf, [FromQuery] string city)
        {
            try
            {
                return Ok(_service.GetAreas(uf, city));
            }
            catch (AppException)
            {
                return BadRequest("Parametro inválido");
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<IEnumerable<string>>> ComputeFuzzyClassifier([FromBody] FuzzyClassifierInputDto inputDto)
        {
            try
            {
                return Ok(await _service.GetFuzzyClassifierOutputAsync(inputDto));
            }
            catch (AppException)
            {
                return BadRequest("Parametro inválido");
            }
        }
    }
}