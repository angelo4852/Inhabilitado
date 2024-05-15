using ConstanciaNoInhabilitado.Shared.Entities.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstanciaNoInhabilitado.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrarInhabilitado : ControllerBase
    {
        
       
        [HttpPost]
        [Route("Create")]
        // GET: RegistrarInhabilitadoController/Create
        public async Task<ActionResult> Create(ServidorPublico userTaxLogin)
        {

            return Ok();
        }

        
    }
}
