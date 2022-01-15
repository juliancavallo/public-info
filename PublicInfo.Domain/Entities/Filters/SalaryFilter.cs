using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicInfo.Domain.Entities.Filters
{
    public class SalaryFilter
    {
        public int? MonthNum { get; set; }
        public string Section { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Position { get; set; }
        public decimal? MinMonthlyWage { get; set; }
        public decimal? MaxMonthlyWage { get; set; }
    }
}
