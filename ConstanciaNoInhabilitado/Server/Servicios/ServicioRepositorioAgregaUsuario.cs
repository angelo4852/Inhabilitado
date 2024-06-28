using ConstanciaNoInhabilitado.Server.Interfaces;
using ConstanciaNoInhabilitado.Shared.Entities.Login;
using ConstanciaNoInhabilitado.Shared.Entities.RegistroInhabilitacion;
using ConstanciaNoInhabilitado.Shared.Entities.Reportes;
using Dapper;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Collections.Generic;

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

        public async Task<List<Usuarios>> GetInhabilitado()
        {
            try
            {
                using var connection = new SqlConnection(connectionString);

                var data = await connection.QueryAsync(@"  SELECT IdUsuario,Nombre,ApellidoPaterno,ApellidoMaterno,CorreoElectronico,Usuario,Contrasena,FechaCreacion,FechaModificacion,IdUsuarioModifica,Descripcion,Usuario.IdRolUsuario
                                                           FROM Usuario 
                                                           INNER JOIN RolUsuario ON Usuario.IdRolUsuario = RolUsuario.IdRolUsuario");
                var jsonResult = JsonConvert.SerializeObject(data);
                var TipoSancion = JsonConvert.DeserializeObject<List<Usuarios>>(jsonResult);
                return TipoSancion;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Usuarios> UpdateUsuarios(Usuarios _usuarios) 
        {
            return _usuarios;
		}
    }
}
