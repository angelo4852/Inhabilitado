using ConstanciaNoInhabilitado.Shared.Entities.CargaMasiva;

namespace ConstanciaNoInhabilitado.Client.Interfaces
{
    public interface IFileService
    {
        Task<CargaMasivaExcelDTO> ProcessFile(byte[] fileContent, int IdUsuario);
        Task<List<InhabilitadoCarga>> GetInhabilitado(List<CargaMasivaExcel> cargaMasivaExcels, int IdUsuario);
        Task<List<InhabilitacionCarga>> GetInhabilitacion(List<CargaMasivaExcel> cargaMasivaExcels);
    }
}
