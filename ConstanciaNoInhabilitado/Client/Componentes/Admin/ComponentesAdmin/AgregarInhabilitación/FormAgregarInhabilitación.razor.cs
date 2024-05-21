using ConstanciaNoInhabilitado.Client.Shared.Partial.Dialogs;
using ConstanciaNoInhabilitado.Shared.Entities.Catalogos;
using ConstanciaNoInhabilitado.Shared.Entities.Login;
using ConstanciaNoInhabilitado.Shared.Entities.RegistroInhabilitacion;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using MudBlazor;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConstanciaNoInhabilitado.Client.Componentes.Admin.ComponentesAdmin.AgregarInhabilitación
{
    partial class FormAgregarInhabilitación
    {
        [Parameter] public bool DefineInhabilitacion { set; get; } = false;
        private RegistrarNuevaInhabilitacion RegistrarNuevaInhabilitacion { set; get; } = new();
        private RegistroInhabilitacionValidaciones registroInhabilitacionValidaciones { set; get; } = new();
        private List<string> ErroresEncontrados { set; get; } = new();
        private List<Dependencia> Dependencias { set; get; } = new();
        private List<CausaInhabilitacion> Causas { set; get; } = new();
        private List<OrigenInhabilitacion> Origen { set; get; } = new();
        private List<Nivel> Nivel { set; get; } = new();
        private List<Moneda> Moneda { set; get; } = new();
        private List<TipoFalta> TipoFalta { set; get; } = new();
        private List<TipoInhabilitacion> ListTipoInhabilitacion { set; get; } = new();
        private List<TipoSancion> TipoSancion { set; get; } = new();
        public bool MostrarSpinnerInicial { set; get; }

        protected override async Task OnInitializedAsync()
        {
            MostrarSpinnerInicial = true;
            await CargarCatalogos();
            MostrarSpinnerInicial = false;
        }
        private async Task CargarCatalogos()
        {
            try
            {
                await CargarDependencias();
                await CargarCausas();
                await CargarOrigenes();
                await CargarNivel();
                await CargarMoneda();
                await CargarTipoFalta();
                await TipoInhabilitacion();
                await CargarTipoSancion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private async Task CargarDependencias() 
        {
            var listaDependencias = await httpClient.PostAsync("/api/Catalogos/CargasDependencias", null);

            if (listaDependencias.IsSuccessStatusCode)
            {
                var dependenciasResponde = await listaDependencias.Content.ReadFromJsonAsync<List<Dependencia>>();               
                Dependencias.AddRange(dependenciasResponde!);

                Console.WriteLine($"Dependencias - {Dependencias.Count()}");
            }
        }

        private async Task CargarCausas()
        {
            var listaDependencias = await httpClient.PostAsync("/api/Catalogos/CargasCausas", null);

            if (listaDependencias.IsSuccessStatusCode)
            {
                var CausasResponde = await listaDependencias.Content.ReadFromJsonAsync<List<CausaInhabilitacion>>();
                Causas = CausasResponde!;

                Console.WriteLine($"Causas - {Causas.Count()}");
            }
        }
        private async Task CargarOrigenes()
        {
            var listaDependencias = await httpClient.PostAsync("/api/Catalogos/CargasOrigen", null);

            if (listaDependencias.IsSuccessStatusCode)
            {
                var OrigenResponde = await listaDependencias.Content.ReadFromJsonAsync<List<OrigenInhabilitacion>>();
                Origen = OrigenResponde!;

                Console.WriteLine($"Origen - {Origen.Count()}");
            }
        }

        private async Task CargarNivel()
        {
            var listaDependencias = await httpClient.PostAsync("/api/Catalogos/CargasNivel", null);

            if (listaDependencias.IsSuccessStatusCode)
            {
                var dependenciasResponde = await listaDependencias.Content.ReadFromJsonAsync<List<Nivel>>();
                Nivel = dependenciasResponde!;

                Console.WriteLine($"Nivel - {Nivel.Count()}");
            }
        }

        private async Task CargarMoneda()
        {
            var listaDependencias = await httpClient.PostAsync("/api/Catalogos/CargasMoneda", null);

            if (listaDependencias.IsSuccessStatusCode)
            {
                var CausasResponde = await listaDependencias.Content.ReadFromJsonAsync<List<Moneda>>();
                Moneda = CausasResponde!;

                Console.WriteLine($"Moneda - {Moneda.Count()}");
            }
        }
        private async Task CargarTipoFalta()
        {
            var listaDependencias = await httpClient.PostAsync("/api/Catalogos/CargasTipoFalta", null);

            if (listaDependencias.IsSuccessStatusCode)
            {
                var OrigenResponde = await listaDependencias.Content.ReadFromJsonAsync<List<TipoFalta>>();
                TipoFalta = OrigenResponde!;

                Console.WriteLine($"TipoFalta - {TipoFalta.Count()}");
            }
        }
        private async Task TipoInhabilitacion()
        {
            var listaDependencias = await httpClient.PostAsync("/api/Catalogos/CargasTipoInhabilitacion", null);

            if (listaDependencias.IsSuccessStatusCode)
            {
                var CausasResponde = await listaDependencias.Content.ReadFromJsonAsync<List<TipoInhabilitacion>>();
                
                ListTipoInhabilitacion.AddRange(CausasResponde!);

                Console.WriteLine($"ListTipoInhabilitacion - {ListTipoInhabilitacion.Count()}");
            }
        }
        private async Task CargarTipoSancion()
        {
            var listaDependencias = await httpClient.PostAsync("/api/Catalogos/CargasTipoSancion", null);

            if (listaDependencias.IsSuccessStatusCode)
            {
                var OrigenResponde = await listaDependencias.Content.ReadFromJsonAsync<List<TipoSancion>>();
                TipoSancion = OrigenResponde!;

                Console.WriteLine($"TipoSancion - {TipoSancion.Count()}");
            }
        }

        private async Task GuardarInhabilitación() 
        {
            List<string> listaErrores = new();
            ErroresEncontrados.Clear();

            listaErrores = await registroInhabilitacionValidaciones.ValidarInformacion(RegistrarNuevaInhabilitacion);

            if (listaErrores.Count() > 0)
            {
                ErroresEncontrados = listaErrores;
                await MostrarModales(TipoModal.Errores);
            }
            else
            {
                listaErrores = await registroInhabilitacionValidaciones.GuardarDatos(RegistrarNuevaInhabilitacion);
                if (listaErrores.Count() > 0)
                {                   
                    ErroresEncontrados = listaErrores;
                    await MostrarModales(TipoModal.Errores);
                }
                else 
                {
                    //Existoso
                    ReiniciarCampos();
                }
            }

        }

        private void ReiniciarCampos() 
        {
            RegistrarNuevaInhabilitacion = new();
        }

        public enum TipoModal 
        {
            Errores = 1,
        }

        private async Task MostrarModales(TipoModal tipoModal) 
        {
            if (tipoModal == TipoModal.Errores) 
            {
                var options = new DialogOptions 
                {
                    CloseOnEscapeKey = true, 
                    FullWidth = true,
                    MaxWidth = MaxWidth.Small,
                    CloseButton = true
                };
                var parameters = new DialogParameters<ModalErrores>();
                parameters.Add(p => p.listaErrores, ErroresEncontrados);
                DialogService.Show<ModalErrores>("Simple Dialog", parameters, options);
            }
        }
    }
}
