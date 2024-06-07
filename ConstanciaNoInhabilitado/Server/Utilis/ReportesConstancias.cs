using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Mvc;

namespace ConstanciaNoInhabilitado.Server.Utilis
{
    public class ReportesConstancias : ControllerBase
    {
        private readonly IWebHostEnvironment _webHost;

        public ReportesConstancias(IWebHostEnvironment webHostEnvironment)
        {
            _webHost = webHostEnvironment;
        }     

        public ActionResult GeneratePdf(string text)
        {
            try
            {
                byte[] pdfBytes;

                using (var stream = new MemoryStream())
                {
                    PdfWriter writer = new PdfWriter(stream);
                    PdfDocument pdf = new PdfDocument(writer);
                    Document document = new Document(pdf);

                    document.Add(new Paragraph(text));
                    document.Close();

                    pdfBytes = stream.ToArray();
                }

                return File(pdfBytes, "application/pdf", "generated.pdf");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
