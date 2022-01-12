using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicInfo.Domain.Entities.Responses
{
    public class ProjectResponseItem
    {
        public ProjectHeaderResponse Header { get; set; }

        public ProjectDetailResponse Detail { get; set; }
    }
}
