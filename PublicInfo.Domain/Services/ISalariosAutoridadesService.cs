using PublicInfo.Domain.Entities;
using PublicInfo.Domain.Entities.Filters;
using PublicInfo.Domain.Entities.Responses;
using System.Collections.Generic;

namespace PublicInfo.Domain.Services
{
    public interface ISalariosAutoridadesService
    {
        public List<SalariosAutoridadesResponse> Get(string url, PagedData pagedData, SalariosAutoridadesFilter filter);
    }
}
