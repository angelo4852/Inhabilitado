using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstanciaNoInhabilitado.Shared.Entities.Catalogos
{
	public class Dependencia
	{
		public int IdDependencia { set; get; }
		public string? Descripcion { set; get; }
		public string? siglas { set; get; }
        public string? Value { set; get; }
        public bool Acciones { get; set; }
		public int idBandera { set; get; } = 2;


    }

    public class OrigenInhabilitacion
	{
		public int IdOrigenInhabilitacion { set; get; }
		public string? Descripcion { set; get; }
	}

	public class CausaInhabilitacion
	{
		public int IdCausaInhabilitacion { set; get; }
		public string? Descripcion { set; get; }
	}

	public class Nivel
	{
		public int idNivel { set; get; }
		public string? Descripcion { set; get; }
	}

	public class Moneda
	{
		public int idMoneda { set; get; }
		public string? moneda_clave { set; get; }
		public string? moneda_valor { set; get; }
	}

	public class TipoFalta
	{
		public int idTipoFalta { set; get; }
		public string? tipoFalta_clave { set; get; }
		public string? tipoFalta_valor { set; get; }
	}

	public class TipoInhabilitacion
	{
		public int IdTipoInhabilitacion { set; get; }
		public string? Descripcion { set; get; }
	}

	public class TipoSancion
	{
		public int idTipoSancion { set; get; }
		public string? tipoSancion_clave { set; get; }
		public string? tipoSancion_valor { set; get; }
	}
}


