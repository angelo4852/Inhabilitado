using ConstanciaNoInhabilitado.Server.Interfaces;
using ConstanciaNoInhabilitado.Server.Servicios;
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
        public async Task<Inhabilitacion> CreateInhabilitacion(Inhabilitacion inhabilitacion)
        {
            try
            {
                var inhabilitacionResponde = await _service.AgregarInhabilitacion(inhabilitacion);
                return inhabilitacionResponde;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [Route("GetInhabilitado")]
        public async Task<Inhabilitado> GetInhabilitado(string rfc)
        {
            try
            {
                Inhabilitado inhabilitadoResponde = await _service.GetInhabilitado(rfc);
                return inhabilitadoResponde;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
