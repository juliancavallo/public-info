using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicInfo.Domain.Entities.Responses
{
    public class SalariosAutoridadesResponse
    {
        public int Year { get; set; }
        public int MonthNum { get; set; }
        public string Month { get; set; }
        public string Section { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string DocumentType { get; set; }
        public int DocumentNumber { get; set; }
        public string Position { get; set; }
        public string MonthlyWage { get; set; }
        public string Remarks { get; set; }
    }
}
