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
    public class ObrasPublicasService : IObrasPublicasService
    {
        public List<ObrasPublicasResponse> Get(string url, PagedData pagedData, ObrasPublicasFilter filter)
        {
            var records = Domain.Helpers.CsvHelper.GetAllRecordsFromCsv<ObrasPublicasCsvRecord>(url);

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


            var list = new List<ObrasPublicasResponse>();

            foreach (var item in records)
            {
                list.Add(new ObrasPublicasResponse()
                {
                    Header = new ObrasPublicasHeaderResponse()
                    {
                        ProjectName = item.NombreObra,
                        TotalAmount = CurrencyHelper.ParseCurrencyValueToString(item.MontoTotal),
                        Province = item.NombreProvincia,
                        Deparment = item.NombreDepto,
                    },
                    Detail = new ObrasPublicasDetailResponse()
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

            return list
                .OrderByDescending(x => x.Detail.StartYear)
                .Skip(pagedData.page * pagedData.size)
                .Take(pagedData.size)
                .ToList();
        }

        private DateTime ConvertYearToDate(string year)
        {
            return new DateTime(Convert.ToInt32(year), 1, 1);
        }
    }
}
