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

        public async Task<InhabilitacionDTO> AgregarInhabilitacion(InhabilitacionDTO inhabilitacionDTO)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);               

                string query = $@"INSERT INTO Inhabilitacion VALUES (@ExpedienteLegal, @FechaInicio, @FechaTermino, @Cargo, @Periodo, @FechaResolucion, @Autoridad, @Descripcion, @IdInhabilitado, @IdDependencia, @IdTipoInhabilitacion, 
                                @IdOrigenInhabilitacion, @IdCausaInhabilitacion, @IdUsuario, @FechaCreacion, @FechaModificacion, @Estatus, @EnProceso, @IdTipoFalta, @OtroTipoFalta, @IdTipoSancion, @OtroTipoSancion, @Monto, 
                                @IdMoneda, @Siglas, @IdNivel)SELECT SCOPE_IDENTITY();";               

                var idInhabilitacion = await connection.QuerySingleAsync<int>($@"{query}", inhabilitacionDTO);

                inhabilitacionDTO.IdInhabilitacion = idInhabilitacion;
                return inhabilitacionDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return inhabilitacionDTO;
            }
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
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<InhabilitacionADD>> GetInhabilitaciones()
        {
            try
            {
                List<InhabilitacionADD> inhabilitacionBD = new();
                using var connection = new SqlConnection(connectionString);
                string query = @"
                        SELECT 
                              i.[IdInhabilitacion],i.[ExpedienteLegal],i.[FechaInicio], i.[FechaTermino],i.[Cargo],i.[Periodo], i.[FechaResolucion], i.[Autoridad], i.[Descripcion], inh.[Nombre] AS NombreInhabilitado,
                              inh.[ApellidoPaterno] AS ApellidoPaternoInhabilitado,inh.[ApellidoMaterno] AS ApellidoMaternoInhabilitado, inh.[RFC], inh.[Tipo], d.[Descripcion] AS Dependencia, ti.[Descripcion] AS TipoInhabilitacion,
                              oi.[Descripcion] AS OrigenInhabilitacion, ci.[Descripcion] AS CausaInhabilitacion, CONCAT(u.[Nombre], ' ', u.[ApellidoPaterno], ' ', u.[ApellidoMaterno]) AS NombreCompletoUsuario, i.[FechaCreacion],
                              i.[FechaModificacion], i.[Estatus], i.[EnProceso],tf.[tipoFalta_valor] AS TipoFalta,i.[OtroTipoFalta],ts.[tipoSancion_valor] AS TipoSancion,i.[OtroTipoSancion],i.[monto],m.[moneda_valor] AS Moneda,i.[siglas],n.[Descripcion] AS Nivel
                        FROM [ConstanciaInhabilitado].[dbo].[Inhabilitacion] i
                        JOIN [ConstanciaInhabilitado].[dbo].[Dependencia] d ON i.[IdDependencia] = d.[IdDependencia]
                        JOIN [ConstanciaInhabilitado].[dbo].[TipoInhabilitacion] ti ON i.[IdTipoInhabilitacion] = ti.[IdTipoInhabilitacion]
                        JOIN [ConstanciaInhabilitado].[dbo].[OrigenInhabilitacion] oi ON i.[IdOrigenInhabilitacion] = oi.[IdOrigenInhabilitacion]
                        JOIN [ConstanciaInhabilitado].[dbo].[CausaInhabilitacion] ci ON i.[IdCausaInhabilitacion] = ci.[IdCausaInhabilitacion]
                        JOIN [ConstanciaInhabilitado].[dbo].[TipoFalta] tf ON i.[idTipoFalta] = tf.[IdTipoFalta]
                        JOIN [ConstanciaInhabilitado].[dbo].[TipoSancion] ts ON i.[idTipoSancion] = ts.[IdTipoSancion]
                        JOIN [ConstanciaInhabilitado].[dbo].[Moneda] m ON i.[idMoneda] = m.[IdMoneda]
                        JOIN [ConstanciaInhabilitado].[dbo].[Nivel] n ON i.[idNivel] = n.[IdNivel]
                        JOIN [ConstanciaInhabilitado].[dbo].[Inhabilitado] inh ON i.[IdInhabilitado] = inh.[IdInhabilitado]
                        JOIN [ConstanciaInhabilitado].[dbo].[Usuario] u ON i.[IdUsuario] = u.[IdUsuario];
                        ";
                var data = await connection.QueryAsync(query);

                var jsonResult = JsonConvert.SerializeObject(data);
                var causa = JsonConvert.DeserializeObject<List<InhabilitacionADD>>(jsonResult);
                inhabilitacionBD = causa!;
                return inhabilitacionBD;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<InhabilitacionUpdate> ActualizarInhabilitacion(InhabilitacionUpdate InhabilitacionUpdate)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                string query = "UPDATE [dbo].[Inhabilitacion] SET [ExpedienteLegal] = @ExpedienteLegal ,[FechaInicio] = @FechaInicio  ,[FechaTermino] = @FechaTermino  ,[Cargo] = Cargo  ,[Periodo] = @Periodo ," +
                               "[FechaResolucion] = @FechaResolucion ,[Autoridad] = @Autoridad  ,[Descripcion] = @Descripcion ,[IdDependencia] = @Dependencia ,[IdTipoInhabilitacion] = @TipoInhabilitacion ," +
                               "[IdOrigenInhabilitacion] = @OrigenInhabilitacion,[IdCausaInhabilitacion] = @CausaInhabilitacion ,[FechaModificacion] = @FechaModificacion WHERE [IdInhabilitacion] = @IdInhabilitacion";

                var responde = await connection.ExecuteAsync(query,InhabilitacionUpdate);

                if (responde > 0) InhabilitacionUpdate.RespondeUdpate = true;

                return InhabilitacionUpdate;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

