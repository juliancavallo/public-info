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
        public SalaryResponse Get(string url, PagedData pagedData, SalaryFilter filter)
        {
            var records = Domain.Helpers.CsvHelper.GetAllRecordsFromCsv<SalaryCsvRecord>(url, System.Text.Encoding.UTF8);

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

            var list = new List<SalaryResponseItem>();

            foreach (var item in records)
            {
                list.Add(new SalaryResponseItem()
                {
                    Header = new SalaryHeaderResponse
                    {
                        FullName = item.Apellido + ", " + item.Nombre,
                        Month = item.Mes,
                        MonthlyWage = CurrencyHelper.ParseCurrencyValueToString(item.Asignacion_Mensual),
                        Section = item.Juridiccion,
                        Year = int.Parse(item.Ano)
                    },
                    Detail = new SalaryDetailResponse()
                    {
                        DocumentNumber = int.Parse(item.NumDocumento),
                        MonthlyWage = CurrencyHelper.ParseCurrencyValueToString(item.Asignacion_Mensual),
                        Position = item.Cargo,
                        MonthNum = int.Parse(item.NumMes)
                    }
                    
                });
            }

            return new SalaryResponse()
            {
                Items = 
                    this.Sort(pagedData.sidx, pagedData.sord, list)
                    .Skip(pagedData.page * pagedData.size)
                    .Take(pagedData.size)
                    .ToList(),
                pages = (int)Math.Ceiling((decimal)(list.Count / pagedData.size))
            };
        }


        private IOrderedEnumerable<SalaryResponseItem> Sort(string sidx, string sord, List<SalaryResponseItem> list)
        {
            IOrderedEnumerable<SalaryResponseItem> result;
            switch (sidx)
            {
                case SalarySidx.YEAR:
                default:
                    if (sord == "asc")
                        result = list.OrderBy(x => x.Header.Year).ThenBy(x => x.Detail.MonthNum);
                    else
                        result = list.OrderByDescending(x => x.Header.Year).ThenByDescending(x => x.Detail.MonthNum);
                    break;

                case SalarySidx.MONTH:
                    if (sord == "asc")
                        result = list.OrderBy(x => x.Detail.MonthNum).ThenBy(x => x.Header.Year);
                    else
                        result = list.OrderByDescending(x => x.Detail.MonthNum).ThenByDescending(x => x.Header.Year);
                    break;

                case SalarySidx.MONTHLY_WAGE:
                    if (sord == "asc")
                        result = list.OrderBy(x => decimal.Parse(x.Header.MonthlyWage.Replace("$", "").Replace(".", "").Trim()));
                    else
                        result = list.OrderByDescending(x => decimal.Parse(x.Header.MonthlyWage.Replace("$", "").Replace(".", "").Trim()));
                    break;
                
                case SalarySidx.SECTION:
                    if (sord == "asc")
                        result = list.OrderBy(x => x.Header.Section);
                    else
                        result = list.OrderByDescending(x => x.Header.Section);
                    break;


                case SalarySidx.NAME:
                    if (sord == "asc")
                        result = list.OrderBy(x => x.Header.FullName);
                    else
                        result = list.OrderByDescending(x => x.Header.FullName);
                    break;
            }

            return result;
        }

    }
}
