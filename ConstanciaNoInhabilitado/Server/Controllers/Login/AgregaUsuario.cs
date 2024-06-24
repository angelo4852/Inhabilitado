using ConstanciaNoInhabilitado.Server.Servicios;
using ConstanciaNoInhabilitado.Shared.Entities.Catalogos;
using ConstanciaNoInhabilitado.Shared.Entities.Login;
using Microsoft.AspNetCore.Mvc;

namespace ConstanciaNoInhabilitado.Server.Controllers.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgregaUsuario : ControllerBase
    {
        private readonly ServicioRepositorioAgregaUsuario service;
        public AgregaUsuario(IConfiguration configuration)
        {
            service = new ServicioRepositorioAgregaUsuario(configuration);
        }
        [HttpPost]
        [Route("AgregaUsuarios")]
        public async Task<Usuarios> AgregaUsuarios(Usuarios _usuarios)
        {
            Usuarios reponseUsuarios = await service.AgregarUsuarios(_usuarios);

            return reponseUsuarios;


        }
    }
}
