using PublicInfo.Domain.Entities;
using PublicInfo.Domain.Entities.Filters;
using PublicInfo.Domain.Entities.Responses;
using System.Collections.Generic;

namespace PublicInfo.Domain.Services
{
    public interface IProjectService
    {
        public ProjectResponse Get(string url, PagedData pagedData, ProjectFilter filter);
    }
}
