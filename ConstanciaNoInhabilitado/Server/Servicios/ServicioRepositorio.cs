using ConstanciaNoInhabilitado.Shared.Entities;
using ConstanciaNoInhabilitado.Shared.Entities.Login;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace ConstanciaNoInhabilitado.Server.Servicios
{
    public interface IServicioRepositorio
    {
        Task<ServidorPublico> InsertarInhabilitado(ServidorPublico usuarioEntities);
        Task<UsuarioEntities> login(UserTaxLogin userTaxLogin);

    }
    public class ServicioRepositorio : IServicioRepositorio
    {
        private readonly string connectionString;
        public ServicioRepositorio(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");

        }

        public async Task<UsuarioEntities> login(UserTaxLogin userTaxLogin)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QueryFirstOrDefaultAsync(@"Select * from Usuario where 
                                                                Usuario= @Usuario AND Contrasena = @Contrasena;",
                                                                new { userTaxLogin.Usuario, userTaxLogin.Contrasena });
            var jsonResult = JsonConvert.SerializeObject(id);
            UsuarioEntities? resutadoInhabilitado = JsonConvert.DeserializeObject<UsuarioEntities>(jsonResult);
            return resutadoInhabilitado;
        }


        public async Task<ServidorPublico> SelectInhabilitadoUpdate(int IdInhabilitado)
        {
            using var connection = new SqlConnection(connectionString);
            var resp = await connection.QueryFirstOrDefaultAsync(@"Select * from Inhabilitado where 
                                                                IdInhabilitado = @IdInhabilitado ;",
                                                                new { IdInhabilitado });
            var jsonResult = JsonConvert.SerializeObject(IdInhabilitado);
            ServidorPublico? resutadoInhabilitado = JsonConvert.DeserializeObject<ServidorPublico>(jsonResult);
            return resutadoInhabilitado;
        }

        public async Task<bool> Existe(string nombre, string rfc)
        {

            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(
                                         @"SELECT 1
                                         FROM Inhabilitado WHERE  RFC = @RFC",
                                         new {  rfc }
                                         );
            return existe == 1;

        }

        public async Task<List<ServidorPublico>> SeletFrom()
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                var existe =  connection.Query("SELECT * FROM Inhabilitado INNER JOIN Genero ON Inhabilitado.idGenero = Genero.idGenero").ToList();
                var jsonResult = JsonConvert.SerializeObject(existe);
                List<ServidorPublico>? resutadoInhabilitado = JsonConvert.DeserializeObject<List<ServidorPublico>>(jsonResult);
                return resutadoInhabilitado;
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }

        public async Task<List<Sexo>> SelectGenero()
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                var existe = connection.Query("SELECT * from Genero").ToList();
                var jsonResult = JsonConvert.SerializeObject(existe);
                List<Sexo>? resutadoInhabilitado = JsonConvert.DeserializeObject<List<Sexo>>(jsonResult);
                return resutadoInhabilitado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<ServidorPublico> InsertarInhabilitado(ServidorPublico usuarioEntities)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                var id = await connection.QuerySingleAsync<int>($@"INSERT INTO Inhabilitado VALUES (@Nombre,@ApellidoPaterno,@ApellidoMaterno,
                                                                                @RFC,@CURP,@Tipo,@FechaCreacion,
                                                                                @FechaUltimaModificacion,
                                                                                @IdUsuario,@idGenero) 
                                                                                SELECT SCOPE_IDENTITY();", usuarioEntities);

                usuarioEntities.IdInhabilitado = id;
                //var jsonResult = JsonConvert.SerializeObject(id);
                //ServidorPublico? resutadoInhabilitado = JsonConvert.DeserializeObject<ServidorPublico>(jsonResult);
                //resutadoInhabilitado.IdInhabilitado = usuarioEntities.IdInhabilitado;
                return usuarioEntities;
            }
            catch (Exception e)
            {

                throw;
            }

        }

        public async Task UpdateInhabilitado(ServidorPublico usuarioEntities)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                var id = await connection.ExecuteAsync(@"UPDATE Inhabilitado SET Nombre = @Nombre, ApellidoPaterno = @ApellidoPaterno,
                                                                               ApellidoMaterno = @ApellidoMaterno, RFC = @RFC, CURP= @CURP, Tipo= @Tipo,
                                                                               FechaCreacion= @FechaCreacion, FechaUltimaModificacion= @FechaUltimaModificacion,
                                                                               IdUsuario= @IdUsuario, idGenero= @idGenero where IdInhabilitado = @IdInhabilitado", usuarioEntities);

                //var jsonResult = JsonConvert.SerializeObject(id);
                //ServidorPublico? resutadoInhabilitado = JsonConvert.DeserializeObject<ServidorPublico>(jsonResult);
                //resutadoInhabilitado.IdInhabilitado = usuarioEntities.IdInhabilitado;
            }
            catch (Exception e)
            {

                throw;
            }

        }


    }
}
