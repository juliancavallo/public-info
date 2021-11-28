using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicInfo.Domain.Entities.Responses
{
    public class ObrasPublicasResponse
    {
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string TotalAmount { get; set; }
        public string Section { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Duration { get; set; }
        public string ProjectType { get; set; }
        public string Deparment { get; set; }
        public string Province { get; set; }
        public string Status { get; set; }
        public string CurrencyType { get; set; }
        public string ProjectUrl { get; set; }
    }
}
