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
    public class KmlFileController : ControllerBase
    {
        private readonly IKmlFileService _service;
        public KmlFileController(IKmlFileService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<string> GetFilePath([FromQuery] string uf, [FromQuery] string city)
        {
            try
            {
                var result = _service.GetFilePath(uf, city).Replace("\0", string.Empty);
                return Ok($"\"{result}\"");
            }
            catch (NotFoundException)
            {
                return NotFound("Cidade não encontrada");
            }
            catch (InvalidParameterException)
            {
                return BadRequest("Parametro inválido");
            }
        }
    }
}