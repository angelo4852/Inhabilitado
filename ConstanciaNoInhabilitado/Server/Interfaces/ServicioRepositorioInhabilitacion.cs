using ConstanciaNoInhabilitado.Shared.Entities.Catalogos;
using ConstanciaNoInhabilitado.Shared.Entities.Login;
using ConstanciaNoInhabilitado.Shared.Entities.RegistroInhabilitacion;

namespace ConstanciaNoInhabilitado.Server.Interfaces
{
    public interface IServicioRepositorioInhabilitacion
    {
        Task<InhabilitacionDTO> AgregarInhabilitacion(InhabilitacionDTO InhabilitacionDTO);       
        Task<Inhabilitado> GetInhabilitado(string RFC);
        Task<List<InhabilitacionADD>> GetInhabilitaciones();
        Task<InhabilitacionUpdate> ActualizarInhabilitacion(InhabilitacionUpdate InhabilitacionUpdate);
    }
}
