using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstanciaNoInhabilitado.Shared.Entities.Constancia
{
    public class ConstanciaInhabilitado
    {
        public string? Referencia { get; set; }
        public string? Nombre { get; set; }
        public string? RFC { get; set; }
        public string? CURP { get; set; }
        public string? Sello { get; set; }
        public string? Tipo { get; set; }
        public DateTime? FechaExpedicion { get; set; }
        public DateTime? FechaVigencia { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? IdUsuario { get; set; }
    }

    public class ConstanciaBusqueda
    {
        public string? Criterio { get; set; }
        public int? Tipo { get; set; }
    }
}
