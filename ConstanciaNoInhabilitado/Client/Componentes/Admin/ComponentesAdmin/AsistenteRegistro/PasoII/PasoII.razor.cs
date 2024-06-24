using ConstanciaNoInhabilitado.Client.Shared.Partial.Dialogs;
using ConstanciaNoInhabilitado.Shared.Entities.RegistroInhabilitacion;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ConstanciaNoInhabilitado.Client.Componentes.Admin.ComponentesAdmin.AsistenteRegistro.PasoII
{
    partial class PasoII
    {
        [Parameter]
        public Inhabilitado InhabilitadoPasoII { set; get; }
        public InhabilitacionDTO InhabilitacionDTOResponde { set; get; }

        [Parameter] public EventCallback<InhabilitacionDTO> EventCallbackInhabilitacionDTO { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (InhabilitadoPasoII is not null) Console.WriteLine($"InhabilitadoPasoII {InhabilitadoPasoII.RFC}");
        }

        public async Task Dependencia()
        {
            var options = new DialogOptions()
            {
                FullWidth = true,
                MaxWidth = MaxWidth.Small,
                DisableBackdropClick = true,
                ClassBackground = "filterBackground",
                CloseButton = true
            };
            await dialogService.ShowAsync<ModalRegistrarDependencia>("Simple Dialog", options);
        }

        public async Task Inhabilitacion()
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

                DialogParameters<ModalDefineLaInhabilitación> parameters = new DialogParameters<ModalDefineLaInhabilitación>()
                {
                    {x => x.InhabilitadoModalDefineInhabilitacion, InhabilitadoPasoII }
                };

                var dialogReference = await dialogService.ShowAsync<ModalDefineLaInhabilitación>("Simple Dialog", parameters,options);

                var result = await dialogReference.Result;

                if (!result.Canceled)
                {
                    InhabilitacionDTOResponde = (InhabilitacionDTO)result.Data;                   
                    await EventCallbackInhabilitacionDTO.InvokeAsync(InhabilitacionDTOResponde);
                    Console.WriteLine($" InhabilitacionDTOResponde {InhabilitacionDTOResponde.RFC} ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
           
        }
    }
}
