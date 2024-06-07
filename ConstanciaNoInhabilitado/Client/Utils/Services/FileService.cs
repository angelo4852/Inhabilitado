using ClosedXML.Excel;
using ConstanciaNoInhabilitado.Client.Interfaces;
using ConstanciaNoInhabilitado.Shared.Entities.CargaMasiva;

public class FileService : IFileService
{
    public async Task<List<InhabilitacionCarga>> GetInhabilitacion(List<CargaMasivaExcel> cargaMasivaExcels)
    {
        List<InhabilitacionCarga> inhabilitacionCargas = new();
        foreach (var carga in cargaMasivaExcels) 
        {
            InhabilitacionCarga inhabilitacionCarga = new InhabilitacionCarga 
            {
                Dependencia = carga.Dependencia,
                Autoridad = carga.AutoridadSancionadora,
                Cargo = carga.Puesto,
                Periodo = carga.Periodo,
                FechaResolucion = carga.FechaNotificacion.ToString(),
                FechaNotificacion = carga.FechaNotificacion.ToString(),
                FechaInicio = carga.FechaInicio.ToString(),
                FechaFin = carga.FechaFin.ToString(),                
            };
            inhabilitacionCargas.Add(inhabilitacionCarga);
        }

        return inhabilitacionCargas;
    }

    public async Task<List<InhabilitadoCarga>> GetInhabilitado(List<CargaMasivaExcel> cargaMasivaExcels, int IdUsuario)
    {
        List<InhabilitadoCarga> inhabilitadoCargas = new();
        foreach (var carga in cargaMasivaExcels)
        {
            InhabilitadoCarga inhabilitadoCarga = new InhabilitadoCarga 
            {
                NOMB = carga.Nombre,
                AP_PAT = carga.ApellidoPaterno,
                AP_MAT = carga.ApellidoMaterno,
                R_F_C = $"{carga.RFC!.Trim()}{carga.Homo!.Trim()}",
                USU = IdUsuario,
                TIP = 1
            };
            inhabilitadoCargas.Add(inhabilitadoCarga);
        }
        return inhabilitadoCargas;
    }

    public async Task<CargaMasivaExcelDTO> ProcessFile(byte[] fileContent, int IdUsuario)
    {
        CargaMasivaExcelDTO cargaMasivaExcels = new();
        List<CargaMasivaExcel> data = new();
        try
        {
            using var stream = new MemoryStream(fileContent);
            using var workbook = new XLWorkbook(stream);
            var worksheet = workbook.Worksheets.First();

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
            Console.WriteLine($"Error al procesar el archivo: {ex.Message}");
            throw new InvalidDataException("El archivo proporcionado no es un archivo Excel válido o está corrupto.", ex);
        }

        return cargaMasivaExcels;
    }
}