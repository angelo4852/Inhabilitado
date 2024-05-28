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
        public string ExpedienteLegal { get; set; } 
        public string FechaInicio { get; set; }
        public string FechaTermino { get; set; } 
        public string Cargo { get; set; } 
        public string Periodo { get; set; } 
        public string FechaResolucion { get; set; }
        public string Autoridad { get; set; } 
        public string Descripcion { get; set; } 
        public int IdInhabilitado { get; set; } 
        public int IdDependencia { get; set; } 
        public int IdTipoInhabilitacion { get; set; } 
        public int IdOrigenInhabilitacion { get; set; }
        public int IdCausaInhabilitacion { get; set; }
        public int IdUsuario { get; set; } 
        public string FechaCreacion { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");    
        public string FechaModificacion { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        public int? Estatus { get; set; } 
        public int EnProceso { get; set; } 
        public int? IdTipoFalta { get; set; } 
        public string? OtroTipoFalta { get; set; } 
        public int? IdTipoSancion { get; set; } 
        public string? OtroTipoSancion { get; set; } 
        public decimal? Monto { get; set; } 
        public int? IdMoneda { get; set; } 
        public string? Siglas { get; set; } 
        public int? IdNivel { get; set; }
        public string RFC { get; set; } 
    }

    public class InhabilitacionADD
    {
        public int? IdInhabilitacion { get; set; }
        public string? ExpedienteLegal { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaTermino { get; set; }
        public string? Cargo { get; set; }
        public string? Periodo { get; set; }
        public DateTime? FechaResolucion { get; set; }
        public string? Autoridad { get; set; }
        public string? Descripcion { get; set; }
        public string? NombreInhabilitado { get; set; }
        public string? ApellidoPaternoInhabilitado { get; set; }
        public string? ApellidoMaternoInhabilitado { get; set; }
        public string? RFC { get; set; }
        public string? Tipo { get; set; }
        public string? Dependencia { get; set; }
        public string? TipoInhabilitacion { get; set; }
        public string? OrigenInhabilitacion { get; set; }
        public string? CausaInhabilitacion { get; set; }
        public string? NombreCompletoUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? Estatus { get; set; }
        public bool? EnProceso { get; set; }
        public string? TipoFalta { get; set; }
        public string? OtroTipoFalta { get; set; }
        public string? TipoSancion { get; set; }
        public string? OtroTipoSancion { get; set; }
        public decimal? Monto { get; set; }
        public string? Moneda { get; set; }
        public string? Siglas { get; set; }
        public string? Nivel { get; set; }
    }

    public class InhabilitacionUpdate
    {
        public int IdInhabilitacion { get; set; }
        public string ExpedienteLegal { get; set; }
        public string FechaInicio { get; set; }
        public string FechaTermino { get; set; }
        public string Cargo { get; set; }
        public string Periodo { get; set; }
        public string FechaResolucion { get; set; }
        public string Autoridad { get; set; }
        public string Descripcion { get; set; }
        public string Dependencia { get; set; }
        public string TipoInhabilitacion { get; set; }
        public string OrigenInhabilitacion { get; set; }
        public string CausaInhabilitacion { get; set; }
        public string FechaModificacion { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        public bool RespondeUdpate { get; set; } = false;       
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
