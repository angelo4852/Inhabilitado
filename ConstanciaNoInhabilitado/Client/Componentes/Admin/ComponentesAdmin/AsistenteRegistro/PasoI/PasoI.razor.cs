using ConstanciaNoInhabilitado.Client.Shared.Partial.Dialogs;
using ConstanciaNoInhabilitado.Shared.Entities.RegistroInhabilitacion;
using MudBlazor;

namespace ConstanciaNoInhabilitado.Client.Componentes.Admin.ComponentesAdmin.AsistenteRegistro.PasoI
{
    partial class PasoI
    {
        private Inhabilitado inhabilitado { set; get; }

        public async Task Inhabilitado()
        {
            try
            {
                var options = new DialogOptions()
                {
                    FullWidth = true,
                    MaxWidth = MaxWidth.Medium,
                    DisableBackdropClick = true,
                    ClassBackground = "filterBackground",
                    CloseButton = true
                };
                var dialogReference = await dialogService.ShowAsync<ModalDefineInhabilitado>("Simple Dialog", options);

                var result = await dialogReference.Result;

                if (!result.Canceled)
                {
                    inhabilitado = new();
                    var responde = result.Data;
                    List<Inhabilitado> inhabilitados = new();
                    inhabilitados.AddRange((List<Inhabilitado>)responde);
                    inhabilitado = inhabilitados.First();
                    Console.WriteLine($" RFC {inhabilitado.RFC}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
