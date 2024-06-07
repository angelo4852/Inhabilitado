using ConstanciaNoInhabilitado.Server.Interfaces;
using ConstanciaNoInhabilitado.Shared.Entities.CargaMasiva;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;

namespace ConstanciaNoInhabilitado.Server.Servicios
{
    public class ServicioRepositorioCargaMasivaExcel : IServicioRepositorioCargaMasivaExcel
    {
        private readonly string connectionString;
        public ServicioRepositorioCargaMasivaExcel(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }
        public async Task<CargaMasivaExcelDTO> AgregarCargaArchivoExcel(CargaMasivaExcelDTO cargaMasivaExcelDTO)
        {
            try
            {
                var respondeValidado = await ValidarInformacion(cargaMasivaExcelDTO);
                var responde = await GetInhabilitado(cargaMasivaExcelDTO.CargaMasivaExcel.Where(c => c.FaltaInformacion == false).ToList());
                responde.ErroresDeLaCarga.AddRange(respondeValidado.ErroresDeLaCarga);
                responde.CargaMasivaExcel.AddRange(respondeValidado.CargaMasivaExcel.Where(c => c.FaltaInformacion == true).ToList());
                return responde;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private async Task<CargaMasivaExcelDTO> ValidarInformacion(CargaMasivaExcelDTO cargaMasivaExcelDTO)
        {
            try
            {
                int cell = 7;
                foreach (var item in cargaMasivaExcelDTO.CargaMasivaExcel)
                {
                    if (item.Dependencia is null || item.Dependencia == string.Empty) { item.FaltaInformacion = true; cargaMasivaExcelDTO.ErroresDeLaCarga.Add($" Falta información en la columna Dependencia en la fila #{cargaMasivaExcelDTO.CargaMasivaExcel.IndexOf(item) + cell}"); }
                    if (item.RFC is null || item.RFC == string.Empty) { item.FaltaInformacion = true; cargaMasivaExcelDTO.ErroresDeLaCarga.Add($" Falta información en la columna RFC en la fila #{cargaMasivaExcelDTO.CargaMasivaExcel.IndexOf(item) + cell}"); }
                    if (item.Homo is null || item.Homo == string.Empty) { item.FaltaInformacion = true; cargaMasivaExcelDTO.ErroresDeLaCarga.Add($" Falta información en la columna Homo en la fila #{cargaMasivaExcelDTO.CargaMasivaExcel.IndexOf(item) + cell}"); }
                    if (item.ApellidoPaterno is null || item.ApellidoPaterno == string.Empty) { item.FaltaInformacion = true; cargaMasivaExcelDTO.ErroresDeLaCarga.Add($" Falta información en la columna Apellido Paterno en la fila #{cargaMasivaExcelDTO.CargaMasivaExcel.IndexOf(item) + cell}"); }
                    if (item.ApellidoMaterno is null || item.ApellidoMaterno == string.Empty) { item.FaltaInformacion = true; cargaMasivaExcelDTO.ErroresDeLaCarga.Add($" Falta información en la columna Apellido Materno en la fila #{cargaMasivaExcelDTO.CargaMasivaExcel.IndexOf(item) + cell}"); }
                    if (item.Nombre is null || item.Nombre == string.Empty) { item.FaltaInformacion = true; cargaMasivaExcelDTO.ErroresDeLaCarga.Add($" Falta información en la columna Nombre en la fila #{cargaMasivaExcelDTO.CargaMasivaExcel.IndexOf(item) + cell}"); }
                    if (item.AutoridadSancionadora is null || item.AutoridadSancionadora == string.Empty) { item.FaltaInformacion = true; cargaMasivaExcelDTO.ErroresDeLaCarga.Add($" Falta información en la columna Autoridad Sancionadora en la fila #{cargaMasivaExcelDTO.CargaMasivaExcel.IndexOf(item) + cell}"); }
                    if (item.Puesto is null || item.Puesto == string.Empty) { item.FaltaInformacion = true; cargaMasivaExcelDTO.ErroresDeLaCarga.Add($" Falta información en la columna Puesto en la fila #{cargaMasivaExcelDTO.CargaMasivaExcel.IndexOf(item) + cell}"); }
                    if (item.Periodo is null || item.Periodo == string.Empty) { item.FaltaInformacion = true; cargaMasivaExcelDTO.ErroresDeLaCarga.Add($" Falta información en la columna Periodo en la fila #{cargaMasivaExcelDTO.CargaMasivaExcel.IndexOf(item) + cell}"); }
                    if (item.FechaResolucion is null) { item.FaltaInformacion = true; cargaMasivaExcelDTO.ErroresDeLaCarga.Add($" Falta información en la columna Fecha Resolucion en la fila #{cargaMasivaExcelDTO.CargaMasivaExcel.IndexOf(item) + cell}"); }
                    if (item.FechaNotificacion is null) { item.FaltaInformacion = true; cargaMasivaExcelDTO.ErroresDeLaCarga.Add($" Falta información en la columna Fecha Notificacion en la fila #{cargaMasivaExcelDTO.CargaMasivaExcel.IndexOf(item) + cell}"); }
                    if (item.FechaInicio is null) { item.FaltaInformacion = true; cargaMasivaExcelDTO.ErroresDeLaCarga.Add($" Falta información en la columna Fecha Inicio en la fila #{cargaMasivaExcelDTO.CargaMasivaExcel.IndexOf(item) + cell}"); }
                    if (item.FechaFin is null) { item.FaltaInformacion = true; cargaMasivaExcelDTO.ErroresDeLaCarga.Add($" Falta información en la columna Fecha Fin en la fila #{cargaMasivaExcelDTO.CargaMasivaExcel.IndexOf(item) + cell}"); }
                }
                return cargaMasivaExcelDTO;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<CargaMasivaExcelDTO> GetInhabilitado(List<CargaMasivaExcel> cargaMasivaExcels)
        {
            CargaMasivaExcelDTO cargaMasivaExcelDTO = new();
            InhabilitadoCarga inhabilitadoCargaErrores = new();
            try
            {
                string query = "SELECT count(IdInhabilitado) FROM Inhabilitado WHERE Nombre = @NOMB and ApellidoPaterno = @AP_PAT and ApellidoMaterno = @AP_MAT and RFC = @R_F_C and Tipo = @TIP;";
                List<InhabilitadoCarga> inhabilitadoCargas = new();
                List<InhabilitacionCarga> inhabilitacionCarga = new();
                foreach (var carga in cargaMasivaExcels)
                {
                    try
                    {
                        using var connection = new SqlConnection(connectionString);
                        connection.Open();
                        InhabilitadoCarga inhabilitadoCarga = new InhabilitadoCarga
                        {
                            NOMB = carga.Nombre!.Trim(),
                            AP_PAT = carga.ApellidoPaterno!.Trim(),
                            AP_MAT = carga.ApellidoMaterno!.Trim(),
                            R_F_C = $"{carga.RFC!.Trim()}{carga.Homo!.Trim()}",
                            USU = carga.IdUsuario,
                            TIP = 1
                        };

                        var resp = connection.QueryFirstOrDefault<int>(query, new { inhabilitadoCarga.NOMB, inhabilitadoCarga.AP_PAT, inhabilitadoCarga.AP_MAT, inhabilitadoCarga.R_F_C, inhabilitadoCarga.TIP });                        

                        if (resp > 0)
                        {
                            string queryBuscaID = "SELECT TOP (1) IdInhabilitado FROM Inhabilitado WHERE Nombre=@NOMB and @AP_PAT=ApellidoPaterno and @AP_MAT=ApellidoMaterno and @R_F_C=RFC and Tipo=@TIP;";
                            inhabilitadoCarga.IdInhabilitado = connection.QueryFirstOrDefault<int>(queryBuscaID, new { inhabilitadoCarga.NOMB, inhabilitadoCarga.AP_PAT, inhabilitadoCarga.AP_MAT, inhabilitadoCarga.R_F_C, inhabilitadoCarga.TIP });
                            inhabilitadoCarga.SeInserto = false;
                        }
                        else
                        {
                            string queryBuscaIDSoloPorRfc = "SELECT IdInhabilitado FROM Inhabilitado Where RFC = @R_F_C";
                            inhabilitadoCarga.IdInhabilitado = connection.QueryFirstOrDefault<int>(queryBuscaIDSoloPorRfc, new { inhabilitadoCarga.R_F_C });
                            inhabilitadoCarga.SeInserto = false;
                            if (inhabilitadoCarga.IdInhabilitado == 0) 
                            {
                                string queryInsertarId = "INSERT INTO Inhabilitado (Nombre,ApellidoPaterno, ApellidoMaterno,RFC, Tipo, fechaCreacion, fechaUltimaModificacion, IdUsuario) values (@NOMB,@AP_PAT,@AP_MAT,@R_F_C,@TIP,GetDate(),GetDate(),@USU); SELECT SCOPE_IDENTITY();";
                                inhabilitadoCarga.IdInhabilitado = connection.QueryFirstOrDefault<int>(queryInsertarId, new { inhabilitadoCarga.NOMB, inhabilitadoCarga.AP_PAT, inhabilitadoCarga.AP_MAT, inhabilitadoCarga.R_F_C, inhabilitadoCarga.TIP, inhabilitadoCarga.USU });
                                inhabilitadoCarga.SeInserto = true;
                            }
                        }

                        inhabilitadoCargas.Add(inhabilitadoCarga);
                        inhabilitadoCargaErrores = inhabilitadoCarga;

                        if (inhabilitadoCarga.IdInhabilitado > 0)
                        {
                            var responde = await InhabilitacionCarga(carga, inhabilitadoCarga);
                            inhabilitacionCarga.Add(responde);
                        }
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        cargaMasivaExcelDTO.ErroresDeLaCarga.Add($"Error: RFC: {inhabilitadoCargaErrores.R_F_C}, {inhabilitadoCargaErrores.IdInhabilitado}, {ex.Message}");
                    }
                }

                cargaMasivaExcelDTO.CargaMasivaExcel.AddRange(cargaMasivaExcels);
                cargaMasivaExcelDTO.InhabilitadoCarga.AddRange(inhabilitadoCargas);
                cargaMasivaExcelDTO.InhabilitacionCarga.AddRange(inhabilitacionCarga);

                return cargaMasivaExcelDTO;
            }
            catch (Exception ex)
            {
                cargaMasivaExcelDTO.ErroresDeLaCarga.Add($"Error general: {inhabilitadoCargaErrores.R_F_C}, {ex}");
            }
            return cargaMasivaExcelDTO;
        }

        public async Task<InhabilitacionCarga> InhabilitacionCarga(CargaMasivaExcel cargaMasivaExcels, InhabilitadoCarga inhabilitadoCarga)
        {
            try
            {
                using var connectionInhabilitacionCarga = new SqlConnection(connectionString);
                connectionInhabilitacionCarga.Open();
                //string query = "USP_CARGA_INHABILITACION";
                string queryInhabilitacionCarga = $"SELECT COUNT(IdInhabilitacionFederacion) FROM InhabilitacionFederacion WHERE Dependencia = @DEPENDENCIA AND AutoridadSancionadora=@AUTORIDAD AND Cargo = @CARGO AND PERIODO = @PERIODO AND FECHARESOLUCION=CONVERT(date,@FECHARESOLUCION,105) AND FECHANOTIFICACION = CONVERT(date,@FECHANOTIFICACION,105) AND FECHANOTIFICACION = CONVERT(date,@FECHANOTIFICACION,105) AND FECHAINICIO = CONVERT(date,@FECHAINICIO,105) AND FECHAFIN=CONVERT(date,@FECHAFIN,105) 	AND IDINHABILITADO = @IDINHABILITADO AND IDUSUARIO=@IDUSUARIO;";

                InhabilitacionCarga inhabilitacionCarga = new InhabilitacionCarga
                {
                    Dependencia = cargaMasivaExcels.Dependencia,
                    Autoridad = cargaMasivaExcels.AutoridadSancionadora,
                    Cargo = cargaMasivaExcels.Puesto,
                    Periodo = cargaMasivaExcels.Periodo,
                    FechaResolucion = await ConvertirFecha(cargaMasivaExcels.FechaResolucion),
                    FechaNotificacion = await ConvertirFecha(cargaMasivaExcels.FechaNotificacion),
                    FechaInicio = await ConvertirFecha(cargaMasivaExcels.FechaInicio),
                    FechaFin = await ConvertirFecha(cargaMasivaExcels.FechaFin),
                    IdInhabilitado = inhabilitadoCarga.IdInhabilitado,
                    IdUsuario = cargaMasivaExcels.IdUsuario
                };

                var responde = await connectionInhabilitacionCarga.QuerySingleAsync<int>($@"{queryInhabilitacionCarga}", new
                {
                    inhabilitacionCarga.Dependencia,
                    inhabilitacionCarga.Autoridad,
                    inhabilitacionCarga.Cargo,
                    inhabilitacionCarga.Periodo,
                    inhabilitacionCarga.FechaResolucion,
                    inhabilitacionCarga.FechaNotificacion,
                    inhabilitacionCarga.FechaInicio,
                    inhabilitacionCarga.FechaFin,
                    inhabilitacionCarga.IdInhabilitado,
                    inhabilitacionCarga.IdUsuario
                });
                if (responde > 0)
                {
                    string queryBuscar = "SELECT TOP 1 Count(IdInhabilitacionFederacion) FROM InhabilitacionFederacion WHERE Dependencia = @DEPENDENCIA AND AutoridadSancionadora=@AUTORIDAD AND Cargo = @CARGO AND PERIODO = @PERIODO AND FECHARESOLUCION=CONVERT(date,@FECHARESOLUCION,105) AND FECHANOTIFICACION = CONVERT(date,@FECHANOTIFICACION,105) AND FECHANOTIFICACION = CONVERT(date,@FECHANOTIFICACION,105) AND FECHAINICIO = CONVERT(date,@FECHAINICIO,105) AND FECHAFIN=CONVERT(date,@FECHAFIN,105) AND IDINHABILITADO = @IDINHABILITADO AND IDUSUARIO=@IDUSUARIO";
                    var idInhabilitacionResponde = await connectionInhabilitacionCarga.QuerySingleAsync<int>($@"{queryBuscar}", new
                    {
                        inhabilitacionCarga.Dependencia,
                        inhabilitacionCarga.Autoridad,
                        inhabilitacionCarga.Cargo,
                        inhabilitacionCarga.Periodo,
                        inhabilitacionCarga.FechaResolucion,
                        inhabilitacionCarga.FechaNotificacion,
                        inhabilitacionCarga.FechaInicio,
                        inhabilitacionCarga.FechaFin,
                        inhabilitacionCarga.IdInhabilitado,
                        inhabilitacionCarga.IdUsuario
                    });
                    inhabilitacionCarga.IdInhabilitacion = idInhabilitacionResponde;
                    inhabilitacionCarga.SeInserto = false;
                }
                else
                {
                    string queryInsertar = "INSERT INTO InhabilitacionFederacion (Dependencia,AutoridadSancionadora,Cargo,Periodo, FechaResolucion,FechaNotificacion,FechaInicio,FechaFin, FechaCreacion,FechaModificacion,IdInhabilitado,IdUsuario) VALUES(@DEPENDENCIA,@AUTORIDAD,@CARGO,@PERIODO, CONVERT(date,@FECHARESOLUCION,105),CONVERT(date,@FECHANOTIFICACION,105),CONVERT(date,@FECHAINICIO,105),CONVERT(date,@FECHAFIN,105), GETDATE(), GETDATE(),@IDINHABILITADO,@IDUSUARIO); SELECT SCOPE_IDENTITY();";
                    var idInhabilitacionResponde = await connectionInhabilitacionCarga.QuerySingleAsync<int>($@"{queryInsertar}", new
                    {
                        inhabilitacionCarga.Dependencia,
                        inhabilitacionCarga.Autoridad,
                        inhabilitacionCarga.Cargo,
                        inhabilitacionCarga.Periodo,
                        inhabilitacionCarga.FechaResolucion,
                        inhabilitacionCarga.FechaNotificacion,
                        inhabilitacionCarga.FechaInicio,
                        inhabilitacionCarga.FechaFin,
                        inhabilitacionCarga.IdInhabilitado,
                        inhabilitacionCarga.IdUsuario
                    });
                    inhabilitacionCarga.IdInhabilitacion = idInhabilitacionResponde;
                    inhabilitacionCarga.SeInserto = true;
                }
                connectionInhabilitacionCarga.Close();
                return inhabilitacionCarga;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        private async Task<string> ConvertirFecha(DateTime? fecha)
        {
            string fechaConvertida = string.Empty;
            DateTime dateTime = (DateTime)fecha!;
            fechaConvertida = dateTime.ToString("dd/MM/yyyy");
            return fechaConvertida;
        }
    }
}
