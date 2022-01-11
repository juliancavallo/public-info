using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicInfo.Domain.Entities
{
    public class PagedData
    {
        public int page { get; set; }
        public int size { get; set; }
        public string sord { get; set; }
        public string sidx { get; set; }
    }
}
