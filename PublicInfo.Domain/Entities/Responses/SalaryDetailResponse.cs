using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicInfo.Domain.Entities.Responses
{
    public class SalaryDetailResponse
    {
        public int DocumentNumber { get; set; }
        public string Position { get; set; }
        public string MonthlyWage { get; set; }
        public int MonthNum { get; set; }
    }
}
