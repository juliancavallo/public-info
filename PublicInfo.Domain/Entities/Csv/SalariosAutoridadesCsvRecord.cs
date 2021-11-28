using CsvHelper.Configuration.Attributes;

namespace PublicInfo.Domain.Entities.Csv
{
    public class SalariosAutoridadesCsvRecord : CsvRecord
    {
        [Name("Año")]
        public string Ano { get; set; }
        [Name("N° mes")]
        public string NumMes { get; set; }
        public string Mes { get; set; }
        [Name("Juridicción")]
        public string Juridiccion { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string Tipo_documento { get; set; }
        [Name("N°_documento")]
        public string NumDocumento { get; set; }
        public string Cargo { get; set; }
        public string Asignacion_Mensual { get; set; }
        public string Sac { get; set; }
        public string Desarraigo { get; set; }
        public string Otros { get; set; }
        public string Observaciones { get; set; }
    }

}
