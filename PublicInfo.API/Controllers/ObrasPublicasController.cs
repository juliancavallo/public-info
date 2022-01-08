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
    public class ObrasPublicasController : ControllerBase
    {
        private readonly IGovernmentAPIService service;
        private readonly IObrasPublicasService obrasPublicasService;
        public ObrasPublicasController(IGovernmentAPIService service, IObrasPublicasService obrasPublicasService)
        {
            this.service = service;
            this.obrasPublicasService = obrasPublicasService;
        }

        [HttpGet]
        public ActionResult<ObrasPublicasResponse> Get([FromQuery] ObrasPublicasFilter filter, [FromQuery] PagedData pagedData)
        {
            try
            {
                pagedData.size = pagedData.size == 0 ? 10 : pagedData.size;
                pagedData.size = Math.Min(pagedData.size, 20);
                pagedData.page = Math.Max(pagedData.page, 0);

                string url = service.GetDatasetCsvURL("obras-mapa-inversiones-argentina");
                var result = obrasPublicasService.Get(url, pagedData, filter);

                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
        }
    }
}
