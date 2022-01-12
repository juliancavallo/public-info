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
    public class ProjectsController : ControllerBase
    {
        private readonly IGovernmentAPIService service;
        private readonly IProjectService projectService;
        public ProjectsController(IGovernmentAPIService service, IProjectService projectService)
        {
            this.service = service;
            this.projectService = projectService;
        }

        [HttpGet]
        public ActionResult<ProjectResponse> Get([FromQuery] ProjectFilter filter, [FromQuery] PagedData pagedData)
        {
            try
            {
                pagedData.size = pagedData.size == 0 ? 10 : pagedData.size;
                pagedData.size = Math.Min(pagedData.size, 50);
                pagedData.page = Math.Max(pagedData.page, 0);
                pagedData.sord = string.IsNullOrWhiteSpace(pagedData.sord) ? "asc" : pagedData.sord;
                pagedData.sidx = string.IsNullOrWhiteSpace(pagedData.sidx) ? "project" : pagedData.sidx;

                string url = service.GetDatasetCsvURL("obras-mapa-inversiones-argentina");
                var result = projectService.Get(url, pagedData, filter);

                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
        }
    }
}
