using ConstanciaNoInhabilitado.Client.Auth;
using ConstanciaNoInhabilitado.Client.Utils.Services;
using ConstanciaNoInhabilitado.Shared.Entities.Login;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using MudBlazor;
using System.Net.Http.Json;

namespace ConstanciaNoInhabilitado.Client.Pages.Login
{
    public partial class Login
    {
        public UserTaxLogin loguear { get; set; } = new();

        private bool MostrarSppinerLogin { get; set; }
        private bool MostrarErrorDeConsulta { get; set; }
        private string InputText { get; set; }
        private string Hash { get; set; }

        InputType PasswordInput = InputType.Password;
        string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
        bool isShow;
        protected override async Task OnInitializedAsync()
        {
            //loguear.Usuario = "angel.hernandezj";
            //loguear.Contrasena = "e10adc3949ba59abbe56e057f20f883e";
        }

        private async Task<string> GenerateHash(string contrasena)
        {
            Hash = MD5Service.ComputeHash(contrasena);
            return Hash;
        }

        void ButtonTestclick()
        {
            if (isShow)
            {
                isShow = false;
                PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                PasswordInput = InputType.Password;
            }
            else
            {
                isShow = true;
                PasswordInputIcon = Icons.Material.Filled.Visibility;
                PasswordInput = InputType.Text;
            }
        }

        private async Task ConsultaUsuario()
        {
            MostrarSppinerLogin = true;
            MostrarErrorDeConsulta = false;
            //Console.WriteLine($"ConsultaUsuario - {loguear.Usuario} - {loguear.Contrasena}");
            try
            {
                var contraseñaEncriptada = await GenerateHash(loguear.Contrasena);

                //Console.WriteLine($"contraseñaEncriptada - {contraseñaEncriptada}");

                loguear.Contrasena = contraseñaEncriptada;

                var logueoResponse = await httpClient.PostAsJsonAsync<UserTaxLogin>("/api/Login/ConsultaUsuario", loguear);
                if (logueoResponse.IsSuccessStatusCode)
                {
                    var sesionUser = await logueoResponse.Content.ReadFromJsonAsync<Session>();
                    if (sesionUser!.IdUser == 0)
                    {
                        MostrarErrorDeConsulta = true;
                    }
                    else
                    {
                        var autenticationExt = (Autentication)authenticationStateProvider;
                        await autenticationExt.ActualizarEstadoAutenticacion(sesionUser);
                        navigationManager.NavigateTo("/ConstanciaNoInhabilitado/Admin/Default");
                    }
                    Console.WriteLine($"Name: {sesionUser.User} Email: {sesionUser.Correo} - Rol: {sesionUser.Rol}");
                }
                else 
                {
                    Console.WriteLine($"StatusCode {logueoResponse.StatusCode.ToString()}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                MostrarSppinerLogin = false;
                loguear = new();
            }
        }
    }
}
