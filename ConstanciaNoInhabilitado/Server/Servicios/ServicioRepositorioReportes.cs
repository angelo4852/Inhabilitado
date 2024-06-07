using ConstanciaNoInhabilitado.Server.Interfaces;
using ConstanciaNoInhabilitado.Shared.Entities.Reportes;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Dapper;

namespace ConstanciaNoInhabilitado.Server.Servicios
{
    public class ServicioRepositorioReportes : IServicioRepositorioReportes
    {
        private readonly string connectionString;
        public ServicioRepositorioReportes(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<ReportesConstanciasConsulta>> GetReportes(CriterioDeReportesConsulta criterioDeReportesConsulta)
        {
			try
			{
                List<ReportesConstanciasConsulta> ReportesExpedidas = new();
                using var connection = new SqlConnection(connectionString);
                string query = "USP_GET_REPORTE";               

                var data = await connection.QueryAsync(query, criterioDeReportesConsulta);

                var jsonResult = JsonConvert.SerializeObject(data);
                ReportesExpedidas = JsonConvert.DeserializeObject<List<ReportesConstanciasConsulta>>(jsonResult);  
                
                return ReportesExpedidas;
            }
			catch (Exception ex)
			{
                Console.WriteLine(ex.Message);
				throw;
			}
        }

        public async Task<List<ReportesConstanciasAltasoBajas>> GetReportesAltasoBajas(CriterioDeReportesConsulta criterioDeReportesConsulta)
        {
            try
            {
                List<ReportesConstanciasAltasoBajas> ReportesAltasoBajas = new();
                using var connection = new SqlConnection(connectionString);
                string query = "USP_GET_REPORTE";

                var data = await connection.QueryAsync(query, criterioDeReportesConsulta);

                var jsonResult = JsonConvert.SerializeObject(data);
                ReportesAltasoBajas = JsonConvert.DeserializeObject<List<ReportesConstanciasAltasoBajas>>(jsonResult);

                return ReportesAltasoBajas;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
