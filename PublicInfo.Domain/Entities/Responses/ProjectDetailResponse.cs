using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicInfo.Domain.Entities.Responses
{
    public class ProjectDetailResponse
    {
        public string Description { get; set; }
        public string Section { get; set; }
        public string StartYear { get; set; }
        public string EndYear { get; set; }
        public string Duration { get; set; }
        public string ProjectType { get; set; }
        public string Status { get; set; }
        public string CurrencyType { get; set; }
        public string ProjectUrl { get; set; }
    }
}
