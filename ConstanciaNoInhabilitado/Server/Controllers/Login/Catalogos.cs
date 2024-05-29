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
       

        [HttpPost]
        [Route("ActualizarDependencia")]
        // GET: RegistrarInhabilitadoController/Create
        public async Task<ActionResult> ActualizarDependencia(Dependencia causasInhabilitacion)
        {
            try
            {
                Dependencia listaOrigenes = await service.UpdateDependencia(causasInhabilitacion);              
                return Ok(listaOrigenes);
			}
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("RegistrarDependencia")]
        // GET: RegistrarInhabilitadoController/Create
        public async Task<ActionResult> RegistrarDependencia(Dependencia registroDependencia)
        {
            try
            {
                Dependencia listaOrigenes = await service.RegistrarDependencia(registroDependencia);

				if (listaOrigenes.IdDependencia > 0)
				{
					listaOrigenes.idBandera = 1;

				}
				else 
				{
					listaOrigenes.idBandera = 2;
				
				}
                return Ok(listaOrigenes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("RegistrarCausa")]
        public async Task<ActionResult> RegistrarCausa(CausasInhabilitacion causaInhabilitacion) 
		{
            CausasInhabilitacion causaInhabilitacion1 = await service.RegistrarCausa(causaInhabilitacion);

            if (causaInhabilitacion1.IdCausaInhabilitacion > 0)
            {
                causaInhabilitacion1.idBandera = 1;

            }
            else
            {
                causaInhabilitacion1.idBandera = 2;

            }
            return Ok(causaInhabilitacion1);
        }

        [HttpPost]
        [Route("ActualizarCausa")]
        // GET: RegistrarInhabilitadoController/Create
        public async Task<ActionResult> ActualizarCausa(CausasInhabilitacion causasInhabilitacion)
        {
            try
            {
                CausasInhabilitacion listaOrigenes = await service.UpdateCausa(causasInhabilitacion);
                return Ok(listaOrigenes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        [Route("RegistraOrigen")]
        public async Task<ActionResult> RegistraOrigen(OrigenesInhabilitacion causaInhabilitacion)
        {
            OrigenesInhabilitacion causaInhabilitacion1 = await service.RegistraOrigen(causaInhabilitacion);

            if (causaInhabilitacion1.IdOrigenInhabilitacion > 0)
            {
                causaInhabilitacion1.idBandera = 1;

            }
            else
            {
                causaInhabilitacion1.idBandera = 2;

            }
            return Ok(causaInhabilitacion1);
        }

        [HttpPost]
        [Route("ActualizarOrigen")]
        // GET: RegistrarInhabilitadoController/Create
        public async Task<ActionResult> ActualizarOrigen(OrigenesInhabilitacion origenInhabilitacion)
        {
            try
            {
                OrigenesInhabilitacion listaOrigenes = await service.UpdateOrigen(origenInhabilitacion);
                return Ok(listaOrigenes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
