using ConstanciaNoInhabilitado.Server.Interfaces;
using ConstanciaNoInhabilitado.Shared.Entities.Login;
using Dapper;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace ConstanciaNoInhabilitado.Server.Servicios
{
    public class ServicioRepositorioAgregaUsuario : IServicioRepositorioAgregaUsuario
    {
        private readonly string connectionString;
        public ServicioRepositorioAgregaUsuario(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Usuarios> AgregarUsuarios(Usuarios _usuarios)
        {
            try
            {
                Usuarios ReportesExpedidas = new();
                using var connection = new SqlConnection(connectionString);
                string query = "USP_INSERTA_USUARIO";
                var data = await connection.QueryAsync<int>(query, _usuarios);
                foreach (var item in data)
                {
                    _usuarios.IdUsuario = item;
                }

                return _usuarios;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
