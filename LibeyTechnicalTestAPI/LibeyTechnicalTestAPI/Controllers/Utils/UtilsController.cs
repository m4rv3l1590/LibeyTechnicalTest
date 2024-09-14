using LibeyTechnicalTestDomain.DocumentTypeAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibeyTechnicalTestAPI.Controllers.Utils
{
    [ApiController]
    [Route("[controller]")]
    public class UtilsController : Controller
    {
        private readonly IUtilsAggregate _aggregate;
        public UtilsController(IUtilsAggregate aggregate)
        {
            _aggregate = aggregate;
        }
        [HttpGet("GetAllDocumentType")]
        public IActionResult GetAllDocumentType()
        {
            var row = _aggregate.GetAllDocumentType();
            return Ok(row);
        }

        [HttpGet("GetAllRegion")]
        public IActionResult GetAllRegion()
        {
            var row = _aggregate.GetAllRegion();
            return Ok(row);
        }

        [HttpGet("GetProvince")]
        public IActionResult GetProvince([FromQuery] string regionCode)
        {
            var row = _aggregate.GetProvince(regionCode);
            return Ok(row);
        }

        [HttpGet("GetUbigeo")]
        public IActionResult GetUbigeo([FromQuery] string regionCode, [FromQuery] string provinceCode)
        {
            var row = _aggregate.GetUbigeo(regionCode, provinceCode);
            return Ok(row);
        }

    }
}
