using PublicInfo.Domain.Entities.Csv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicInfo.Domain.Entities.Csv
{
    public class ObrasPublicasCsvRecord : CsvRecord
    {   
            public string IdProyecto { get; set; }
            public string NumeroObra { get; set; }
            public string CodigoBapin { get; set; }
            public string FechaInicioAnio { get; set; }
            public string FechaFinAnio { get; set; }
            public string NombreObra { get; set; }
            public string DescripicionFisica { get; set; }
            public string MontoTotal { get; set; }
            public string SectorNombre { get; set; }
            public string AvanceFinanciero { get; set; }
            public string AvanceFisico { get; set; }
            public string EntidadEjecutoraNombre { get; set; }
            public string FechaInicioProyecto { get; set; }
            public string FechaFinProyecto { get; set; }
            public string DuracionObrasDias { get; set; }
            public string ObjetivoGeneral { get; set; }
            public string TipoProyecto { get; set; }
            public string NombreDepto { get; set; }
            public string NombreProvincia { get; set; }
            public string Codigo_Bahra { get; set; }
            public string EtapaObra { get; set; }
            public string TipoMoneda { get; set; }
            public string Url_perfil_obra { get; set; }
    }

}
