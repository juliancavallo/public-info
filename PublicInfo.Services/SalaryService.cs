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
    public class SalaryService : ISalaryService
    {
        public List<SalaryResponse> Get(string url, PagedData pagedData, SalaryFilter filter)
        {
            var records = Domain.Helpers.CsvHelper.GetAllRecordsFromCsv<SalariosAutoridadesCsvRecord>(url);

            if (!string.IsNullOrWhiteSpace(filter.FirstName))
                records = records.Where(x => x.Nombre.ToLower().Contains(filter.FirstName.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.LastName))
                records = records.Where(x => x.Apellido.ToLower().Contains(filter.LastName.ToLower()));

            if (filter.MinMonthlyWage.HasValue)
                records = records.Where(x => decimal.Parse(x.Asignacion_Mensual) >= filter.MinMonthlyWage);

            if (filter.MaxMonthlyWage.HasValue)
                records = records.Where(x => decimal.Parse(x.Asignacion_Mensual) <= filter.MaxMonthlyWage);

            if (filter.MonthNum.HasValue)
                records = records.Where(x => int.Parse(x.NumMes) == filter.MonthNum);

            if (!string.IsNullOrWhiteSpace(filter.Position))
                records = records.Where(x => x.Cargo.ToLower().Contains(filter.Position.ToLower()));

            if(!string.IsNullOrWhiteSpace(filter.Section))
                records = records.Where(x => x.Juridiccion.ToLower().Contains(filter.Section.ToLower()));

            if(filter.Year.HasValue)
                records = records.Where(x => int.Parse(x.Ano) == filter.Year);

            var list = new List<SalaryResponse>();

            foreach (var item in records)
            {
                list.Add(new SalaryResponse()
                {
                    DocumentNumber = int.Parse(item.NumDocumento),
                    DocumentType = item.Tipo_documento,
                    FirstName = item.Nombre,
                    LastName = item.Apellido,
                    Month = item.Mes,
                    MonthlyWage = CurrencyHelper.ParseCurrencyValueToString(item.Asignacion_Mensual),
                    MonthNum = int.Parse(item.NumMes),
                    Position = item.Cargo,
                    Remarks = item.Observaciones,
                    Section = item.Juridiccion,
                    Year = int.Parse(item.Ano)
                });
            }

            return list
                .OrderByDescending(x => x.Year).ThenByDescending(x => x.MonthNum)
                .Skip(pagedData.page * pagedData.size)
                .Take(pagedData.size)
                .ToList();
        }

    }
}
