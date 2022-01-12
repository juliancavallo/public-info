using PublicInfo.Domain.Entities;
using PublicInfo.Domain.Entities.Filters;
using PublicInfo.Domain.Entities.Responses;
using System.Collections.Generic;

namespace PublicInfo.Domain.Services
{
    public interface ISalaryService
    {
        public List<SalaryResponse> Get(string url, PagedData pagedData, SalaryFilter filter);
    }
}
