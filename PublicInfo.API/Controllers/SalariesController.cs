using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PublicInfo.Domain.Entities;
using PublicInfo.Domain.Entities.Filters;
using PublicInfo.Domain.Entities.Responses;
using PublicInfo.Domain.Services;
using System;
using System.Globalization;
using System.IO;

namespace PublicInfo.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalariesController : ControllerBase
    {
        private readonly IGovernmentAPIService service;
        private readonly ISalaryService salaryService;
        public SalariesController(IGovernmentAPIService service, ISalaryService salaryService)
        {
            this.service = service;
            this.salaryService = salaryService;
        }

        [HttpGet]
        public ActionResult<SalaryResponse> Get([FromQuery] SalaryFilter filter, [FromQuery] PagedData pagedData)
        {
            try
            {

                pagedData.size = pagedData.size == 0 ? 10 : pagedData.size;
                pagedData.size = Math.Min(pagedData.size, 50);
                pagedData.page = Math.Max(pagedData.page, 1);
                pagedData.sord = string.IsNullOrWhiteSpace(pagedData.sord) ? "asc" : pagedData.sord;
                pagedData.sidx = string.IsNullOrWhiteSpace(pagedData.sidx) ? "year" : pagedData.sidx;

                string url = service.GetDatasetCsvURL("jgm-asignacion-salarial-autoridades-superiores-poder-ejecutivo-nacional");
                var result = salaryService.Get(url, pagedData, filter);

                return Ok(result);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
        }
    }
}
