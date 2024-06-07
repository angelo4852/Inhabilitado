using ConstanciaNoInhabilitado.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using ConstanciaNoInhabilitado.Client.Auth;
using ConstanciaNoInhabilitado.Client.Utils.Services;
using MudBlazor;
using ConstanciaNoInhabilitado.Client.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<ExcelService>();
builder.Services.AddSingleton<IFileService, FileService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<AuthenticationStateProvider,Autentication>();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();


