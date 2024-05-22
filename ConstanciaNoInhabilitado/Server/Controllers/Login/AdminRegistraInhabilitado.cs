using ConstanciaNoInhabilitado.Server.Servicios;
using ConstanciaNoInhabilitado.Shared.Entities.Login;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ConstanciaNoInhabilitado.Server.Controllers.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminRegistraInhabilitado : ControllerBase
    {
        private readonly IServicioRepositorio servicioRepositorio;
        private readonly ServicioRepositorio service;
        private ServidorPublico servidorPublicoBandera;

        public AdminRegistraInhabilitado(IConfiguration configuration)
        {
            //this.servicioRepositorio = servicioRepositorio;
            service = new ServicioRepositorio(configuration);
        }
        [HttpGet("[action]")]
        [Route("SelectInhabilitado")]        
        public async Task<List<ServidorPublico>> SelectInhabilitado()
        {
            List<ServidorPublico> servidorPublico = new();
            var respExiste =  service.SeletFrom();
            foreach (var item in respExiste.Result) 
            {
                servidorPublico.Add(item);
            }
            return servidorPublico;
        }

        [HttpPost]
        [Route("Create")]
        // GET: RegistrarInhabilitadoController/Create
        public async Task<ServidorPublico> Create(ServidorPublico userTaxLogin)
        {
            var respExiste = await service.Existe(userTaxLogin.Nombre, userTaxLogin.RFC);

            if (respExiste)
            {
                userTaxLogin.idBandera = 2;
                userTaxLogin.banderaExiste = false;
                userTaxLogin.IdInhabilitado = 0;
                return userTaxLogin;
            }
            else 
            {
                userTaxLogin.idBandera = 1;                
                userTaxLogin.IdUsuario = 4;
                userTaxLogin.Tipo = 1;
                var resp = service.InsertarInhabilitado(userTaxLogin);

                if (resp.Id > 0)
                {
                    var resultadp = resp.Result;
                    userTaxLogin.banderaExiste = true;
                }
                else
                {
                    servidorPublicoBandera.IdInhabilitado = userTaxLogin.IdInhabilitado;

                }                
            }
            
            return userTaxLogin;
        }


        [HttpPost]
        [Route("SelectInhabilitadoUpdate")]
        // GET: RegistrarInhabilitadoController/Create
        public async Task<ServidorPublico> SelectInhabilitadoUpdate(ServidorPublico servidorPublico)
        {
            ServidorPublico _servidorPublico = new ServidorPublico();
            //var respExiste = await service.SelectInhabilitadoUpdate(IdInhabilitado);
            return _servidorPublico;
        }

        [HttpGet("[action]")]
        [Route("SelectGenero")]
        public async Task<List<Sexo>> SelectGenero()
        {
            List<Sexo> servidorPublico = new();
            var respExiste = service.SelectGenero();
            foreach (var item in respExiste.Result)
            {
                servidorPublico.Add(item);
            }
            return servidorPublico;
        }


        [HttpPost]
        [Route("ActualizarInhabilitado")]
        // GET: RegistrarInhabilitadoController/Create
        public async Task<ServidorPublico> ActualizarInhabilitado(ServidorPublico servidorPublico)
        {
            ServidorPublico _servidorPublico = new ServidorPublico();


            var respExiste = await service.Existe(servidorPublico.Nombre, servidorPublico.RFC);

            if (respExiste)
            {
                servidorPublico.banderaExiste = false;
                servidorPublico.idBandera = 2;
                return servidorPublico;

            }
            else
            {
                var respUpdate = service.UpdateInhabilitado(servidorPublico);
                servidorPublico.idBandera = 1;

                if (respUpdate.IsCompletedSuccessfully)
                {
                    return _servidorPublico;

                }


            }


            //var respExiste = await service.SelectInhabilitadoUpdate(IdInhabilitado);
            return servidorPublico;
        }
    }
}
