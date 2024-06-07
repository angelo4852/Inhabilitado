using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using ConstanciaNoInhabilitado.Shared.Entities.Reportes;
using ClosedXML.Excel;
using System.IO;

namespace ConstanciaNoInhabilitado.Server.Servicios
{
    public class ServicioCrearReportes
    {
        private readonly IWebHostEnvironment _environment;

        public ServicioCrearReportes(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public byte[] CreatePdf(ObtenerReportePdf obtenerReportePdf)
        {
            using (var ms = new MemoryStream())
            {
                // Crear un PdfWriter con el MemoryStream
                using (var writer = new PdfWriter(ms))
                {
                    // Crear un PdfDocument con el PdfWriter
                    using (var pdf = new PdfDocument(writer))
                    {
                        // Crear un Document con el PdfDocument
                        var document = new Document(pdf);

                        // Añadir contenido al documento
                        document.Add(new Paragraph($"Reporte de Constancias Expedidas de {obtenerReportePdf.FechaInicio} al {obtenerReportePdf.FechaTermino}"));

                        // Añadir los datos de constancias al documento
                        //foreach (var constancia in obtenerReportePdf.Constancias)
                        //{                           
                        //    document.Add(new Paragraph(constancia.Referencia));                            
                        //}

                        // Cerrar el documento
                        document.Close();
                    }
                }

                // Retornar los bytes del MemoryStream
                return ms.ToArray();
            }
        }
    }    
}
