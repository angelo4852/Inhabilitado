using ConstanciaNoInhabilitado.Server.Interfaces;
using ConstanciaNoInhabilitado.Server.Servicios;
using ConstanciaNoInhabilitado.Shared.Entities.CargaMasiva;
using ConstanciaNoInhabilitado.Shared.Entities.RegistroInhabilitacion;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstanciaNoInhabilitado.Server.Controllers.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargaMasivaArchivoExcel : ControllerBase
    {
        private readonly IServicioRepositorioCargaMasivaExcel _servicioRepositorio;
        private readonly ServicioRepositorioCargaMasivaExcel _service;

        public CargaMasivaArchivoExcel(IConfiguration configuration)
        {
            _service = new ServicioRepositorioCargaMasivaExcel(configuration);
        }

        [HttpPost]
        [Route("RegistrarCargaMasicaExcel")]
        public async Task<CargaMasivaExcelDTO> RegistrarCargaMasicaExcel(CargaMasivaExcelDTO cargaMasivaExcelDTO)
        {
            try
            {
                var CargaMasivaExcelDTOResponde = await _service.AgregarCargaArchivoExcel(cargaMasivaExcelDTO);
                return CargaMasivaExcelDTOResponde;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return cargaMasivaExcelDTO;
            }
        }
    }
}
