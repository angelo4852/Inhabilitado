using ConstanciaNoInhabilitado.Server.Interfaces;
using ConstanciaNoInhabilitado.Server.Servicios;
using ConstanciaNoInhabilitado.Shared.Entities;
using ConstanciaNoInhabilitado.Shared.Entities.Catalogos;
using ConstanciaNoInhabilitado.Shared.Entities.Login;
using Microsoft.AspNetCore.Mvc;

namespace ConstanciaNoInhabilitado.Server.Controllers.Login
{
	[Route("api/[controller]")]
	[ApiController]
	public class Catalogos : ControllerBase
	{
		private readonly string connectionString;
		List<UsuarioEntities> InhabilitadoList = new List<UsuarioEntities>();
		private readonly IServicioRepositorioCatalogos servicioRepositorio;
		private readonly ServicioRepositorioCatalogos service;

		public Catalogos(IConfiguration configuration)
		{
			service = new ServicioRepositorioCatalogos(configuration);
		}

		[HttpPost]
		[Route("CargasDependencias")]
		// GET: RegistrarInhabilitadoController/Create
		public async Task<ActionResult> CargasDependencias()
		{
			try
			{
				List<Dependencia> listaDependencias = await service.ObtenerCatalogosDependencias();
				return Ok(listaDependencias);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[HttpPost]
		[Route("CargasCausas")]
		// GET: RegistrarInhabilitadoController/Create
		public async Task<ActionResult> CargasCausas()
		{
			try
			{
				List<CausaInhabilitacion> listaCausas = await service.ObtenerCatalogosCausas();
				return Ok(listaCausas);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[HttpPost]
		[Route("CargasOrigen")]
		// GET: RegistrarInhabilitadoController/Create
		public async Task<ActionResult> CargasOrigen()
		{
			try
			{
				List<OrigenInhabilitacion> listaOrigenes = await service.ObtenerCatalogosOrigen();
				return Ok(listaOrigenes);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[HttpPost]
		[Route("CargasMoneda")]
		// GET: RegistrarInhabilitadoController/Create
		public async Task<ActionResult> CargasMoneda()
		{
			try
			{
				List<Moneda> listaOrigenes = await service.ObtenerCatalogosMoneda();
				return Ok(listaOrigenes);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[HttpPost]
		[Route("CargasNivel")]
		// GET: RegistrarInhabilitadoController/Create
		public async Task<ActionResult> CargasNivel()
		{
			try
			{
				List<Nivel> listaOrigenes = await service.ObtenerCatalogosNivel();
				return Ok(listaOrigenes);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[HttpPost]
		[Route("CargasTipoFalta")]
		// GET: RegistrarInhabilitadoController/Create
		public async Task<ActionResult> CargasTipoFalta()
		{
			try
			{
				List<TipoFalta> listaOrigenes = await service.ObtenerCatalogosTipoFalta();
				return Ok(listaOrigenes);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[HttpPost]
		[Route("CargasTipoInhabilitacion")]
		// GET: RegistrarInhabilitadoController/Create
		public async Task<ActionResult> CargasTipoInhabilitacion()
		{
			try
			{
				List<TipoInhabilitacion> listaOrigenes = await service.ObtenerCatalogosTipoInhabilitacion();
				return Ok(listaOrigenes);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[HttpPost]
		[Route("CargasTipoSancion")]
		// GET: RegistrarInhabilitadoController/Create
		public async Task<ActionResult> CargasTipoSancion()
		{
			try
			{
				List<TipoSancion> listaOrigenes = await service.ObtenerCatalogosTipoSancion();
				return Ok(listaOrigenes);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
