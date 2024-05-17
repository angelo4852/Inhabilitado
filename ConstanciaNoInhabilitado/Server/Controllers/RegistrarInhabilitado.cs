using ConstanciaNoInhabilitado.Server.Servicios;
using ConstanciaNoInhabilitado.Shared.Entities.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstanciaNoInhabilitado.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrarInhabilitado : ControllerBase
    {

        private readonly ServicioRepositorio service;

        [HttpGet("[Action]")]
        [Route("SelectInhabilitadoUpdate")]
        // GET: RegistrarInhabilitadoController/Create
        public async Task<ServidorPublico> SelectInhabilitadoUpdate(int IdInhabilitado)
        {
            ServidorPublico _servidorPublico = new ServidorPublico();
            var respExiste = await service.SelectInhabilitadoUpdate(IdInhabilitado);


            return _servidorPublico;
        }

        
    }
}
