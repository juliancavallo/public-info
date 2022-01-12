using PublicInfo.Domain.Entities;
using PublicInfo.Domain.Entities.Csv;
using PublicInfo.Domain.Entities.Filters;
using PublicInfo.Domain.Entities.Responses;
using PublicInfo.Domain.Helpers;
using PublicInfo.Domain.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PublicInfo.Services
{
    public class ProjectService : IProjectService
    {
        public ProjectResponse Get(string url, PagedData pagedData, ProjectFilter filter)
        {
            var records = Domain.Helpers.CsvHelper.GetAllRecordsFromCsv<ProjectCsvRecord>(url);

            if (!string.IsNullOrWhiteSpace(filter.Province))
                records = records.Where(x => x.NombreProvincia.ToLower().Contains(filter.Province.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.Department))
                records = records.Where(x => x.NombreDepto.ToLower().Contains(filter.Department.ToLower()));

            if (filter.TotalAmountMin.HasValue)
                records = records.Where(x => decimal.Parse(x.MontoTotal) >= filter.TotalAmountMin.Value);

            if (filter.TotalAmountMax.HasValue)
                records = records.Where(x => decimal.Parse(x.MontoTotal) <= filter.TotalAmountMax.Value);

            if(filter.FromDate != DateTime.MinValue)
                records = records.Where(x => ConvertYearToDate(x.FechaInicioAnio) >= filter.FromDate);

            if (filter.ToDate != DateTime.MinValue)
                records = records.Where(x => ConvertYearToDate(x.FechaFinAnio) <= filter.ToDate);

            if (!string.IsNullOrWhiteSpace(filter.Description))
                records = records.Where(x => x.DescripicionFisica.Contains(filter.Description) || x.NombreObra.Contains(filter.Description));

            var list = new List<ProjectResponseItem>();

            foreach (var item in records)
            {
                list.Add(new ProjectResponseItem()
                {
                    Header = new ProjectHeaderResponse()
                    {
                        ProjectName = item.NombreObra,
                        TotalAmount = CurrencyHelper.ParseCurrencyValueToString(item.MontoTotal),
                        Province = item.NombreProvincia,
                        Department = item.NombreDepto,
                    },
                    Detail = new ProjectDetailResponse()
                    {
                        CurrencyType = item.TipoMoneda,
                        Description = item.DescripicionFisica,
                        Duration = item.DuracionObrasDias,
                        EndYear = item.FechaFinAnio,
                        ProjectType = item.TipoProyecto,
                        ProjectUrl = item.Url_perfil_obra,
                        Section = item.SectorNombre,
                        StartYear = item.FechaInicioAnio,
                        Status = item.EtapaObra,
                    }
                    
                });
            }


            return new ProjectResponse()
            {
                Items = 
                    this.Sort(pagedData.sidx, pagedData.sord, list)
                    .Skip(pagedData.page * pagedData.size)
                    .Take(pagedData.size)
                    .ToList(),
                pages = (int)Math.Ceiling((decimal)(list.Count / pagedData.size))
            };
        }

        private DateTime ConvertYearToDate(string year)
        {
            return new DateTime(Convert.ToInt32(year), 1, 1);
        }

        private IOrderedEnumerable<ProjectResponseItem> Sort(string sidx, string sord, List<ProjectResponseItem> list)
        {
            IOrderedEnumerable<ProjectResponseItem> result;
            switch (sidx)
            {
                case ProjectSidx.PROJECT:
                    if (sord == "asc")
                        result = list.OrderBy(x => x.Header.ProjectName);
                    else
                        result = list.OrderByDescending(x => x.Header.ProjectName);
                    break;

                case ProjectSidx.TOTAL_AMOUNT:
                    if (sord == "asc")
                        result = list.OrderBy(x => decimal.Parse(x.Header.TotalAmount.Replace("$", "").Replace(".", "").Trim()));
                    else
                        result = list.OrderByDescending(x => decimal.Parse(x.Header.TotalAmount.Replace("$", "").Replace(".", "").Trim()));
                    break;

                case ProjectSidx.DEPARTMENT:
                    if (sord == "asc")
                        result = list.OrderBy(x => x.Header.Department);
                    else
                        result = list.OrderByDescending(x => x.Header.Department);
                    break;

                case ProjectSidx.PROVINCE:
                default:
                    if (sord == "asc")
                        result = list.OrderBy(x => x.Header.Province);
                    else
                        result = list.OrderByDescending(x => x.Header.Province);
                    break;
            }

            return result;
        }
    }
}
