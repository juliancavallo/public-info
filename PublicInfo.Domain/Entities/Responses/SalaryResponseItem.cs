using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicInfo.Domain.Entities.Responses
{
    public class SalaryResponseItem
    {
        public SalaryHeaderResponse Header { get; set; }

        public SalaryDetailResponse Detail { get; set; }
    }
}
