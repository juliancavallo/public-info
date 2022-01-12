using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicInfo.Domain.Entities.Filters
{
    public class ProjectFilter
    {
        public string Province { get; set; }
        public string Department { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal? TotalAmountMin { get; set; }
        public decimal? TotalAmountMax { get; set; }
        public string Description { get; set; }
    }
}
