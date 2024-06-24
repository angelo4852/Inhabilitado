using ConstanciaNoInhabilitado.Shared.Entities.CargaMasiva;
using ConstanciaNoInhabilitado.Shared.Entities.Constancia;

namespace ConstanciaNoInhabilitado.Server.Interfaces
{
    public interface IServicioRepositorioConstancia
    {
        Task<List<ConstanciaInhabilitado>> GetConstancia(ConstanciaBusqueda constanciaBusqueda);
    }
}
