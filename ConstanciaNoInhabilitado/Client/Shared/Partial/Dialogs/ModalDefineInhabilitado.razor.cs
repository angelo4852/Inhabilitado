using ConstanciaNoInhabilitado.Shared.Entities.Login;
using ConstanciaNoInhabilitado.Shared.Entities.RegistroInhabilitacion;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace ConstanciaNoInhabilitado.Client.Shared.Partial.Dialogs
{
    partial class ModalDefineInhabilitado
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        private List<CategoriaRegistrarInhabilitado> CategoriasDefault = new List<CategoriaRegistrarInhabilitado>
        {
                 new CategoriaRegistrarInhabilitado { Value = "1", Text = "Servidor Público"},
                 new CategoriaRegistrarInhabilitado { Value = "2", Text = "Proovedor o Contratista"}
        };

        private string _rfc;
        private string Rfc
        {
            get => _rfc;
            set
            {
                _rfc = value.ToUpper();
            }
        }
        private bool MostrarParteAgregarNuevoServidorPublico {  get; set; }

        private string selectValueCategoria { set; get; }

        List<Inhabilitado> Inhabilitados { set; get; } = new();

        private async Task Buscar()
        {
            try
            {
                MostrarParteAgregarNuevoServidorPublico = false;
                Inhabilitados.Clear();
                HttpResponseMessage inhabilitadoResponde = await httpClient.GetAsync($"api/AgregarInhabilitación/GetInhabilitado?rfc={Rfc}");

                if (inhabilitadoResponde.IsSuccessStatusCode)
                {
                    Inhabilitado? dependenciasResponde = await inhabilitadoResponde.Content.ReadFromJsonAsync<Inhabilitado?>();
                    if (dependenciasResponde != null & dependenciasResponde!.RFC != string.Empty & dependenciasResponde!.RFC != null)
                    {
                        Inhabilitados.Add(dependenciasResponde);
                    }
                    else 
                    {
                        MostrarParteAgregarNuevoServidorPublico = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
          
            //return inhabilitado;
        }
        void Submit() => MudDialog.Close(DialogResult.Ok(Inhabilitados));
        void Cancel() => MudDialog.Cancel();
    }
}
