using ConstanciaNoInhabilitado.Shared.Entities.Login;
using ConstanciaNoInhabilitado.Client.Shared.Partial.Dialogs;
using ConstanciaNoInhabilitado.Client.Utils;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Runtime.CompilerServices;
using System.Net.Http.Json;
using ConstanciaNoInhabilitado.Shared.Entities.Constancia;

namespace ConstanciaNoInhabilitado.Client.Pages.Login
{
    public partial class Default
    {
        [Inject]
        public IDialogService dialogService { get; set; }
        public SearchReference Reference { get; set; } = new();
        public List<ConstanciaInhabilitado> constanciaInhabilitados { get; set; } = new();
        private bool MostrarSppinerLogin { get; set; }
        private string Captcha { get; set; } = "";
        public string CaptchaDeUser { get; set; } = "";
        public string MensajeDeError { get; set; } = "";
        private bool valided { get; set; } = true;
        private int CaptchaLetters { get; set; } = 5;
        protected override async Task OnInitializedAsync()
        {
            GenerateNewCaptcha();
        }

        public void GenerateNewCaptcha()
        {
            Captcha = CaptchaNumberUtility.GetCaptchaNumber(CaptchaLetters);
            Reference.Captcha = ""; // Limpiar el valor del campo de entrada anterior
            valided = false; // Restaurar el estado de validación
            Console.WriteLine($"Captcha - {Captcha}");
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
            await dialogService.ShowAsync<ModalAvisoInformacionReferencia>("Simple Dialog", options);
        }

        private async Task ValidarInformacion() 
        {
            try
            {
                MostrarSppinerLogin = true;
                MensajeDeError = string.Empty;
                if (Reference.Reference is null)
                {
                    MensajeDeError = "¡El campo referencia es obligatoria!";
                }
                else 
                {
                    if (Reference.Reference!.Length < 20) MensajeDeError = "¡La longitud de la referencia debe de ser de 20 digitos!";
                }
                if (Reference.Reference == string.Empty) MensajeDeError = "¡El campo referencia es obligatoria!";                
                if (Reference.Captcha != Captcha) MensajeDeError = "¡Introduce correctamente el texto de la imagen!";
                if (MensajeDeError == string.Empty) await ConsultaConstancia();            
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
        public async Task ConsultaConstancia()
        {
            try
            {
                ConstanciaBusqueda constanciaBusqueda = new ConstanciaBusqueda 
                {
                    Criterio = Reference.Reference,
                    Tipo = 1
                };

                var logueoResponse = await httpClient.PostAsJsonAsync<ConstanciaBusqueda>("/api/Constancias/GetReportes", constanciaBusqueda);
                if (logueoResponse.IsSuccessStatusCode) 
                {
                    var referenceResponse = await logueoResponse.Content.ReadFromJsonAsync<List<ConstanciaInhabilitado>>();
                    if (referenceResponse is not null) 
                    {

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally 
            {
               
            }
        }
    }
}
