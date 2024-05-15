using ConstanciaNoInhabilitado.Shared.Entities.Login;
using ConstanciaNoInhabilitado.Client.Shared.Partial.Dialogs;
using ConstanciaNoInhabilitado.Client.Utils;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Runtime.CompilerServices;
using System.Net.Http.Json;

namespace ConstanciaNoInhabilitado.Client.Pages.Login
{
    public partial class Default
    {
        [Inject]
        public IDialogService dialogService { get; set; }
        public SearchReference Reference { get; set; } = new();
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
            MostrarSppinerLogin = true;
            MensajeDeError = string.Empty;
            if (Reference.Reference.Length < 20) MensajeDeError = "¡La longitud de la referencia debe de ser de 20 digitos!";
            if (Reference.Captcha != Captcha) MensajeDeError = "¡Introduce correctamente el texto de la imagen!";
            if (MensajeDeError == string.Empty) await ConsultaConstancia();
            MostrarSppinerLogin = false;
        }
        public async Task ConsultaConstancia()
        {
            try
            {
                var logueoResponse = await httpClient.PostAsJsonAsync<SearchReference>("/api/Login/DescargarConstancia", Reference);
                if (logueoResponse.IsSuccessStatusCode) 
                {
                    var referenceResponse = await logueoResponse.Content.ReadFromJsonAsync<SearchReferenceReponse>();
                    if(referenceResponse?.ReferenceSearch != string.Empty) navigationManager.NavigateTo(referenceResponse.ReferenceSearch);
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
