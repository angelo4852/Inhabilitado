using ConstanciaNoInhabilitado.Server.Interfaces;
using ConstanciaNoInhabilitado.Server.Servicios;
using ConstanciaNoInhabilitado.Shared.Entities.Catalogos;
using ConstanciaNoInhabilitado.Shared.Entities.Login;
using ConstanciaNoInhabilitado.Shared.Entities.RegistroInhabilitacion;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstanciaNoInhabilitado.Server.Controllers.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgregarInhabilitación : ControllerBase
    {
        private readonly IServicioRepositorioInhabilitacion _servicioRepositorio;
        private readonly ServicioRepositorioInhabilitacion _service;

        public AgregarInhabilitación(IConfiguration configuration)
        {
            _service = new ServicioRepositorioInhabilitacion(configuration);
        }

        [HttpPost]
        [Route("CreateInhabilitacion")]
        public async Task<InhabilitacionDTO> CreateInhabilitacion(InhabilitacionDTO inhabilitacionDTO)
        {
            try
            {
                var inhabilitacionResponde = await _service.AgregarInhabilitacion(inhabilitacionDTO);
                return inhabilitacionResponde;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return inhabilitacionDTO;
            }
        }

        [HttpGet]
        [Route("GetInhabilitado")]
        public async Task<Inhabilitado> GetInhabilitado(string rfc)
        {
            try
            {
                Inhabilitado inhabilitadoResponde = new();
                var inhabilitado = await _service.GetInhabilitado(rfc);
                if (inhabilitado is not null) inhabilitadoResponde = inhabilitado;
                return inhabilitadoResponde;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        [Route("GetInhabilitaciones")]
        // GET: RegistrarInhabilitadoController/Create
        public async Task<ActionResult> GetInhabilitaciones()
        {
            try
            {
                List<InhabilitacionADD> listaDependencias = await _service.GetInhabilitaciones();
                return Ok(listaDependencias);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("ActualizarInhabilitacion")]
        // GET: RegistrarInhabilitadoController/Create
        public async Task<ActionResult> ActualizarInhabilitacion(InhabilitacionUpdate InhabilitacionUpdate)
        {
            try
            {
                InhabilitacionUpdate inhabilitacionUpdateResponde = new();
                inhabilitacionUpdateResponde = await _service.ActualizarInhabilitacion(InhabilitacionUpdate);
                return Ok(inhabilitacionUpdateResponde);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
