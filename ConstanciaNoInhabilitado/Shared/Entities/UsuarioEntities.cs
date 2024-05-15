using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstanciaNoInhabilitado.Shared.Entities
{
    public class UsuarioEntities
    {
        public int? IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? CorreoElectronico { get; set; }
        public string? Usuario { get; set; }
        public string? Contrasena { get; set; }
        public string? FechaCreacion { get; set; }
        public string? FechaModificacion { get; set; }
        public int? IdUsuarioModifica { get; set; }
        public string IdRolUsuario { get; set; }

    }
}
