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

	public class Inhabilitado
	{
		public int _tipo { get; set; }
		public int _idUsuario { get; set; }
		public string _curp { get; set; }
		public string CURP
		{
			get { return _curp; }
			set
			{
				if (_curp != value)
				{
					_curp = value;
				}
			}
		}

		public int Tipo
		{
			get { return _tipo; }
			set
			{
				if (_tipo != value)
				{
					_tipo = value;
				}
			}
		}

		public int IdUsuario
		{
			get { return _idUsuario; }
			set
			{
				if (_idUsuario != value)
				{
					_idUsuario = value;
				}
			}
		}
	}
}
