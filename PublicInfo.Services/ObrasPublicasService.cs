using PublicInfo.Domain.Entities;
using PublicInfo.Domain.Entities.Csv;
using PublicInfo.Domain.Entities.Filters;
using PublicInfo.Domain.Entities.Responses;
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
                records = records.Where(x => DateTime.Parse(x.FechaInicioProyecto) >= filter.FromDate);

            if (filter.ToDate != DateTime.MinValue)
                records = records.Where(x => DateTime.Parse(x.FechaFinProyecto) <= filter.ToDate);


            var list = new List<ObrasPublicasResponse>();

            foreach (var item in records)
            {
                list.Add(new ObrasPublicasResponse()
                {
                    CurrencyType = item.TipoMoneda,
                    Deparment = item.NombreDepto,
                    Description = item.DescripicionFisica,
                    Duration = item.DuracionObrasDias,
                    EndDate = DateTime.Parse(item.FechaFinProyecto),
                    ProjectName = item.NombreObra,
                    ProjectType = item.TipoProyecto,
                    ProjectUrl = item.Url_perfil_obra,
                    Province = item.NombreProvincia,
                    Section = item.SectorNombre,
                    StartDate = DateTime.Parse(item.FechaInicioProyecto),
                    Status = item.EtapaObra,
                    TotalAmount = decimal.Parse(item.MontoTotal.Replace('.', ',')).ToString("C", CultureInfo.CurrentCulture)
                });
            }

            return list.Skip(pagedData.page * pagedData.size).Take(pagedData.size).ToList();
        }

    }
}
