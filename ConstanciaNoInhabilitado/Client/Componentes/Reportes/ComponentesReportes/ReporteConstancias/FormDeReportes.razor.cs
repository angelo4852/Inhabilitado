using ConstanciaNoInhabilitado.Client.Shared.Partial.Dialogs;
using ConstanciaNoInhabilitado.Shared.Entities.Login;
using ConstanciaNoInhabilitado.Shared.Entities.Reportes;
using iText.StyledXmlParser.Node;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using System.Net.Http.Json;

namespace ConstanciaNoInhabilitado.Client.Componentes.Reportes.ComponentesReportes.ReporteConstancias
{
    partial class FormDeReportes
    {
        [Parameter] public bool ReporteAltasBajas { set; get; }
        private CriterioDeReportes CriterioDeReportes { set; get; } = new();
        private List<string> ErroresEncontrados { set; get; } = new();
        private List<ReportesConstanciasConsulta> ReportesConstancias { set; get; } = new();
        private List<ReportesConstanciasAltasoBajas> ReportesConstanciasAltasoBajas { set; get; } = new();
        private bool MostrarSppinerLogin { get; set; }
        private async Task<string> FormatoFecha(DateTime? fecha)
        {
            DateTime dateTime = (DateTime)fecha!;
            string fechaConvert = dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fff");
            return fechaConvert;
        }
        private async Task<string> FormatoFechaNombre(DateTime? fecha1, DateTime? fecha2)
        {
            DateTime dateTime = (DateTime)fecha1!;
            DateTime dateTime2 = (DateTime)fecha2!;
            string fechaConvert = $"{dateTime.ToString("ddMMyyyy")}-{dateTime2.ToString("ddMMyyyy")}";
            return fechaConvert;
        }
        private async Task DownloadExcel()
        {
            try
            {
                MostrarSppinerLogin = true;
                await Task.Delay(1000);
                var fileBytes = _excelService.CreateExcelFile(ReportesConstancias);
                var base64 = Convert.ToBase64String(fileBytes);
                string fecha = await FormatoFechaNombre(CriterioDeReportes.FechaInicio, CriterioDeReportes.FechaTermino);
                await JSRuntime.InvokeVoidAsync("JS.SaveAs", $"{fecha}.xlsx", base64);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                MostrarSppinerLogin = false;
            }
        }
        private async Task DownloadExcelAltasoBajas()
        {
            try
            {
                MostrarSppinerLogin = true;
                await Task.Delay(1000);
                int tipoInforme = 0;
                if (CriterioDeReportes.TipoDeReporte == "Informe de Altas") tipoInforme = 1;
                else tipoInforme = 2;
                var fileBytes = _excelService.CreateExcelFileAltasoBajas(ReportesConstanciasAltasoBajas, tipoInforme);
                var base64 = Convert.ToBase64String(fileBytes);
                string fecha = await FormatoFechaNombre(CriterioDeReportes.FechaInicio, CriterioDeReportes.FechaTermino);
                await JSRuntime.InvokeVoidAsync("JS.SaveAs", $"{fecha}.xlsx", base64);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                MostrarSppinerLogin = false;
            }
        }
        private async Task DownloadPdf(List<ReportesConstanciasConsulta> reportes, CriterioDeReportes criterioDeReportes)
        {
            try
            {
                ObtenerReportePdf obtenerReportePdf = new ObtenerReportePdf
                {
                    FechaInicio = (DateTime)criterioDeReportes.FechaInicio!,
                    FechaTermino = (DateTime)criterioDeReportes.FechaTermino!,
                    Constancias = reportes!
                };
                HttpResponseMessage reportesPdf = await httpClient.PostAsJsonAsync<ObtenerReportePdf>($"/api/ReportesConstancias/GeneratePdf", obtenerReportePdf);

                if (reportesPdf.IsSuccessStatusCode)
                {
                    var pdf = await reportesPdf.Content.ReadAsByteArrayAsync();
                    var base64 = Convert.ToBase64String(pdf);
                    await Task.Delay(1000);
                    await JSRuntime.InvokeVoidAsync("downloadPdf", base64);
                    await Task.Delay(1000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }           
        }

        private async Task ObtenerReporteAltasoBajas() 
        {
            try
            {
                ReportesConstanciasAltasoBajas.Clear();
                int tipoInforme = 0;
                if(CriterioDeReportes.TipoDeReporte == "Informe de Altas") tipoInforme = 1;
                else tipoInforme = 2;
                CriterioDeReportesConsulta criterioDeReportesConsulta = new CriterioDeReportesConsulta
                {
                    FECHA1 = await FormatoFecha(CriterioDeReportes.FechaInicio),
                    FECHA2 = await FormatoFecha(CriterioDeReportes.FechaTermino),
                    TIPO = tipoInforme
                };

                Console.WriteLine($"FECHA1 {criterioDeReportesConsulta.FECHA1}");
                Console.WriteLine($"FECHA2 {criterioDeReportesConsulta.FECHA2}");
                Console.WriteLine($"TIPO {criterioDeReportesConsulta.TIPO}");

                HttpResponseMessage reportes = await httpClient.PostAsJsonAsync<CriterioDeReportesConsulta>($"/api/ReportesConstancias/GetReportesAltasoBajas", criterioDeReportesConsulta);

                if (reportes.IsSuccessStatusCode)
                {
                    List<ReportesConstanciasAltasoBajas> repondeReportes = await reportes.Content.ReadFromJsonAsync<List<ReportesConstanciasAltasoBajas>>();
                    if (repondeReportes is not null)
                    {
                        if (repondeReportes.Count() > 0)
                        {
                            Console.WriteLine($"ReportesConstanciasAltasoBajas: {repondeReportes.Count()}");
                            ReportesConstanciasAltasoBajas.AddRange(repondeReportes);                           
                        }
                        else
                        {
                            ErroresEncontrados.Add("No se encontro ningun registro dentro de este periodo de tiempo");
                        }
                    }
                    else
                    {
                        ErroresEncontrados.Add("No se encontro ningun registro dentro de este periodo de tiempo");
                    }
                }
                if (ErroresEncontrados.Count() > 0) await MostrarModales(TipoModal.Errores);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                MostrarSppinerLogin = false;
            }
        }
        private async Task ObtenerReporte() 
        {
            try
            {
                ReportesConstancias.Clear();
                CriterioDeReportesConsulta criterioDeReportesConsulta = new CriterioDeReportesConsulta
                {
                    FECHA1 = await FormatoFecha(CriterioDeReportes.FechaInicio),
                    FECHA2 = await FormatoFecha(CriterioDeReportes.FechaTermino),
                    TIPO = 3
                };

                Console.WriteLine($"FECHA1 {criterioDeReportesConsulta.FECHA1}");
                Console.WriteLine($"FECHA2 {criterioDeReportesConsulta.FECHA2}");
                Console.WriteLine($"TIPO {criterioDeReportesConsulta.TIPO}");

                HttpResponseMessage reportes = await httpClient.PostAsJsonAsync<CriterioDeReportesConsulta>($"/api/ReportesConstancias/GetReportes", criterioDeReportesConsulta);

                if (reportes.IsSuccessStatusCode)
                {
                    List<ReportesConstanciasConsulta> repondeReportes = await reportes.Content.ReadFromJsonAsync<List<ReportesConstanciasConsulta>>();
                    if (repondeReportes is not null)
                    {
                        if (repondeReportes.Count() > 0)
                        {
                            Console.WriteLine($"repondeReportes: {repondeReportes.Count()}");
                            ReportesConstancias.AddRange(repondeReportes);
                            //await DescargarReporte(ReportesConstancias, CriterioDeReportes);
                            await DownloadPdf(ReportesConstancias, CriterioDeReportes);
                        }
                        else
                        {
                            ErroresEncontrados.Add("No se encontro ningun registro dentro de este periodo de tiempo");
                        }
                    }
                    else
                    {
                        ErroresEncontrados.Add("No se encontro ningun registro dentro de este periodo de tiempo");
                    }
                }
                if (ErroresEncontrados.Count() > 0) await MostrarModales(TipoModal.Errores);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally 
            {
                MostrarSppinerLogin = false;
            }
        }

        private async Task DescargarReporte(List<ReportesConstanciasConsulta> reportes, CriterioDeReportes criterioDeReportes) 
        {
            try
            {
                ObtenerReportePdf obtenerReportePdf = new ObtenerReportePdf
                {
                    FechaInicio = (DateTime)criterioDeReportes.FechaInicio!,
                    FechaTermino = (DateTime)criterioDeReportes.FechaTermino!,
                    Constancias = reportes!
                };

                HttpResponseMessage respondePdf = await httpClient.PostAsJsonAsync<ObtenerReportePdf>($"/api/ReportesConstancias/GetReportesPDF", obtenerReportePdf);

                if (respondePdf.IsSuccessStatusCode)
                {
                    using (Stream pdfStream = await respondePdf.Content.ReadAsStreamAsync())
                    {
                        string fileName = $"ReporteConstancias{DateTime.Now.ToString("dd-MM-yyyy")}.pdf";

                        // Convertir el archivo PDF a Base64
                        byte[] pdfBytes = await ReadFully(pdfStream);
                        string pdfBase64 = Convert.ToBase64String(pdfBytes);

                        // Invocar la función JavaScript para mostrar el archivo PDF
                        await JSRuntime.InvokeAsync<object>("open_reportviewer", fileName, pdfBase64);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally 
            {
                MostrarSppinerLogin = false;
            }
        }
        public async Task<byte[]> ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                await input.CopyToAsync(ms);
                return ms.ToArray();
            }
        }        

        private async Task ValidarInformacion()
        {
            try
            {
                MostrarSppinerLogin = true;
                ErroresEncontrados.Clear();
                List<string> listaErrores = new();
                if (CriterioDeReportes.FechaInicio == null)
                {
                    listaErrores.Add("El campo Fecha Inicio es obligatorio.");
                }
                if (CriterioDeReportes.FechaTermino == null)
                {
                    listaErrores.Add("El campo Fecha Termino es obligatorio.");
                }
                if (ReporteAltasBajas)
                {
                    if (CriterioDeReportes.TipoDeReporte == null || CriterioDeReportes.TipoDeReporte == string.Empty)
                    {
                        listaErrores.Add("El campo tipo de reporte es obligatorio.");
                    }
                }
                if (CriterioDeReportes.FechaInicio != null & CriterioDeReportes.FechaTermino != null)
                {
                    DateTime f1 = (DateTime)CriterioDeReportes.FechaInicio!;
                    DateTime f2 = (DateTime)CriterioDeReportes.FechaTermino!;
                    int result = DateTime.Compare(f2, f1);
                    if (result < 0)
                    {
                        listaErrores.Add("La fecha de inicio debe ser menor a la fecha de término");
                    }
                }

                ErroresEncontrados = listaErrores;
                if (ErroresEncontrados.Count() > 0) await MostrarModales(TipoModal.Errores);
                else 
                {

                    if (ReporteAltasBajas) await ObtenerReporteAltasoBajas();
                    else await ObtenerReporte();
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally 
            {
                MostrarSppinerLogin = false;
            }

        }

        public enum TipoModal
        {
            Errores = 1,
        }

        private async Task MostrarModales(TipoModal tipoModal)
        {
            if (tipoModal == TipoModal.Errores)
            {
                var options = new DialogOptions
                {
                    CloseOnEscapeKey = true,
                    FullWidth = true,
                    MaxWidth = MaxWidth.Small,
                    CloseButton = true
                };
                var parameters = new DialogParameters<ModalErrores>();
                parameters.Add(p => p.listaErrores, ErroresEncontrados);
                DialogService.Show<ModalErrores>("Simple Dialog", parameters, options);
            }
        }
    }
}
