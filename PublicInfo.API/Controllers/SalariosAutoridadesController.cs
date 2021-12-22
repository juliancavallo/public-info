using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PublicInfo.Domain.Entities;
using PublicInfo.Domain.Entities.Filters;
using PublicInfo.Domain.Services;
using System;
using System.Globalization;
using System.IO;

namespace PublicInfo.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalariosAutoridadesController : ControllerBase
    {
        private readonly IGovernmentAPIService service;
        private readonly ISalariosAutoridadesService salariosAutoridadesService;
        public SalariosAutoridadesController(IGovernmentAPIService service, ISalariosAutoridadesService salariosAutoridadesService)
        {
            this.service = service;
            this.salariosAutoridadesService = salariosAutoridadesService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] SalariosAutoridadesFilter filter, [FromQuery] PagedData pagedData)
        {
            try
            {

            pagedData.size = pagedData.size == 0 ? 10 : pagedData.size;
            pagedData.size = Math.Min(pagedData.size, 50);
            pagedData.page = Math.Max(pagedData.page, 0);

            string url = service.GetDatasetCsvURL("jgm-asignacion-salarial-autoridades-superiores-poder-ejecutivo-nacional");
            var result = salariosAutoridadesService.Get(url, pagedData, filter);

            return Ok(result);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
        }
    }
}
