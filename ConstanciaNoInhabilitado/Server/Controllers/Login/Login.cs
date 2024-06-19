using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ConstanciaNoInhabilitado.Shared.Entities.Login;
using Microsoft.Data.SqlClient;
using Dapper;
using ConstanciaNoInhabilitado.Shared.Entities;
using Newtonsoft.Json;
using ConstanciaNoInhabilitado.Server.Servicios;

namespace ConstanciaNoInhabilitado.Server.Controllers.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class Login : ControllerBase
    {
        private readonly string connectionString;
        List<UsuarioEntities> InhabilitadoList = new List<UsuarioEntities>();
        private readonly IServicioRepositorio servicioRepositorio;
        private readonly ServicioRepositorio service;

        public Login(IConfiguration configuration)
        {
            //connectionString = configuration.GetConnectionString("DefaultConnection");
            //servicioRepositorio = IservicioRepositorio;
            service = new ServicioRepositorio(configuration);

        }

        [HttpPost]
        [Route("ConsultaUsuario")]
        public async Task<IActionResult> ConsultaUsuario(UserTaxLogin userTaxLogin)
        {
            Session session = new Session();
            var resp = await service.login(userTaxLogin);
            if (resp != null)
            {
                session.IdUser = (int)resp.IdUsuario!;
                session.User = resp.Nombre;
                session.Correo = resp.CorreoElectronico;
                session.Rol = (string)resp.IdRolUsuario;
            }
            else
            {
                session.User = $"El usuario: {userTaxLogin.Usuario} no existe";
                session.Rol = "0";
            }
            return StatusCode(StatusCodes.Status200OK, session);
        }

        [HttpPost]
        [Route("DescargarConstancia")]
        public async Task<ActionResult> DescargarConstancia(SearchReference searchReference)
        {
            SearchReferenceReponse searchReferenceReponse = new();

            if (searchReference.Reference == "12420000727841991212")
            {
                searchReferenceReponse.ReferenceSearch = "http://testappdstnet.puebla.gob.mx/api/Divers/GetMethodPaymentWindow?referenceNumber=12420000727841991212";
            }

            return StatusCode(StatusCodes.Status200OK, searchReferenceReponse);
        }
    }
}
