using ConstanciaNoInhabilitado.Server.Interfaces;
using ConstanciaNoInhabilitado.Shared.Entities;
using ConstanciaNoInhabilitado.Shared.Entities.Catalogos;
using ConstanciaNoInhabilitado.Shared.Entities.Login;
using ConstanciaNoInhabilitado.Shared.Entities.RegistroInhabilitacion;
using Dapper;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using static MudBlazor.CategoryTypes;

namespace ConstanciaNoInhabilitado.Server.Servicios
{
    public class ServicioRepositorioInhabilitacion : IServicioRepositorioInhabilitacion
    {
        private readonly string connectionString;
        public ServicioRepositorioInhabilitacion(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Inhabilitacion> AgregarInhabilitacion(Inhabilitacion Inhabilitacion)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);

                InhabilitacionDTO inhabilitacionDTO = new();

                inhabilitacionDTO = await ContruirInhabilitacionDTO(Inhabilitacion);

                string query = $@"INSERT INTO Inhabilitacion 
                                VALUES (@ExpedienteLegal, @FechaInicio, @FechaTermino, @Cargo, @Periodo, @FechaResolucion, @Autoridad, @Descripcion, @IdInhabilitado, @IdDependencia, @IdTipoInhabilitacion, 
                                @IdOrigenInhabilitacion, @IdCausaInhabilitacion, @IdUsuario, @FechaCreacion, @FechaModificacion, @Estatus, @EnProceso, @idTipoFalta, @OtroTipoFalta, @idTipoSancion, @OtroTipoSancion, @monto, 
                                @idMoneda, @siglas, @idNivel);";               

                var idInhabilitacion = await connection.QuerySingleAsync<int>($@"{query}", inhabilitacionDTO);

                Inhabilitacion.IdInhabilitacion = idInhabilitacion;
                return Inhabilitacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<InhabilitacionDTO> ContruirInhabilitacionDTO(Inhabilitacion inhabilitacion)
        {
            InhabilitacionDTO inhabilitacionDTO = new InhabilitacionDTO
            {
                //IdInhabilitacion = inhabilitacion.IdInhabilitacion,
                ExpedienteLegal = inhabilitacion.ExpedienteLegal,
                //FechaInicio = DateTime.Now.ToString("yyyy-mm-dd"),
                //FechaInicio = inhabilitacion.FechaInicio,
                //FechaTermino = inhabilitacion.FechaTermino,
                //FechaTermino = DateTime.Now.ToString("yyyy-mm-dd"),
                Cargo = inhabilitacion.Cargo,
                Periodo = inhabilitacion.Periodo,
                //FechaResolucion = inhabilitacion.FechaResolucion,
                Autoridad = inhabilitacion.Autoridad,
                Descripcion = inhabilitacion.Descripcion,
                IdInhabilitado = inhabilitacion.IdInhabilitado,
                IdDependencia = inhabilitacion.IdDependencia,
                IdTipoInhabilitacion = inhabilitacion.IdTipoInhabilitacion,
                IdOrigenInhabilitacion = inhabilitacion.IdOrigenInhabilitacion,
                IdCausaInhabilitacion = inhabilitacion.IdCausaInhabilitacion,
                IdUsuario = inhabilitacion.IdUsuario,               
                EnProceso = inhabilitacion.EnProceso                
            };

            return Task.FromResult(inhabilitacionDTO);
        }

        public async Task<Inhabilitado> GetInhabilitado(string RFC)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                string query = "SELECT * FROM Inhabilitado WHERE RFC = @RFC";
                var inhabilitado = await connection.QueryFirstOrDefaultAsync<Inhabilitado>(query, new { RFC });
                return inhabilitado;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

