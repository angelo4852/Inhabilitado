using ConstanciaNoInhabilitado.Client.Shared.Partial.Dialogs;
using ConstanciaNoInhabilitado.Shared.Entities.Login;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http;
using System.Net.Http.Json;

namespace ConstanciaNoInhabilitado.Client.Componentes.Admin.ComponentesAdmin.RegistrarInhabilitado
{
    partial class RegistrarInhabilitado
    {
        [Parameter]
        public ServidorPublico servidorPublico { set; get; } = new ServidorPublico();
        public ServidorPublico servidorPublicoRegistro { set; get; } = new ServidorPublico();
        private int idInhabilitado { get; set; }
        private string nombreInhabilitado { get; set; }
        private bool bandera { get; set; } = true;



        private List<string> stringsErrores { get; set; } = new List<string>();  
        private List<CategoriaRegistrarInhabilitado> Categorias { set; get; }
        private List<CategoriaRegistrarInhabilitado> CategoriasDefault = new List<CategoriaRegistrarInhabilitado>
        {
             new CategoriaRegistrarInhabilitado { Value = "1", Text = "Servidor Público"},
             new CategoriaRegistrarInhabilitado { Value = "2", Text = "Proovedor o Contratista"}
        };

        private string selectValueCategoria { set; get; }

        //protected override async Task OnInitializedAsync()
        //{

        //}

        public async Task Insertar()
        {

            if (selectValueCategoria.Equals("Servidor Público"))
            {
                Console.WriteLine("Servidor Público");
                //servidorPublicoRegistro = servidorPublico;
                await ValidaInfo(servidorPublico);
                if (stringsErrores.Count > 0)
                {
                    var options = new DialogOptions { CloseOnEscapeKey = true };
                    var parameters = new DialogParameters<ModalErrores>();
                    parameters.Add(p => p.listaErrores, stringsErrores);
                    DialogService.Show<ModalErrores>("Simple Dialog", parameters, options);
                }

                else
                {
                    var logueoResponse = await httpClient.PostAsJsonAsync<ServidorPublico>("/api/AdminRegistraInhabilitado/Create", servidorPublico);
                    var sesionUser = await logueoResponse.Content.ReadFromJsonAsync<ServidorPublico>();
                    servidorPublico.IdInhabilitado = sesionUser.IdInhabilitado;

                    if (sesionUser.banderaExiste)
                    {
                        bandera = sesionUser.banderaExiste;
                        idInhabilitado = sesionUser.IdInhabilitado;
                        sesionUser.Nombre = string.Empty;
                        sesionUser.ApellidoPaterno = string.Empty;
                        
                    }
                    else
                    {
                        bandera = sesionUser.banderaExiste;
                        idInhabilitado = sesionUser.IdInhabilitado;                       
                        
                    }
                    await LimpiaServidorPublico();
                    
                }

            }
            else 
            {
               
                if (servidorPublico.TypePersonProovedor.Equals("Fisica"))
                {
                    await ValidaInfoProveedores(servidorPublico);
                    Console.WriteLine(servidorPublico.TypePersonProovedor);
                    Console.WriteLine("Entro a proveedores" + " " + servidorPublico.TypePersonProovedor);

                    if (stringsErrores.Count > 0)
                    {
                        var options = new DialogOptions { CloseOnEscapeKey = true };
                        var parameters = new DialogParameters<ModalErrores>();
                        parameters.Add(p => p.listaErrores, stringsErrores);
                        DialogService.Show<ModalErrores>("Simple Dialog", parameters, options);
                    }

                    else
                    {
                        var logueoResponse = await httpClient.PostAsJsonAsync<ServidorPublico>("/api/AdminRegistraInhabilitado/Create", servidorPublico);
                        var sesionUser = await logueoResponse.Content.ReadFromJsonAsync<ServidorPublico>();
                        if (sesionUser.banderaExiste)
                        {
                            bandera = sesionUser.banderaExiste;
                            idInhabilitado = sesionUser.IdInhabilitado;
                            sesionUser.Nombre = string.Empty;
                            sesionUser.ApellidoPaterno = string.Empty; 
                            
                        }
                        else
                        {
                            bandera = sesionUser.banderaExiste;
                            idInhabilitado = sesionUser.IdInhabilitado;
                            
                        }
                        await LimpiaServidorPublico();
                       
                    }
                }
                else
                {
                    await ValidaInfoProveedores(servidorPublico);
                    Console.WriteLine(servidorPublico.TypePersonProovedor);
                    Console.WriteLine("Entro a proveedores" + " " + servidorPublico.TypePersonProovedor);

                    if (stringsErrores.Count > 0)
                    {
                        var options = new DialogOptions { CloseOnEscapeKey = true };
                        var parameters = new DialogParameters<ModalErrores>();
                        parameters.Add(p => p.listaErrores, stringsErrores);
                        DialogService.Show<ModalErrores>("Simple Dialog", parameters, options);
                    }

                    else
                    {
                        var logueoResponse = await httpClient.PostAsJsonAsync<ServidorPublico>("/api/AdminRegistraInhabilitado/Create", servidorPublico);
                        var sesionUser = await logueoResponse.Content.ReadFromJsonAsync<ServidorPublico>();

                        if (sesionUser.banderaExiste)
                        {
                            bandera = sesionUser.banderaExiste;
                            idInhabilitado = sesionUser.IdInhabilitado;
                            sesionUser.Nombre = string.Empty;
                            sesionUser.ApellidoPaterno = string.Empty;
                            //await Task.Delay(5000);
                            //bandera = false;
                        }
                        else
                        {
                            bandera = sesionUser.banderaExiste;
                            idInhabilitado = sesionUser.IdInhabilitado;
                        
                        }
                        await LimpiaServidorPublico();
                       
                    }
                }
                
            }          

        }

        private async Task MostrarFormulario()
        {

        }

        public async Task ValidaInfo(ServidorPublico servidorPublico) 
        {
            stringsErrores.Clear();
            List<string> listaErrores = new List<string>();            
            listaErrores.Clear();

            if (servidorPublico.Nombre == null ) 
            {
                listaErrores.Add("Falta Nombre del Servidor Publico");

            }
            if (servidorPublico.ApellidoPaterno == null)
            {
                listaErrores.Add("Falta Apellido Paterno del Servidor Publico");

            }
            if (servidorPublico.ApellidoMaterno == null)
            {
                listaErrores.Add("Falta Apellido Materno del Servidor Publico");

            }
            if (servidorPublico.CURP == null)
            {
                listaErrores.Add("Falta CURP del Servidor Publico");
            }
            if (servidorPublico.RFC == null)
            {
                listaErrores.Add("Falta RFC del Servidor Publico");

            }
            if (servidorPublico.idGenero == 0)
            {
                listaErrores.Add("Falta SEXO del Servidor Publico");

            }
            stringsErrores.AddRange(listaErrores);        
        }

        public async Task ValidaInfoProveedores(ServidorPublico servidorPublico)
        {
            stringsErrores.Clear();
            List<string> listaErrores = new List<string>();
            listaErrores.Clear();

            if (servidorPublico.Nombre == null )
            {
                listaErrores.Add("Falta Nombre del Servidor Publico");

            }
            if (servidorPublico.ApellidoPaterno == null && servidorPublico.TypePersonProovedor.Equals("Fisica"))
            {
                listaErrores.Add("Falta Apellido Paterno del Servidor Publico");

            }
            if (servidorPublico.ApellidoMaterno == null && servidorPublico.TypePersonProovedor.Equals("Fisica"))
            {
                listaErrores.Add("Falta Apellido Materno del Servidor Publico");

            }
            if (servidorPublico.CURP == null && servidorPublico.TypePersonProovedor.Equals("Fisica"))
            {
                listaErrores.Add("Falta CURP del Servidor Publico" );
            }
            if (servidorPublico.RFC == null )
            {
                listaErrores.Add("Falta RFC del Servidor Publico");

            }
            if (servidorPublico.idGenero == 0 )
            {
                listaErrores.Add("Falta SEXO del Servidor Publico");

            }
            stringsErrores.AddRange(listaErrores);
        }

        public async Task LimpiaServidorPublico()
        {
            servidorPublico.Nombre = string.Empty;
            servidorPublico.ApellidoPaterno = string.Empty;
            servidorPublico.ApellidoMaterno = string.Empty;
            servidorPublico.CURP = string.Empty;
            servidorPublico.RFC = string.Empty;
            servidorPublico.FechaCreacion = DateTime.Now;
            servidorPublico.FechaUltimaModificacion = DateTime.Now;
            servidorPublico.IdUsuario= 4;
            servidorPublico.idGenero = 3;

        }
    }
}
