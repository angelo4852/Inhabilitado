using ClosedXML.Excel;
using ConstanciaNoInhabilitado.Client.Interfaces;
using ConstanciaNoInhabilitado.Shared.Entities.CargaMasiva;
using DocumentFormat.OpenXml.Spreadsheet;

public class FileService : IFileService
{
    private List<string> _ErroresEncontradosDeFormatoArchivo { get; set; } = new();
    public async Task<CargaMasivaExcelDTO> ProcessFile(byte[] fileContent, int IdUsuario)
    {
        CargaMasivaExcelDTO cargaMasivaExcels = new();
        List<CargaMasivaExcel> data = new();
        try
        {
            using var stream = new MemoryStream(fileContent);
            using var workbook = new XLWorkbook(stream);
            var worksheet = workbook.Worksheets.First();

            await ValidarFormatoExcel(worksheet);

            if (_ErroresEncontradosDeFormatoArchivo.Count() > 0) { cargaMasivaExcels.ErroresDeLaCarga.AddRange(_ErroresEncontradosDeFormatoArchivo); return cargaMasivaExcels; }

            foreach (var row in worksheet.RowsUsed().Skip(6))
            {
                var carga = new CargaMasivaExcel
                {
                    Dependencia = row.Cell(1).GetValue<string>(),
                    RFC = row.Cell(2).GetValue<string>(),
                    Homo = row.Cell(3).GetValue<string>(),
                    ApellidoPaterno = row.Cell(4).GetValue<string>(),
                    ApellidoMaterno = row.Cell(5).GetValue<string>(),
                    Nombre = row.Cell(6).GetValue<string>(),
                    AutoridadSancionadora = row.Cell(7).GetValue<string>(),
                    Puesto = row.Cell(8).GetValue<string>(),
                    Periodo = row.Cell(9).GetValue<string>(),
                    FechaResolucion = row.Cell(10).IsEmpty() ? (DateTime?)null : row.Cell(10).GetValue<DateTime>(),
                    FechaNotificacion = row.Cell(11).IsEmpty() ? (DateTime?)null : row.Cell(11).GetValue<DateTime>(),
                    FechaInicio = row.Cell(12).IsEmpty() ? (DateTime?)null : row.Cell(12).GetValue<DateTime>(),
                    FechaFin = row.Cell(13).IsEmpty() ? (DateTime?)null : row.Cell(13).GetValue<DateTime>(),
                    IdUsuario = IdUsuario
                };
                data.Add(carga);
            }
            cargaMasivaExcels.CargaMasivaExcel.AddRange(data);
        }
        catch (Exception ex)
        {
            // Manejo de errores más detallado
            cargaMasivaExcels.ErroresDeLaCarga.Add($"El archivo proporcionado no es un archivo Excel válido o está corrupto.{ex.Message}");
            //throw new InvalidDataException("El archivo proporcionado no es un archivo Excel válido o está corrupto.", ex);
        }

        return cargaMasivaExcels;
    }

    private async Task ValidarFormatoExcel(IXLWorksheet xLWorksheet) 
    {
        try
        {
            _ErroresEncontradosDeFormatoArchivo = new();
            bool encabezadoFaltante = false;

            foreach (var headerRow in xLWorksheet.RowsUsed().Take(6)) // Ajusta el rango de filas según sea necesario
            {
                if (headerRow.Cells().Any(cell => cell.GetValue<string>().Trim().Equals("DEPENDENCIA", StringComparison.OrdinalIgnoreCase)))
                {
                    encabezadoFaltante = true;
                }                
            }

            if (!encabezadoFaltante)
            {
                _ErroresEncontradosDeFormatoArchivo.Add($"El archivo proporcionado no contiene el encabezado 'DEPENDENCIA'");
                //return cargaMasivaExcels;
            }

            foreach (var headerRow in xLWorksheet.RowsUsed().Take(6)) // Ajusta el rango de filas según sea necesario
            {
                if (headerRow.Cells().Any(cell => cell.GetValue<string>().Trim().Equals("RFC", StringComparison.OrdinalIgnoreCase)))
                {
                    encabezadoFaltante = true;
                }
                else 
                {
                    encabezadoFaltante = false;
                }
            }

            if (!encabezadoFaltante)
            {
                _ErroresEncontradosDeFormatoArchivo.Add($"El archivo proporcionado no contiene el encabezado 'RFC'");
                //return cargaMasivaExcels;
            }

            foreach (var headerRow in xLWorksheet.RowsUsed().Take(6)) // Ajusta el rango de filas según sea necesario
            {
                if (headerRow.Cells().Any(cell => cell.GetValue<string>().Trim().Equals("HOMO", StringComparison.OrdinalIgnoreCase)))
                {
                    encabezadoFaltante = true;
                }
                else
                {
                    encabezadoFaltante = false;
                }
            }

            if (!encabezadoFaltante)
            {
                _ErroresEncontradosDeFormatoArchivo.Add($"El archivo proporcionado no contiene el encabezado 'HOMO'");
                //return cargaMasivaExcels;
            }

            foreach (var headerRow in xLWorksheet.RowsUsed().Take(6)) // Ajusta el rango de filas según sea necesario
            {
                if (headerRow.Cells().Any(cell => cell.GetValue<string>().Trim().Equals("APELLIDO PATERNO", StringComparison.OrdinalIgnoreCase)))
                {
                    encabezadoFaltante = true;
                }
                else
                {
                    encabezadoFaltante = false;
                }
            }

            if (!encabezadoFaltante)
            {
                _ErroresEncontradosDeFormatoArchivo.Add($"El archivo proporcionado no contiene el encabezado 'APELLIDO PATERNO'");
                //return cargaMasivaExcels;
            }

            foreach (var headerRow in xLWorksheet.RowsUsed().Take(6)) // Ajusta el rango de filas según sea necesario
            {
                if (headerRow.Cells().Any(cell => cell.GetValue<string>().Trim().Equals("APELLIDO MATERNO", StringComparison.OrdinalIgnoreCase)))
                {
                    encabezadoFaltante = true;
                }
                else
                {
                    encabezadoFaltante = false;
                }
            }

            if (!encabezadoFaltante)
            {
                _ErroresEncontradosDeFormatoArchivo.Add($"El archivo proporcionado no contiene el encabezado 'APELLIDO MATERNO'");
                //return cargaMasivaExcels;
            }

            foreach (var headerRow in xLWorksheet.RowsUsed().Take(6)) // Ajusta el rango de filas según sea necesario
            {
                if (headerRow.Cells().Any(cell => cell.GetValue<string>().Trim().Equals("NOMBRE", StringComparison.OrdinalIgnoreCase)))
                {
                    encabezadoFaltante = true;
                }
                else
                {
                    encabezadoFaltante = false;
                }
            }

            if (!encabezadoFaltante)
            {
                _ErroresEncontradosDeFormatoArchivo.Add($"El archivo proporcionado no contiene el encabezado 'NOMBRE'");
                //return cargaMasivaExcels;
            }

            foreach (var headerRow in xLWorksheet.RowsUsed().Take(6)) // Ajusta el rango de filas según sea necesario
            {
                if (headerRow.Cells().Any(cell => cell.GetValue<string>().Trim().Equals("AUTORIDAD SANCIONADORA", StringComparison.OrdinalIgnoreCase)))
                {
                    encabezadoFaltante = true;
                }
                else
                {
                    encabezadoFaltante = false;
                }
            }

            if (!encabezadoFaltante)
            {
                _ErroresEncontradosDeFormatoArchivo.Add($"El archivo proporcionado no contiene el encabezado 'AUTORIDAD SANCIONADORA'");
                //return cargaMasivaExcels;
            }

            foreach (var headerRow in xLWorksheet.RowsUsed().Take(6)) // Ajusta el rango de filas según sea necesario
            {
                if (headerRow.Cells().Any(cell => cell.GetValue<string>().Trim().Equals("PUESTO", StringComparison.OrdinalIgnoreCase)))
                {
                    encabezadoFaltante = true;
                }
                else
                {
                    encabezadoFaltante = false;
                }
            }

            if (!encabezadoFaltante)
            {
                _ErroresEncontradosDeFormatoArchivo.Add($"El archivo proporcionado no contiene el encabezado 'PUESTO'");
                //return cargaMasivaExcels;
            }

            foreach (var headerRow in xLWorksheet.RowsUsed().Take(6)) // Ajusta el rango de filas según sea necesario
            {
                if (headerRow.Cells().Any(cell => cell.GetValue<string>().Trim().Equals("PERIODO", StringComparison.OrdinalIgnoreCase)))
                {
                    encabezadoFaltante = true;
                }
                else
                {
                    encabezadoFaltante = false;
                }
            }

            if (!encabezadoFaltante)
            {
                _ErroresEncontradosDeFormatoArchivo.Add($"El archivo proporcionado no contiene el encabezado 'PERIODO'");
                //return cargaMasivaExcels;
            }

            foreach (var headerRow in xLWorksheet.RowsUsed().Take(6)) // Ajusta el rango de filas según sea necesario
            {
                if (headerRow.Cells().Any(cell => cell.GetValue<string>().Trim().Equals("FECHA RESOLUCION", StringComparison.OrdinalIgnoreCase)))
                {
                    encabezadoFaltante = true;
                }
                else
                {
                    encabezadoFaltante = false;
                }
            }

            if (!encabezadoFaltante)
            {
                _ErroresEncontradosDeFormatoArchivo.Add($"El archivo proporcionado no contiene el encabezado 'FECHA RESOLUCION'");
                //return cargaMasivaExcels;
            }

            foreach (var headerRow in xLWorksheet.RowsUsed().Take(6)) // Ajusta el rango de filas según sea necesario
            {
                if (headerRow.Cells().Any(cell => cell.GetValue<string>().Trim().Equals("FECHA NOTIFICACION", StringComparison.OrdinalIgnoreCase)))
                {
                    encabezadoFaltante = true;
                }
                else
                {
                    encabezadoFaltante = false;
                }
            }

            if (!encabezadoFaltante)
            {
                _ErroresEncontradosDeFormatoArchivo.Add($"El archivo proporcionado no contiene el encabezado 'FECHA NOTIFICACION'");
                //return cargaMasivaExcels;
            }

            foreach (var headerRow in xLWorksheet.RowsUsed().Take(6)) // Ajusta el rango de filas según sea necesario
            {
                if (headerRow.Cells().Any(cell => cell.GetValue<string>().Trim().Equals("FECHA INICIO", StringComparison.OrdinalIgnoreCase)))
                {
                    encabezadoFaltante = true;
                }
                else
                {
                    encabezadoFaltante = false;
                }
            }

            if (!encabezadoFaltante)
            {
                _ErroresEncontradosDeFormatoArchivo.Add($"El archivo proporcionado no contiene el encabezado 'FECHA INICIO'");
                //return cargaMasivaExcels;
            }

            foreach (var headerRow in xLWorksheet.RowsUsed().Take(6)) // Ajusta el rango de filas según sea necesario
            {
                if (headerRow.Cells().Any(cell => cell.GetValue<string>().Trim().Equals("FECHA FIN", StringComparison.OrdinalIgnoreCase)))
                {
                    encabezadoFaltante = true;
                }
                else
                {
                    encabezadoFaltante = false;
                }
            }

            if (!encabezadoFaltante)
            {
                _ErroresEncontradosDeFormatoArchivo.Add($"El archivo proporcionado no contiene el encabezado 'FECHA FIN'");
                //return cargaMasivaExcels;
            }
        }
        catch (Exception ex)
        {
            throw new InvalidDataException("Error al buscar los encabezados", ex);
        }
       
    }
}