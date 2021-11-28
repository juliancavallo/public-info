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
    public class SalariosAutoridadesService : ISalariosAutoridadesService
    {
        public List<SalariosAutoridadesResponse> Get(string url, PagedData pagedData, SalariosAutoridadesFilter filter)
        {
            var records = Domain.Helpers.CsvHelper.GetAllRecordsFromCsv<SalariosAutoridadesCsvRecord>(url);

            if (!string.IsNullOrWhiteSpace(filter.FirstName))
                records = records.Where(x => x.Nombre.Contains(filter.FirstName));

            if (!string.IsNullOrWhiteSpace(filter.LastName))
                records = records.Where(x => x.Apellido.Contains(filter.LastName));

            if (filter.MinMonthlyWage.HasValue)
                records = records.Where(x => decimal.Parse(x.Asignacion_Mensual) >= filter.MinMonthlyWage);

            if (filter.MaxMonthlyWage.HasValue)
                records = records.Where(x => decimal.Parse(x.Asignacion_Mensual) <= filter.MaxMonthlyWage);

            if (filter.MonthNum.HasValue)
                records = records.Where(x => int.Parse(x.NumMes) == filter.MonthNum);

            if (!string.IsNullOrWhiteSpace(filter.Position))
                records = records.Where(x => x.Cargo.Contains(filter.Position));

            if(!string.IsNullOrWhiteSpace(filter.Section))
                records = records.Where(x => x.Juridiccion.Contains(filter.Section));

            if(filter.Year.HasValue)
                records = records.Where(x => int.Parse(x.Ano) == filter.Year);

            var list = new List<SalariosAutoridadesResponse>();

            foreach (var item in records)
            {
                list.Add(new SalariosAutoridadesResponse()
                {
                    DocumentNumber = int.Parse(item.NumDocumento),
                    DocumentType = item.Tipo_documento,
                    FirstName = item.Nombre,
                    LastName = item.Apellido,
                    Month = item.Mes,
                    MonthlyWage = decimal.Parse(item.Asignacion_Mensual.Replace('.',',')).ToString("C", CultureInfo.CurrentCulture),
                    MonthNum = int.Parse(item.NumMes),
                    Position = item.Cargo,
                    Remarks = item.Observaciones,
                    Sac = item.Sac,
                    Section = item.Juridiccion,
                    Year = int.Parse(item.Ano)
                });
            }

            return list.Skip(pagedData.page * pagedData.size).Take(pagedData.size).ToList();
        }

    }
}
