using ConstanciaNoInhabilitado.Client.Auth;
using ConstanciaNoInhabilitado.Shared.Entities.Login;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace ConstanciaNoInhabilitado.Client.Pages.Login
{
    public partial class Login
    {
        public UserTaxLogin loguear { get; set; } = new UserTaxLogin();
        private bool MostrarSppinerLogin { get; set; }
        private bool MostrarErrorDeConsulta { get; set; }

        private async Task ConsultaUsuario()
        {
            MostrarSppinerLogin = true;
            MostrarErrorDeConsulta = false; 
            Console.WriteLine($"ConsultaUsuario - {loguear.Usuario} - {loguear.Contrasena}");
            try
            {
                var logueoResponse = await httpClient.PostAsJsonAsync<UserTaxLogin>("/api/Login/ConsultaUsuario", loguear);
                if (logueoResponse.IsSuccessStatusCode) 
                {
                    var sesionUser = await logueoResponse.Content.ReadFromJsonAsync<Session>();
                    if (sesionUser.Rol == "0")
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally 
            {
                MostrarSppinerLogin = false;
            }
        }
    }
}
