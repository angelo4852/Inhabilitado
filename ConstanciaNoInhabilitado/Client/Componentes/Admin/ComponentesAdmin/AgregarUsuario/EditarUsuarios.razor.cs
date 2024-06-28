using ConstanciaNoInhabilitado.Client.Componentes.Admin.ComponentesAdmin.EditarInhabilitación;
using ConstanciaNoInhabilitado.Client.Shared.Partial.Dialogs;
using ConstanciaNoInhabilitado.Shared.Entities.Login;
using ConstanciaNoInhabilitado.Shared.Entities.RegistroInhabilitacion;
using MudBlazor;
using System.Net.Http.Json;

namespace ConstanciaNoInhabilitado.Client.Componentes.Admin.ComponentesAdmin.AgregarUsuario
{
    partial  class EditarUsuarios
    {
        private string RFC { set; get; }
        private List<Usuarios> inhabilitacionBDs { get; set; } = new();
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
            var listaDependencias = await httpClient.PostAsync("/api/AgregaUsuario/GetUsuarios", null);

            if (listaDependencias.IsSuccessStatusCode)
            {
                var dependenciasResponde = await listaDependencias.Content.ReadFromJsonAsync<List<Usuarios>>();
                inhabilitacionBDs.AddRange(dependenciasResponde!);

                Console.WriteLine($"inhabilitacionBDs - {inhabilitacionBDs.Count()}");
            }
        }

        private async Task EditarCausa(Usuarios inhabilitacionBD)
        {
            await Inhabilitacion(inhabilitacionBD);
        }

        public async Task Inhabilitacion(Usuarios inhabilitacionBD)
        {
            var options = new DialogOptions()
            {
                FullWidth = true,
                MaxWidth = MaxWidth.Medium,
                DisableBackdropClick = true,
                ClassBackground = "filterBackground",
                CloseButton = true
            };

            DialogParameters<ModalEditarUsuarios> parameters = new DialogParameters<ModalEditarUsuarios>()
            {
                {x => x.EditarInhabilitacion, inhabilitacionBD }
            };

            await dialogService.ShowAsync<ModalEditarUsuarios>("Simple Dialog", parameters, options);
        }

        private Func<Usuarios, object> _sortBy => x =>
        {
            if (_sortNameByLength)
                return x.Nombre.Length;
            else
                return x.ApellidoPaterno;
        };
        // quick filter - filter globally across multiple columns with the same input
        private Func<Usuarios, bool> _quickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if (x.Nombre.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.ApellidoPaterno.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.ApellidoPaterno.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.Usuario.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        };

     
        // events
        void RowClicked(DataGridRowClickEventArgs<Usuarios> args)
        {
            _events.Insert(0, $"Event = RowClick, Index = {args.RowIndex}, Data = {System.Text.Json.JsonSerializer.Serialize(args.Item)}");
        }

        void RowRightClicked(DataGridRowClickEventArgs<Usuarios> args)
        {
            _events.Insert(0, $"Event = RowRightClick, Index = {args.RowIndex}, Data = {System.Text.Json.JsonSerializer.Serialize(args.Item)}");
        }

        void SelectedItemsChanged(HashSet<Usuarios> items)
        {
            _events.Insert(0, $"Event = SelectedItemsChanged, Data = {System.Text.Json.JsonSerializer.Serialize(items)}");
        }
    }
}
