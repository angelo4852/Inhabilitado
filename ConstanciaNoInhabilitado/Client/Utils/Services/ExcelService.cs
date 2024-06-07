using ClosedXML.Excel;
using ConstanciaNoInhabilitado.Shared.Entities.Reportes;
using DocumentFormat.OpenXml.Spreadsheet;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using MudBlazor;
using System.IO;


namespace ConstanciaNoInhabilitado.Client.Utils.Services
{
    public class ExcelService
    {        
        public byte[] CreateExcelFile(List<ReportesConstanciasConsulta> ReportesConstancias)
        {
            using (var workbook = new XLWorkbook())
            {
                int celda = 1;
                int fila = 1;

                var encabezados = new List<string>
                {
                    "REFERENCIA",
                    "FECHA DE PAGO",
                    "FECHA ENTREGA",
                    "R.F.C.",
                    "NOMBRE DEL INTERESADO",
                    "C.U.R.P.",
                    "TIPO CONSTANCIA",
                    "USUARIO"
                };

                var worksheet = workbook.Worksheets.Add("Reporte");
                foreach (var titulo in encabezados) 
                {
                    worksheet.Cell(fila, celda).Value = titulo;
                    celda++;
                }

                celda = 1;
                fila = 2;
                foreach (var constancia in ReportesConstancias)
                {
                    celda = 1;
                    worksheet.Cell(fila, celda).Value = constancia.Referencia;
                    celda++;
                    worksheet.Cell(fila, celda).Value = constancia.FechaExpedicion;
                    celda++;
                    worksheet.Cell(fila, celda).Value = constancia.FechaCreacion;
                    celda++;
                    worksheet.Cell(fila, celda).Value = constancia.RFC;
                    celda++;
                    worksheet.Cell(fila, celda).Value = constancia.Nombre;
                    celda++;
                    worksheet.Cell(fila, celda).Value = constancia.CURP;
                    celda++;
                    worksheet.Cell(fila, celda).Value = constancia.Tipo.Equals("1") ? "PERSONAL" : "EMPRESA";
                    celda++;
                    worksheet.Cell(fila, celda).Value = constancia.IdUsuario;                    
                    fila++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }

        public byte[] CreateExcelFileAltasoBajas(List<ReportesConstanciasAltasoBajas> ReportesConstancias,int tipo)
        {
            using (var workbook = new XLWorkbook())
            {
                int celda = 1;
                int fila = 1;

                var encabezados = new List<string>
                {
                    "#",
                    "R.F.C.",
                    "APELLIDO PATERNO",
                    "APELLIDO MATERNO",
                    "NOMBRE",
                    "AUTORIDAD SANCIONADORA",
                    "DEPENDENCIA",
                    "PUESTO O CARGO",
                    "PERIODO INHAB.",
                    "FECHA INICIO",
                    "FECHA TERMINO",
                    "FECHA RESOLUCIÓN",
                    "ORIGEN",
                    "CAUSA",
                    "DESCRIPCIÓN",
                };

                var worksheet = tipo == 1 ? workbook.Worksheets.Add("Altas") : workbook.Worksheets.Add("Bajas");


                foreach (var titulo in encabezados)
                {
                    worksheet.Cell(fila, celda).Value = titulo;
                    celda++;
                }

                celda = 1;
                fila = 2;
                foreach (var constancia in ReportesConstancias)
                {
                    celda = 1;
                    worksheet.Cell(fila, celda).Value = constancia.IdInhabilitacion;
                    celda++;
                    worksheet.Cell(fila, celda).Value = constancia.RFC;
                    celda++;
                    worksheet.Cell(fila, celda).Value = constancia.ApellidoPaterno;
                    celda++;
                    worksheet.Cell(fila, celda).Value = constancia.ApellidoMaterno;
                    celda++;
                    worksheet.Cell(fila, celda).Value = constancia.Nombre;
                    celda++;
                    worksheet.Cell(fila, celda).Value = constancia.Autoridad;
                    celda++;
                    worksheet.Cell(fila, celda).Value = constancia.Dependencia;
                    celda++;
                    worksheet.Cell(fila, celda).Value = constancia.Cargo;
                    celda++;
                    worksheet.Cell(fila, celda).Value = constancia.Periodo;
                    celda++;
                    worksheet.Cell(fila, celda).Value = constancia.FechaInicio;
                    celda++;
                    worksheet.Cell(fila, celda).Value = constancia.FechaTermino;
                    celda++;
                    worksheet.Cell(fila, celda).Value = constancia.FechaResolucion;
                    celda++;
                    worksheet.Cell(fila, celda).Value = constancia.Origen;
                    celda++;
                    worksheet.Cell(fila, celda).Value = constancia.Causa;
                    celda++;
                    worksheet.Cell(fila, celda).Value = constancia.Descripcion;
                    celda++;                   
                    fila++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}