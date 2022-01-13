using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicInfo.Domain.Entities.Responses
{
    public class SalaryHeaderResponse
    {
        public string FullName { get; set; }
        public string Section { get; set; }
        public int Year { get; set; }
        public string Month { get; set; }
        public string MonthlyWage { get; set; }
    }
}
