using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConstanciaNoInhabilitado.Shared.Entities.Login;
using ConstanciaNoInhabilitado.Shared.Entities.RegistroInhabilitacion;

namespace ConstanciaNoInhabilitado.Shared.Entities.RegistroInhabilitacion
{
	public class RegistroInhabilitacionValidaciones
	{

		private async Task<string> formatoFecha(string fecha)
		{
			try
			{
				String[] v = fecha.Split(new char[] { '-' });
				return v[2] + "/" + v[1] + "/" + v[0];
			}
			catch
			{
				return fecha;
			}
		}

		public async Task<List<string>> GuardarDatos(RegistrarNuevaInhabilitacion RegistrarNuevaInhabilitacion)
		{
			List<string> listaErrores = new();
			if (Security.ValidarRFC(RegistrarNuevaInhabilitacion.RFC))
			{
				List<Inhabilitado> inhabilitado = ClienteInhabilitado.GetInhabilitado(RegistrarNuevaInhabilitacion.RFC, 0);
				if (inhabilitado != null)
				{
					if (inhabilitado.Count() > 0)
					{
						Inhabilitacion inhabilitacion = new Inhabilitacion();
						if (RegistrarNuevaInhabilitacion.InhabilitacionProceso == 0)
						{
							inhabilitacion.IdInhabilitacion = 0;
							inhabilitacion.IdInhabilitado = inhabilitado[0]._idUsuario;
							inhabilitacion.ExpedienteLegal = RegistrarNuevaInhabilitacion.ExpedienteLegal.ToUpper();
							inhabilitacion.IdDependencia = Convert.ToInt32(RegistrarNuevaInhabilitacion.Dependencia);
							inhabilitacion.Cargo = RegistrarNuevaInhabilitacion.Puesto;
							inhabilitacion.FechaInicio = await formatoFecha(RegistrarNuevaInhabilitacion.FechaInicio.ToString()!);
							inhabilitacion.FechaTermino = await formatoFecha(RegistrarNuevaInhabilitacion.FechaTermino.ToString()!);
							inhabilitacion.Periodo = RegistrarNuevaInhabilitacion.Periodo;
							inhabilitacion.FechaResolucion = await formatoFecha(RegistrarNuevaInhabilitacion.FechaResolución.ToString()!);
							inhabilitacion.Autoridad = RegistrarNuevaInhabilitacion.AutoridadSancionadora;
							inhabilitacion.IdTipoInhabilitacion = Convert.ToInt32(RegistrarNuevaInhabilitacion.TipoInhabilitación);
							inhabilitacion.IdCausaInhabilitacion = Convert.ToInt32(RegistrarNuevaInhabilitacion.CausaInhabilitación);
							inhabilitacion.IdOrigenInhabilitacion = Convert.ToInt32(RegistrarNuevaInhabilitacion.OrigenInhabilitación);
							inhabilitacion.Descripcion = RegistrarNuevaInhabilitacion.DescripciónInhabilitación;
							inhabilitacion.IdUsuario = RegistrarNuevaInhabilitacion.idUsuario;
							inhabilitacion.Estatus = RegistrarNuevaInhabilitacion.Estatus;
							inhabilitacion.EnProceso = RegistrarNuevaInhabilitacion.InhabilitacionProceso;
						}
						else
						{
							inhabilitacion.IdInhabilitacion = 0;
							inhabilitacion.IdInhabilitado = inhabilitado[0]._idUsuario;
							inhabilitacion.ExpedienteLegal = "En proceso";
							inhabilitacion.IdDependencia = 92;
							inhabilitacion.Cargo = "No definido";
							inhabilitacion.FechaInicio = await formatoFecha(DateTime.Today.ToShortDateString());
							inhabilitacion.FechaTermino = await formatoFecha(DateTime.Today.ToShortDateString());
							inhabilitacion.Periodo = "No definido";
							inhabilitacion.FechaResolucion = await formatoFecha(DateTime.Today.ToShortDateString());
							inhabilitacion.Autoridad = RegistrarNuevaInhabilitacion.AutoridadSancionadora;
							inhabilitacion.IdTipoInhabilitacion = Convert.ToInt32(RegistrarNuevaInhabilitacion.TipoInhabilitación);
							inhabilitacion.IdCausaInhabilitacion = 8;
							inhabilitacion.IdOrigenInhabilitacion = 8;
							inhabilitacion.Descripcion = "NA";
							inhabilitacion.IdUsuario = RegistrarNuevaInhabilitacion.idUsuario;
							inhabilitacion.Estatus = RegistrarNuevaInhabilitacion.Estatus;
							inhabilitacion.EnProceso = RegistrarNuevaInhabilitacion.InhabilitacionProceso;
						}

						var errores = await ValidarDatos(inhabilitacion, listaErrores);
						if (errores.Count() == 0)
						{
							//if (ClienteInhabilitado.SetObjeto(inhabilitacion) > 0;
						}
					}
					else
						listaErrores.Add("No se recuperó ningun registro del RFC " + RegistrarNuevaInhabilitacion.RFC);
				}
				else
					listaErrores.Add("No se recuperaron datos del servicio");
			}
			else
				listaErrores.Add("El RFC " + RegistrarNuevaInhabilitacion.RFC + " no es valido");
			return listaErrores;
		}

		private async Task<List<string>> ValidarDatos(Inhabilitacion inhabilitacion, List<string> errores)
		{
			if (inhabilitacion.FechaInicio.Equals("") || inhabilitacion.FechaTermino.Equals("") || inhabilitacion.IdInhabilitado == 0 || inhabilitacion.Cargo.Equals("") ||
					inhabilitacion.Periodo.Equals("") || inhabilitacion.FechaResolucion.Equals("") || inhabilitacion.Autoridad.Equals("") || inhabilitacion.IdTipoInhabilitacion == -1 ||
					inhabilitacion.IdOrigenInhabilitacion == -1 || inhabilitacion.IdCausaInhabilitacion == -1 || inhabilitacion.Descripcion.Equals("") || inhabilitacion.ExpedienteLegal.Equals("") ||
					inhabilitacion.IdDependencia == -1)
			{
				errores.Add("Todos los campos son necesarios para proceder con el registro.");
			}
			else
			{
				try
				{
					if (inhabilitacion.EnProceso == 0)
					{
						DateTime fi = DateTime.Parse(inhabilitacion.FechaInicio);
						DateTime ft = DateTime.Parse(inhabilitacion.FechaTermino);
						DateTime fr = DateTime.Parse(inhabilitacion.FechaResolucion);
						int result = DateTime.Compare(fi, ft);
						if (result >= 0)
						{
							errores.Add("La Fecha de Inicio debe ser menor a la Fecha de Termino.");
						}
					}
				}
				catch
				{
					errores.Add("Las fechas no tienen el formato correcto.");
				}

				if (inhabilitacion.ExpedienteLegal.Trim().Equals(""))
					errores.Add("El expediente legal no puede estar vacio");

				if (!Security.EsAlfaNumerico(inhabilitacion.Descripcion))
				{
					errores.Add("La descripción de la inhabilitación solo debe tener letras y números");
				}

				if (!Security.EsAlfaNumerico(inhabilitacion.Periodo))
				{
					errores.Add("El periodo solo puede tener letras y números");
				}

				if (inhabilitacion.EnProceso == 0)
				{
					DateTime fecha1 = new DateTime(Convert.ToInt32(inhabilitacion.FechaInicio.Split('/')[2]),
					Convert.ToInt32(inhabilitacion.FechaInicio.Split('/')[1]),
					Convert.ToInt32(inhabilitacion.FechaInicio.Split('/')[0]));
					DateTime fecha2 = new DateTime(Convert.ToInt32(inhabilitacion.FechaTermino.Split('/')[2]),
						Convert.ToInt32(inhabilitacion.FechaTermino.Split('/')[1]),
						Convert.ToInt32(inhabilitacion.FechaTermino.Split('/')[0]));

					if (DateTime.Compare(fecha1, fecha2) >= 0)
					{
						errores.Add("La fecha de inicio debe ser mayor a la fecha de termino");
					}
				}

			}

			return errores;
		}

		public async Task<List<string>> ValidarInformacion(RegistrarNuevaInhabilitacion RegistrarNuevaInhabilitacion)
		{
			List<string> listaErrores = new();

			if (RegistrarNuevaInhabilitacion.FaltaCometida == 0 || RegistrarNuevaInhabilitacion.FaltaCometida == null)
			{
				listaErrores.Add("El campo de falta cometida es obligatorio.");
			}
			if (RegistrarNuevaInhabilitacion.RFC == string.Empty || RegistrarNuevaInhabilitacion.RFC == null)
			{
				listaErrores.Add("El campo de RFC es obligatorio.");
			}
			if (RegistrarNuevaInhabilitacion.AutoridadSancionadora == string.Empty || RegistrarNuevaInhabilitacion.AutoridadSancionadora == null)
			{
				listaErrores.Add("El campo de autoridad sancionadora es obligatorio.");
			}
			if (RegistrarNuevaInhabilitacion.Dependencia == 0 || RegistrarNuevaInhabilitacion.Dependencia == null)
			{
				listaErrores.Add("El campo de dependencia es obligatorio.");
			}
			if (RegistrarNuevaInhabilitacion.FechaInicio == null)
			{
				listaErrores.Add("El campo de fecha inicio es obligatorio.");
			}
			if (RegistrarNuevaInhabilitacion.FechaTermino == null)
			{
				listaErrores.Add("El campo de fecha termino es obligatorio.");
			}
			if (RegistrarNuevaInhabilitacion.FechaResolución == null)
			{
				listaErrores.Add("El campo de fecha resolución es obligatorio.");
			}
			if (RegistrarNuevaInhabilitacion.Periodo == string.Empty || RegistrarNuevaInhabilitacion.Periodo == null)
			{
				listaErrores.Add("El campo de periodo es obligatorio.");
			}
			if (RegistrarNuevaInhabilitacion.ExpedienteLegal == string.Empty || RegistrarNuevaInhabilitacion.ExpedienteLegal == null)
			{
				listaErrores.Add("El campo de expediente legal es obligatorio.");
			}
			if (RegistrarNuevaInhabilitacion.TipoInhabilitación == 0 || RegistrarNuevaInhabilitacion.TipoInhabilitación == null)
			{
				listaErrores.Add("El campo de tipo inhabilitación es obligatorio.");
			}
			if (RegistrarNuevaInhabilitacion.OrigenInhabilitación == 0 || RegistrarNuevaInhabilitacion.OrigenInhabilitación == null)
			{
				listaErrores.Add("El campo de origen inhabilitación es obligatorio.");
			}
			if (RegistrarNuevaInhabilitacion.CausaInhabilitación == 0 || RegistrarNuevaInhabilitacion.CausaInhabilitación == null)
			{
				listaErrores.Add("El campo de causa inhabilitación es obligatorio.");
			}
			if (RegistrarNuevaInhabilitacion.TipoSanción == 0 || RegistrarNuevaInhabilitacion.TipoSanción == null)
			{
				listaErrores.Add("El campo de tipo sanción es obligatorio.");
			}
			if (RegistrarNuevaInhabilitacion.TipoMoneda == 0 || RegistrarNuevaInhabilitacion.TipoMoneda == null)
			{
				listaErrores.Add("El campo de tipo moneda es obligatorio.");
			}
			if (RegistrarNuevaInhabilitacion.Monto == 0 || RegistrarNuevaInhabilitacion.Monto == null)
			{
				listaErrores.Add("El campo de monto es obligatorio.");
			}
			if (RegistrarNuevaInhabilitacion.ClaveNivelServidorPúblico == 0 || RegistrarNuevaInhabilitacion.ClaveNivelServidorPúblico == null)
			{
				listaErrores.Add("El campo de clave nivel servidor público es obligatorio.");
			}
			if (RegistrarNuevaInhabilitacion.DescripciónInhabilitación == string.Empty || RegistrarNuevaInhabilitacion.DescripciónInhabilitación == null)
			{
				listaErrores.Add("El campo de descripción inhabilitación es obligatorio.");
			}
			return listaErrores;
		}
	}
}
