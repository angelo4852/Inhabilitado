using ConstanciaNoInhabilitado.Server.Interfaces;
using ConstanciaNoInhabilitado.Server.Servicios;
using ConstanciaNoInhabilitado.Shared.Entities.Constancia;
using Microsoft.AspNetCore.Mvc;

namespace ConstanciaNoInhabilitado.Server.Controllers.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class Constancias : ControllerBase
    {
        private readonly ServicioRepositorioConstancia service;
        private readonly IServicioRepositorioConstancia servicioRepositorio;

        public Constancias(IConfiguration configuration)
        {
            service = new ServicioRepositorioConstancia(configuration);
        }

        [HttpPost]
        [Route("GetReportes")]
        public async Task<ActionResult> GetReportes(ConstanciaBusqueda constanciaBusqueda)
        {
            try
            {
                List<ConstanciaInhabilitado> ReportesExpedidas = await service.GetConstancia(constanciaBusqueda);
                return Ok(ReportesExpedidas);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
