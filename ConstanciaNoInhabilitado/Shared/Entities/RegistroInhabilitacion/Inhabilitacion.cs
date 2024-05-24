using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstanciaNoInhabilitado.Shared.Entities.RegistroInhabilitacion
{
    public class Inhabilitacion
    {
        public int IdInhabilitacion { get; set; }
        public int IdInhabilitado { get; set; }
        public string ExpedienteLegal { get; set; }
        public int IdDependencia { get; set; }
        public string Cargo { get; set; }
        public string FechaInicio { get; set; }
        public string FechaTermino { get; set; }
        public string Periodo { get; set; }
        public string FechaResolucion { get; set; }
        public string Autoridad { get; set; }
        public int IdTipoInhabilitacion { get; set; }
        public int IdCausaInhabilitacion { get; set; }
        public int IdOrigenInhabilitacion { get; set; }
        public string Descripcion { get; set; }
        public int IdUsuario { get; set; }
        public int Estatus { get; set; }
        public int EnProceso { get; set; }
    }

    public class InhabilitacionDTO
    {
        public int IdInhabilitacion { get; set; } = 0;
        public string ExpedienteLegal { get; set; } = string.Empty;
        public string FechaInicio { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        public string FechaTermino { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        public string Cargo { get; set; } = string.Empty;
        public string Periodo { get; set; } = string.Empty;
        public string FechaResolucion { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        public string Autoridad { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int IdInhabilitado { get; set; } = 1;
        public int IdDependencia { get; set; } = 1;
        public int IdTipoInhabilitacion { get; set; } = 1;
        public int IdOrigenInhabilitacion { get; set; } = 1;
        public int IdCausaInhabilitacion { get; set; } = 1;
        public int IdUsuario { get; set; } = 1;
        public string FechaCreacion { get; set; }= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        public string FechaModificacion { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        public int? Estatus { get; set; } = 0;
        public int EnProceso { get; set; } = 0;
        public int? IdTipoFalta { get; set; } = 1;
        public string? OtroTipoFalta { get; set; } = string.Empty;
        public int? IdTipoSancion { get; set; } = 1;
        public string? OtroTipoSancion { get; set; } = string.Empty;
        public decimal? Monto { get; set; } = 0;
        public int? IdMoneda { get; set; } = 1;
        public string? Siglas { get; set; } = string.Empty;
        public int? IdNivel { get; set; } = 1;
        public string RFC { get; set; }
    }

    public class Inhabilitado
    {
        public int IdInhabilitado { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string RFC { get; set; }
        public string CURP { get; set; }
        public string Tipo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaUltimaModificacion { get; set; }
        public int IdUsuario { get; set; }
        public int IdGenero { get; set; }
    }
}
