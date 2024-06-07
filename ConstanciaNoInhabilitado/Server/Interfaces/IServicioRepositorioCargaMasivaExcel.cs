using ConstanciaNoInhabilitado.Shared.Entities.CargaMasiva;
using ConstanciaNoInhabilitado.Shared.Entities.RegistroInhabilitacion;

namespace ConstanciaNoInhabilitado.Server.Interfaces
{
    public interface IServicioRepositorioCargaMasivaExcel
    {
        Task<CargaMasivaExcelDTO> AgregarCargaArchivoExcel(CargaMasivaExcelDTO cargaMasivaExcelDTO);
    }
}
