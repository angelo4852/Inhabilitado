using ConstanciaNoInhabilitado.Server.Interfaces;
using ConstanciaNoInhabilitado.Server.Servicios;
using ConstanciaNoInhabilitado.Shared.Entities.Reportes;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Mvc;

namespace ConstanciaNoInhabilitado.Server.Controllers.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesConstancias : ControllerBase
    {
        private readonly ServicioRepositorioReportes service;
        private readonly IServicioRepositorioReportes servicioRepositorio;
        private readonly ServicioCrearReportes _ServicioCrearReportes;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ReportesConstancias(IConfiguration configuration, IWebHostEnvironment iweb)
        {
            service = new ServicioRepositorioReportes(configuration);
            _ServicioCrearReportes = new ServicioCrearReportes(iweb);
            _webHostEnvironment = iweb;
        }

        [HttpPost]
        [Route("GetReportes")]
        public async Task<ActionResult> GetReportes(CriterioDeReportesConsulta criterioDeReportesConsulta)
        {
            try
            {
                List<Shared.Entities.Reportes.ReportesConstanciasConsulta> ReportesExpedidas = await service.GetReportes(criterioDeReportesConsulta);
                return Ok(ReportesExpedidas);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("GetReportesAltasoBajas")]
        public async Task<ActionResult> GetReportesAltasoBajas(CriterioDeReportesConsulta criterioDeReportesConsulta)
        {
            try
            {
                List<ReportesConstanciasAltasoBajas> ReportesExpedidas = await service.GetReportesAltasoBajas(criterioDeReportesConsulta);
                return Ok(ReportesExpedidas);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("GetReportesPDF")]
        public async Task<ActionResult> GetReportesPDF(ObtenerReportePdf obtenerReportePdf)
        {
            try
            {              
                var pdf =  _ServicioCrearReportes.CreatePdf(obtenerReportePdf);
                return Ok(pdf);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<string> FormatoFecha(DateTime? fechaConvetir) 
        {
            DateTime fecha = (DateTime)fechaConvetir!;
            string fechaResult = fecha.ToString("dd/MM/yyyy");
            return fechaResult;
        }

        [HttpPost("GeneratePdf")]
        public async Task<ActionResult> GeneratePdf(ObtenerReportePdf obtenerReportePdf)
        {
            try
            {
                 int M_IZQ = 70; //70
                 int M_DER = 70; //70
                 int M_SUP = 55; //115
                 int M_INF = 85; //85

                 byte[] pdfBytes;
                string nombreArchivo = "generated.pdf";

                string FechaInicio = obtenerReportePdf.FechaInicio.ToString("dd/MM/yyyy");
                string FechaTermino = obtenerReportePdf.FechaTermino.ToString("dd/MM/yyyy");

                using (var stream = new MemoryStream())
                {
                    PdfWriter writer = new PdfWriter(stream);
                    PdfDocument pdfDoc = new PdfDocument(writer);
                    Document document = new Document(pdfDoc, iText.Kernel.Geom.PageSize.A4.Rotate(), false);

                    document.SetMargins(M_SUP, M_DER, M_INF, M_IZQ);
                    // Define fonts
                    PdfFont fontCb = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                    PdfFont fontCn = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

                    // Define styles
                    Style fuenteTitulo = new Style().SetFont(fontCb).SetFontSize(10);
                    Style fuenteTitulo2 = new Style().SetFont(fontCb).SetFontSize(12);
                    Style fuenteSubtitulo2 = new Style().SetFont(fontCn).SetFontSize(9);
                    Style fuenteCuerpo = new Style().SetFont(fontCn).SetFontSize(10);
                    Style fuenteCuerpoS = new Style().SetFont(fontCn).SetFontSize(6.5f);

                    //Header image(use a sample image or base64 encoded image)
                    //string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "constancias", "constancia1.gif");
                    //byte[] imageData = System.IO.File.ReadAllBytes(imagePath); // Replace with actual Base64 image data
                    //Image imageHeader = new Image(ImageDataFactory.Create(imageData));
                    //imageHeader.ScaleAbsolute(100f, 60f);                    

                    Console.WriteLine($"Ruta ---> {_webHostEnvironment.WebRootPath}");

                    // Header rows
                    List<string> headers = new List<string>
                    {
                        "REFERENCIA", "FECHA\nPAGO", "FECHA\nENTREGA", "R.F.C", "NOMBRE DEL INTERESADO",
                        "C.U.R.P.", "TIPO", "USUARIO"
                    };

                    // Create table
                    Table table = new Table(8);
                    table.SetWidth(UnitValue.CreatePercentValue(100));

                    // Add header cells
                    foreach (var header in headers)
                    {
                        table.AddHeaderCell(new Paragraph(header).AddStyle(fuenteSubtitulo2));
                    }

                    // Add data rows (replace 'lista' with actual data source)
                    List<ReportesConstanciasConsulta> lista = obtenerReportePdf.Constancias; // Replace with actual data fetching logic
                    foreach (var row in lista)
                    {
                        table.AddCell(new Paragraph(row.Referencia).AddStyle(fuenteCuerpoS));
                        table.AddCell(new Paragraph(await FormatoFecha(row.FechaExpedicion)).AddStyle(fuenteCuerpoS));
                        table.AddCell(new Paragraph(await FormatoFecha(row.FechaCreacion)).AddStyle(fuenteCuerpoS));
                        table.AddCell(new Paragraph(row.RFC).AddStyle(fuenteCuerpoS));
                        table.AddCell(new Paragraph(row.Nombre).AddStyle(fuenteCuerpoS));
                        table.AddCell(new Paragraph(row.CURP ?? string.Empty).AddStyle(fuenteCuerpoS));
                        table.AddCell(new Paragraph(row.Tipo.Equals("1") ? "PERSONAL" : "EMPRESA").AddStyle(fuenteCuerpoS));
                        table.AddCell(new Paragraph(row.IdUsuario).AddStyle(fuenteCuerpoS));
                    }

                    // Add elements to document
                    //document.Add(imageHeader);
                    document.Add(new Paragraph($"Reporte de Constancias Expedidas de {FechaInicio} al {FechaTermino}").AddStyle(fuenteTitulo2));
                    document.Add(table);

                    // Close document
                    document.Close();
                    pdfBytes = stream.ToArray();
                }

                return File(pdfBytes, "application/pdf", nombreArchivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
    }
}
