using ConstanciaNoInhabilitado.Shared.Entities.Login;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace ConstanciaNoInhabilitado.Client.Auth
{
    public class Autentication : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorageService;
        private ClaimsPrincipal _principal = new();

        public Autentication(ISessionStorageService sessionStorageService)
        {
            _sessionStorageService = sessionStorageService;
        }

        public async Task ActualizarEstadoAutenticacion(Session? userTaxLogin) 
        {
            ClaimsPrincipal claimsPrincipal = new();

            if (userTaxLogin != null)
            {
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name,userTaxLogin.User),
                    new Claim(ClaimTypes.Email,userTaxLogin.Correo),
                    new Claim(ClaimTypes.Role,userTaxLogin.Rol)
                }, "JwtAuth"));

                userTaxLogin.ExpiryTime = DateTime.UtcNow;
                await _sessionStorageService.SetItemAsync("sesionUser", userTaxLogin);
            }
            else 
            {
                claimsPrincipal = new();
                await _sessionStorageService.RemoveItemAsync("sesionUser");
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var sesionUsuario = await _sessionStorageService.GetItemAsync<Session>("sesionUser");

            if(sesionUsuario == null)
                return await Task.FromResult(new AuthenticationState(_principal));
            DateTime fechaProporcionada = sesionUsuario.ExpiryTime;
            DateTime fechaActual = DateTime.UtcNow;
            TimeSpan diferencia = fechaActual - fechaProporcionada;

            if (diferencia.TotalMinutes > 30) return await Task.FromResult(new AuthenticationState(_principal));

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name,sesionUsuario.User),
                    new Claim(ClaimTypes.Email,sesionUsuario.Correo),
                    new Claim(ClaimTypes.Role,sesionUsuario.Rol)
                }, "JwtAuth"));
            return await Task.FromResult(new AuthenticationState(claimsPrincipal));

        }
    }
}
