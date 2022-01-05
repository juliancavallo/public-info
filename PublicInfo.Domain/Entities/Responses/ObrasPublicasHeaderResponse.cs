using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicInfo.Domain.Entities.Responses
{
    public class ObrasPublicasHeaderResponse
    {
        public string ProjectName { get; set; }
        public string TotalAmount { get; set; }
        public string Province { get; set; }
        public string Deparment { get; set; }
    }
}
