using ConstanciaNoInhabilitado.Shared.Entities.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstanciaNoInhabilitado.Server.Controllers.Login
{
	[Route("api/[controller]")]
	[ApiController]
	public class AgregarInhabilitación : ControllerBase
	{
		[HttpPost]
		[Route("CreateInhabilitacion")]
		public async Task<RegistrarNuevaInhabilitacion> CreateInhabilitacion(RegistrarNuevaInhabilitacion registrarNuevaInhabilitacion)
		{
			try
			{
				return registrarNuevaInhabilitacion;
			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}
