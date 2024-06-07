using ConstanciaNoInhabilitado.Shared.Entities.Reportes;
using ConstanciaNoInhabilitado.Shared.Entities.Login;
using Microsoft.AspNetCore.Mvc;

namespace ConstanciaNoInhabilitado.Server.Interfaces
{
    public interface IServicioRepositorioReportes
    {
        Task<List<ReportesConstanciasConsulta>> GetReportes(CriterioDeReportesConsulta criterioDeReportesConsulta);
        Task<List<ReportesConstanciasAltasoBajas>> GetReportesAltasoBajas(CriterioDeReportesConsulta criterioDeReportesConsulta);
    }
}
