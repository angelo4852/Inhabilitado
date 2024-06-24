using ConstanciaNoInhabilitado.Shared.Entities.CargaMasiva;

namespace ConstanciaNoInhabilitado.Client.Interfaces
{
    public interface IFileService
    {
        Task<CargaMasivaExcelDTO> ProcessFile(byte[] fileContent, int IdUsuario);
        
    }
}
