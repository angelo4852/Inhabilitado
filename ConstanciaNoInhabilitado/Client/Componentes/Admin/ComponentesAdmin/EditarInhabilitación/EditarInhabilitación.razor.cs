using ConstanciaNoInhabilitado.Client.Shared.Partial.Dialogs;
using ConstanciaNoInhabilitado.Shared.Entities.Catalogos;
using ConstanciaNoInhabilitado.Shared.Entities.RegistroInhabilitacion;
using MudBlazor;
using System.Net.Http;
using System.Net.Http.Json;
using static MudBlazor.CategoryTypes;

namespace ConstanciaNoInhabilitado.Client.Componentes.Admin.ComponentesAdmin.EditarInhabilitación
{
    partial class EditarInhabilitación
    {
        private string RFC { set; get; }
        private List<InhabilitacionADD> inhabilitacionBDs { get; set; } = new();
        private string _searchString { get; set; }
        private bool _sortNameByLength { get; set; }
        private List<string> _events { get; set; } = new();
        private bool MostrarSppinerLogin { get; set; }

        protected override async Task OnInitializedAsync() 
        {
            await CargarInformacionDeInicio();
        }

        private async Task CargarInformacionDeInicio() 
        {
            MostrarSppinerLogin = true;
            await ObtenerInhabalitacaiones();
            await Task.Delay(1000);
            MostrarSppinerLogin = false;
        }
        private async Task ObtenerInhabalitacaiones() 
        {
            var listaDependencias = await httpClient.PostAsync("/api/AgregarInhabilitación/GetInhabilitaciones", null);

            if (listaDependencias.IsSuccessStatusCode)
            {
                var dependenciasResponde = await listaDependencias.Content.ReadFromJsonAsync<List<InhabilitacionADD>>();
                inhabilitacionBDs.AddRange(dependenciasResponde!);

                Console.WriteLine($"inhabilitacionBDs - {inhabilitacionBDs.Count()}");
            }
        }

        private async Task EditarCausa(InhabilitacionADD inhabilitacionBD) 
        {
            await Inhabilitacion( inhabilitacionBD);
        }

        public async Task Inhabilitacion(InhabilitacionADD inhabilitacionBD)
        {
            var options = new DialogOptions()
            {
                FullWidth = true,
                MaxWidth = MaxWidth.Medium,
                DisableBackdropClick = true,
                ClassBackground = "filterBackground",
                CloseButton = true
            };

            DialogParameters<ModalEditarInhabilitación> parameters = new DialogParameters<ModalEditarInhabilitación>()
            {
                {x => x.EditarInhabilitacion, inhabilitacionBD }
            };

            await dialogService.ShowAsync<ModalEditarInhabilitación>("Simple Dialog", parameters,options);           
        }

        private Func<InhabilitacionADD, object> _sortBy => x =>
        {
            if (_sortNameByLength)
                return x.NombreInhabilitado.Length;
            else
                return x.NombreInhabilitado;
        };
        // quick filter - filter globally across multiple columns with the same input
        private Func<InhabilitacionADD, bool> _quickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if (x.RFC.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.NombreInhabilitado.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.ApellidoPaternoInhabilitado.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.ApellidoMaternoInhabilitado.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        };       

        // events
        void RowClicked(DataGridRowClickEventArgs<InhabilitacionADD> args)
        {
            _events.Insert(0, $"Event = RowClick, Index = {args.RowIndex}, Data = {System.Text.Json.JsonSerializer.Serialize(args.Item)}");
        }

        void RowRightClicked(DataGridRowClickEventArgs<InhabilitacionADD> args)
        {
            _events.Insert(0, $"Event = RowRightClick, Index = {args.RowIndex}, Data = {System.Text.Json.JsonSerializer.Serialize(args.Item)}");
        }

        void SelectedItemsChanged(HashSet<InhabilitacionADD> items)
        {
            _events.Insert(0, $"Event = SelectedItemsChanged, Data = {System.Text.Json.JsonSerializer.Serialize(items)}");
        }
    }
}
