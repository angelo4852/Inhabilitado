using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstanciaNoInhabilitado.Shared.Entities.CargaMasiva
{
    public class CargaMasivaExcel
    {
        public string? Dependencia { get; set; }
        public string? RFC { get; set; }
        public string? Homo { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Nombre { get; set; }
        public string? AutoridadSancionadora { get; set; }
        public string? Puesto { get; set; }
        public string? Periodo { get; set; }
        public DateTime? FechaResolucion { get; set; }
        public DateTime? FechaNotificacion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? IdUsuario { get; set; }
        public bool FaltaInformacion { get; set; }
    }

    public class InhabilitadoCarga
    {
        public int? IdInhabilitado { get; set; }
        public string? NOMB { get; set; }
        public string? AP_PAT { get; set; }
        public string? AP_MAT { get; set; }
        public string? R_F_C { get; set; }
        public int? USU { get; set; }
        public int? TIP { get; set; }
        public bool SeInserto { get; set; }
    }
    public class InhabilitacionCarga
    {
        public int? IdInhabilitacion { get; set; }
        public string? Dependencia { get; set; }
        public string? Autoridad { get; set; }
        public string? Cargo { get; set; }
        public string? Periodo { get; set; }
        public string? FechaResolucion { get; set; }
        public string? FechaNotificacion { get; set; }
        public string? FechaInicio { get; set; }
        public string? FechaFin { get; set; }
        public int? IdInhabilitado { get; set; }
        public int? IdUsuario { get; set; }
        public bool SeInserto { get; set; }
    }

    public class CargaMasivaExcelDTO 
    {
        public List<CargaMasivaExcel> CargaMasivaExcel { get; set; } = new();
        public List<InhabilitadoCarga> InhabilitadoCarga { get; set; } = new();
        public List<InhabilitacionCarga> InhabilitacionCarga { get; set; } = new();
        public List<string> ErroresDeLaCarga { get; set; } = new();
    }
}
