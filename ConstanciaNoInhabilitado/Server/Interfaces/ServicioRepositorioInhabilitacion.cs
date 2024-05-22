using ConstanciaNoInhabilitado.Shared.Entities.Catalogos;
using ConstanciaNoInhabilitado.Shared.Entities.Login;
using ConstanciaNoInhabilitado.Shared.Entities.RegistroInhabilitacion;

namespace ConstanciaNoInhabilitado.Server.Interfaces
{
	public interface IServicioRepositorioInhabilitacion
	{
		Task<Inhabilitacion> AgregarInhabilitacion(Inhabilitacion Inhabilitacion);
	}
}
