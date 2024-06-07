using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstanciaNoInhabilitado.Shared.Entities.Reportes
{
    public class ReportesConstanciasConsulta
    {
        public string? Referencia { get; set; } //0
        public string? Nombre { get; set; } //1
        public string? RFC { get; set; } //2
        public string? CURP { get; set; } //3
        public string? Sello { get; set; } //4
        public int? Tipo  { get; set; } //5
        public DateTime? FechaExpedicion { get; set; } //6
        public DateTime? FechaVigencia { get; set; } //7
        public DateTime? FechaCreacion { get; set; } //8
        public string? IdUsuario { get; set; } // 9
    }

    public class ReportesConstanciasAltasoBajas
    {
        public int? IdInhabilitacion { get; set; }
        public string? RFC { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Nombre { get; set; }
        public string? Autoridad { get; set; }
        public string? Dependencia { get; set; }
        public string? Cargo { get; set; }
        public string? Periodo { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaTermino { get; set; }
        public DateTime? FechaResolucion { get; set; }
        public int? Tipo { get; set; }
        public string? Origen { get; set; }
        public string? Causa { get; set; }
        public string? Descripcion { get; set; }
        public string? Estatus { get; set; }
    }
    public class CriterioDeReportesConsulta
    {
        /// <summary>
        /// Fecha de Inicio de Reportes
        /// </summary>   
        public string? FECHA1 { get; set; }

        /// <summary>
        /// Fecha de Término de Reportes
        /// </summary>   
        public string? FECHA2 { get; set; }

        /// <summary>
        /// Tipo de Reporte
        /// </summary>   
        public int TIPO { get; set; }
    }
    public class ObtenerReportePdf
    {
        /// <summary>
        /// Fecha de Inicio de Reportes
        /// </summary>   
        public DateTime FechaInicio { get; set; }

        /// <summary>
        /// Fecha de Término de Reportes
        /// </summary>   
        public DateTime FechaTermino { get; set; }

        /// <summary>
        /// Lista de Constancias
        /// </summary>   
        public List<ReportesConstanciasConsulta> Constancias { get; set; }
    }
}
