using ConstanciaNoInhabilitado.Server.Interfaces;
using ConstanciaNoInhabilitado.Shared.Entities.CargaMasiva;
using ConstanciaNoInhabilitado.Shared.Entities.Constancia;
using ConstanciaNoInhabilitado.Shared.Entities.Reportes;
using Dapper;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace ConstanciaNoInhabilitado.Server.Servicios
{
    public class ServicioRepositorioConstancia : IServicioRepositorioConstancia
    {
        private readonly string connectionString;
        public ServicioRepositorioConstancia(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }    

        public async Task<List<ConstanciaInhabilitado>> GetConstancia(ConstanciaBusqueda constanciaBusqueda)
        {
			try
			{
                List<ConstanciaInhabilitado> ConstanciaExpedidas = new();
                using var connection = new SqlConnection(connectionString);
                string query = "USP_GET_CONSTANCIA";

                var data = await connection.QueryAsync(query, constanciaBusqueda);

                var jsonResult = JsonConvert.SerializeObject(data);
                ConstanciaExpedidas = JsonConvert.DeserializeObject<List<ConstanciaInhabilitado>>(jsonResult)!;

                return ConstanciaExpedidas;
            }
			catch (Exception)
			{

				throw;
			}
        }
    }
}
