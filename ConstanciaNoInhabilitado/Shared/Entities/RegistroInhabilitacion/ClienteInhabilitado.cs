using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstanciaNoInhabilitado.Shared.Entities.RegistroInhabilitacion
{
	public class ClienteInhabilitado
	{
		/// <summary>
		/// Obtiene uno o mas elementos del objeto WsInhabilitado.Entidades.Inhabilitado
		/// </summary>
		/// <param name="criterio">Criterio de busqueda</param>
		/// <param name="tipo">Tipo de Busqueda: 0.- RFC. 1.- Nombre. 2.-ApellidoPaterno. 3.- ApellidoMaterno. 4.-Id </param>
		/// <returns>Arreglo de tipo Inhabilitado</returns>
		public static List<Inhabilitado> GetInhabilitado(string criterio, int tipo)
		{
			List<Inhabilitado> inhabilitado = new();
			return inhabilitado;
		}

		/// <summary>
		/// Inserta un objeto a base de datos si el ID es Cero.
		/// Actualiza un objeto si el ID es mayor a Cero.
		/// </summary>
		/// <param name="objeto">El Objeto debe pertenecer al paquete WsInhabilitado.Entidades </param>
		/// <returns>Retornara el Id (Mayor a Cero) o un estatus (Numero Negativo o Cero)</returns>
		public static int SetObjeto(params Object[] objeto)
		{
			return objeto.Length;
		}
	}
}
