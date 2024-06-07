using ConstanciaNoInhabilitado.Client.Shared.Partial.Dialogs;
using ConstanciaNoInhabilitado.Shared.Entities.CargaMasiva;
using ConstanciaNoInhabilitado.Shared.Entities.Login;
using ConstanciaNoInhabilitado.Shared.Entities.RegistroInhabilitacion;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.Net.Http.Json;
using System.Net.Http;

namespace ConstanciaNoInhabilitado.Client.Componentes.Admin.ComponentesAdmin.CargarDesdeExcel
{
    partial class CargarDesdeExcel
    {
        IBrowserFile ExcelFile { set; get; }
        private bool Spinner { set; get; }
        private bool MostrarRescultadosCargaMasiva { set; get; }
        private CargaMasivaExcelDTO CargaMasivaData { set; get; } = new();
        private int MaxHeight { set; get; } = 150;
        private Session Session { set; get; }

        protected override async Task OnInitializedAsync()
        {
            await CargarInformacion();
        }

        private async Task CargarInformacion()
        {
            Session = await _sessionStorage.GetItemAsync<Session>("sesionUser");
        }
        private void UploadFiles(IBrowserFile file)
        {
            ExcelFile = file;
            //TODO upload the files to the server
        }

        public async Task MostrarMensaje()
        {
            var options = new DialogOptions()
            {
                FullWidth = true,
                MaxWidth = MaxWidth.Small,
                DisableBackdropClick = true,
                ClassBackground = "filterBackground",
                CloseButton = true
            };
            await dialogService.ShowAsync<ModalAyudaCargarExcel>("Simple Dialog", options);
        }

        private async Task LeerArchivoExcel() 
        {
            try
            {
                Spinner = true;
                MostrarRescultadosCargaMasiva = false;
                await Task.Delay(500);
                if (ExcelFile is not null)
                {
                    await CargarArchivo();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }finally 
            {
                Spinner = false; 
            }   
        }
        private async Task CargarArchivo() 
        {
            var file = ExcelFile;
            using var stream = file.OpenReadStream(maxAllowedSize: 10485760); // 10 MB
            var buffer = new byte[file.Size];
            await stream.ReadAsync(buffer);
            Console.WriteLine(buffer);
            CargaMasivaData = await FileService.ProcessFile(buffer, Session.IdUser);

            await RegistrarArchivoExcel();
        }
        private async Task RegistrarArchivoExcel()
        {
            try
            {               
                HttpResponseMessage inhabilitadoResponde = await httpClient.PostAsJsonAsync<CargaMasivaExcelDTO>($"api/CargaMasivaArchivoExcel/RegistrarCargaMasicaExcel", CargaMasivaData);

                if (inhabilitadoResponde.IsSuccessStatusCode)
                {
                    CargaMasivaExcelDTO? responde = await inhabilitadoResponde.Content.ReadFromJsonAsync<CargaMasivaExcelDTO>();

                    if (responde is not null) 
                    {
                        Console.WriteLine($"InhabilitadoCarga {responde.InhabilitadoCarga.Count()}");
                        Console.WriteLine($"InhabilitacionCarga {responde.InhabilitacionCarga.Count()}");
                        Console.WriteLine($"CargaMasivaExcel {responde.CargaMasivaExcel.Count()}");
                        CargaMasivaData = responde;

                        foreach (var item in responde.ErroresDeLaCarga)
                        {
                            Console.WriteLine($"{item}");   
                        }

                        if (responde.ErroresDeLaCarga.Count() > 0) 
                        {
                            int altura = MaxHeight;
                            int alturaaprox = responde.ErroresDeLaCarga.Count() * 60;
                            MaxHeight = alturaaprox + altura;
                            Console.WriteLine($"Altura aprox {MaxHeight}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally 
            { 
                MostrarRescultadosCargaMasiva = true;
            }
        }
    }
}
