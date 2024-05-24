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
using System.Text.Json.Serialization;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Linq.Expressions;
using System.Globalization;

namespace ConstanciaNoInhabilitado.Client.Componentes.Admin.ComponentesAdmin.AgregarInhabilitación
{
    partial class FormAgregarInhabilitación
    {
        [Parameter] public bool DefineInhabilitacion { set; get; } = false;
        private RegistrarNuevaInhabilitacion RegistrarNuevaInhabilitacion { set; get; } = new();
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

            listaErrores = await ValidarInformacion(RegistrarNuevaInhabilitacion);

            if (listaErrores.Count() > 0)
            {
                ErroresEncontrados = listaErrores;
                await MostrarModales(TipoModal.Errores);
            }
            else
            {
                listaErrores = await GuardarDatos(RegistrarNuevaInhabilitacion);
                if (listaErrores.Count() > 0)
                {
                    ErroresEncontrados = listaErrores;
                    await MostrarModales(TipoModal.Errores);
                }
                else
                {
                    if (ErroresEncontrados.Count() > 0)
                    {
                        Snackbar.Add($"Error al registrar el RFC {RegistrarNuevaInhabilitacion.RFC} .", Severity.Error);
                    }
                    else
                    {
                        Snackbar.Add($"RFC {RegistrarNuevaInhabilitacion.RFC} inhabilitado correctamente.", Severity.Success);
                        //Existoso
                        ReiniciarCampos();
                    }
                }
            }
        }

        private void ReiniciarCampos()
        {
            RegistrarNuevaInhabilitacion = new();
        }

        public async Task<List<string>> ValidarInformacion(RegistrarNuevaInhabilitacion RegistrarNuevaInhabilitacion)
        {
            List<string> listaErrores = new();

            if (RegistrarNuevaInhabilitacion.FaltaCometida == 0 || RegistrarNuevaInhabilitacion.FaltaCometida == null)
            {
                listaErrores.Add("El campo de falta cometida es obligatorio.");
            }
            if (RegistrarNuevaInhabilitacion.RFC == string.Empty || RegistrarNuevaInhabilitacion.RFC == null)
            {
                listaErrores.Add("El campo de RFC es obligatorio.");
            }
            if (RegistrarNuevaInhabilitacion.AutoridadSancionadora == string.Empty || RegistrarNuevaInhabilitacion.AutoridadSancionadora == null)
            {
                listaErrores.Add("El campo de autoridad sancionadora es obligatorio.");
            }
            if (RegistrarNuevaInhabilitacion.Dependencia == 0 || RegistrarNuevaInhabilitacion.Dependencia == null)
            {
                listaErrores.Add("El campo de dependencia es obligatorio.");
            }
            if (RegistrarNuevaInhabilitacion.FechaInicio == null)
            {
                listaErrores.Add("El campo de fecha inicio es obligatorio.");
            }
            if (RegistrarNuevaInhabilitacion.FechaTermino == null)
            {
                listaErrores.Add("El campo de fecha termino es obligatorio.");
            }
            if (RegistrarNuevaInhabilitacion.FechaResolución == null)
            {
                listaErrores.Add("El campo de fecha resolución es obligatorio.");
            }
            if (RegistrarNuevaInhabilitacion.Periodo == string.Empty || RegistrarNuevaInhabilitacion.Periodo == null)
            {
                listaErrores.Add("El campo de periodo es obligatorio.");
            }
            if (RegistrarNuevaInhabilitacion.ExpedienteLegal == string.Empty || RegistrarNuevaInhabilitacion.ExpedienteLegal == null)
            {
                listaErrores.Add("El campo de expediente legal es obligatorio.");
            }
            if (RegistrarNuevaInhabilitacion.TipoInhabilitación == 0 || RegistrarNuevaInhabilitacion.TipoInhabilitación == null)
            {
                listaErrores.Add("El campo de tipo inhabilitación es obligatorio.");
            }
            if (RegistrarNuevaInhabilitacion.OrigenInhabilitación == 0 || RegistrarNuevaInhabilitacion.OrigenInhabilitación == null)
            {
                listaErrores.Add("El campo de origen inhabilitación es obligatorio.");
            }
            if (RegistrarNuevaInhabilitacion.CausaInhabilitación == 0 || RegistrarNuevaInhabilitacion.CausaInhabilitación == null)
            {
                listaErrores.Add("El campo de causa inhabilitación es obligatorio.");
            }
            if (RegistrarNuevaInhabilitacion.TipoSanción == 0 || RegistrarNuevaInhabilitacion.TipoSanción == null)
            {
                listaErrores.Add("El campo de tipo sanción es obligatorio.");
            }
            if (RegistrarNuevaInhabilitacion.TipoMoneda == 0 || RegistrarNuevaInhabilitacion.TipoMoneda == null)
            {
                listaErrores.Add("El campo de tipo moneda es obligatorio.");
            }
            if (RegistrarNuevaInhabilitacion.Monto == 0 || RegistrarNuevaInhabilitacion.Monto == null)
            {
                listaErrores.Add("El campo de monto es obligatorio.");
            }
            if (RegistrarNuevaInhabilitacion.ClaveNivelServidorPúblico == 0 || RegistrarNuevaInhabilitacion.ClaveNivelServidorPúblico == null)
            {
                listaErrores.Add("El campo de clave nivel servidor público es obligatorio.");
            }
            if (RegistrarNuevaInhabilitacion.DescripciónInhabilitación == string.Empty || RegistrarNuevaInhabilitacion.DescripciónInhabilitación == null)
            {
                listaErrores.Add("El campo de descripción inhabilitación es obligatorio.");
            }
            return listaErrores;
        }

        public async Task<List<string>> GuardarDatos(RegistrarNuevaInhabilitacion RegistrarNuevaInhabilitacion)
        {
            List<string> listaErrores = new();
            if (Security.ValidarRFC(RegistrarNuevaInhabilitacion.RFC))
            {
                List<Inhabilitado> inhabilitado = await GetInhabilitado(RegistrarNuevaInhabilitacion.RFC);
                if (inhabilitado != null)
                {
                    if (inhabilitado.Count() > 0)
                    {
                        Inhabilitacion inhabilitacion = new Inhabilitacion();
                        InhabilitacionDTO inhabilitacionDTO = new InhabilitacionDTO();
                        if (RegistrarNuevaInhabilitacion.InhabilitacionProceso == 0)
                        {
                            inhabilitacionDTO.IdInhabilitacion = 0;
                            inhabilitacionDTO.IdInhabilitado = inhabilitado[0].IdInhabilitado;
                            inhabilitacionDTO.IdUsuario = RegistrarNuevaInhabilitacion.idUsuario;
                            inhabilitacionDTO.Estatus = RegistrarNuevaInhabilitacion.Estatus;
                            inhabilitacionDTO.EnProceso = RegistrarNuevaInhabilitacion.InhabilitacionProceso;
                            inhabilitacionDTO.RFC = RegistrarNuevaInhabilitacion.RFC;
                            inhabilitacionDTO.Autoridad = RegistrarNuevaInhabilitacion.AutoridadSancionadora;
                            inhabilitacionDTO.IdDependencia = RegistrarNuevaInhabilitacion.Dependencia;
                            inhabilitacionDTO.Cargo = RegistrarNuevaInhabilitacion.Puesto;
                            inhabilitacionDTO.FechaInicio = await ConvertirFecha(RegistrarNuevaInhabilitacion.FechaInicio);
                            inhabilitacionDTO.FechaTermino = await ConvertirFecha(RegistrarNuevaInhabilitacion.FechaTermino);
                            inhabilitacionDTO.Periodo = RegistrarNuevaInhabilitacion.Periodo;
                            inhabilitacionDTO.FechaResolucion = await ConvertirFecha(RegistrarNuevaInhabilitacion.FechaResolución);
                            inhabilitacionDTO.ExpedienteLegal = RegistrarNuevaInhabilitacion.ExpedienteLegal.ToUpper();
                            inhabilitacionDTO.IdTipoInhabilitacion = RegistrarNuevaInhabilitacion.TipoInhabilitación;
                            inhabilitacionDTO.IdOrigenInhabilitacion = RegistrarNuevaInhabilitacion.OrigenInhabilitación;
                            inhabilitacionDTO.IdCausaInhabilitacion = RegistrarNuevaInhabilitacion.CausaInhabilitación;
                            inhabilitacionDTO.IdTipoFalta = RegistrarNuevaInhabilitacion.FaltaCometida;
                            inhabilitacionDTO.IdTipoSancion = RegistrarNuevaInhabilitacion.TipoSanción;
                            inhabilitacionDTO.IdMoneda = RegistrarNuevaInhabilitacion.TipoMoneda;
                            inhabilitacionDTO.Monto = RegistrarNuevaInhabilitacion.Monto;
                            inhabilitacionDTO.IdNivel = RegistrarNuevaInhabilitacion.ClaveNivelServidorPúblico;
                            inhabilitacionDTO.Descripcion = RegistrarNuevaInhabilitacion.DescripciónInhabilitación;
                        }
                        else
                        {
                            inhabilitacionDTO.IdInhabilitacion = 0;
                            inhabilitacionDTO.IdInhabilitado = inhabilitado[0].IdInhabilitado;
                            inhabilitacionDTO.ExpedienteLegal = "En proceso";
                            inhabilitacionDTO.IdDependencia = 92;
                            inhabilitacionDTO.Cargo = "No definido";
                            inhabilitacionDTO.FechaInicio = await formatoFecha(DateTime.Today.ToShortDateString());
                            inhabilitacionDTO.FechaTermino = await formatoFecha(DateTime.Today.ToShortDateString());
                            inhabilitacionDTO.Periodo = "No definido";
                            inhabilitacionDTO.FechaResolucion = await formatoFecha(DateTime.Today.ToShortDateString());
                            inhabilitacionDTO.Autoridad = RegistrarNuevaInhabilitacion.AutoridadSancionadora;
                            inhabilitacionDTO.IdTipoInhabilitacion = Convert.ToInt32(RegistrarNuevaInhabilitacion.TipoInhabilitación);
                            inhabilitacionDTO.IdCausaInhabilitacion = 8;
                            inhabilitacionDTO.IdOrigenInhabilitacion = 8;
                            inhabilitacionDTO.Descripcion = "NA";
                            inhabilitacionDTO.IdUsuario = RegistrarNuevaInhabilitacion.idUsuario;
                            inhabilitacionDTO.Estatus = RegistrarNuevaInhabilitacion.Estatus;
                            inhabilitacionDTO.EnProceso = RegistrarNuevaInhabilitacion.InhabilitacionProceso;
                        }

                        var errores = await ValidarDatos(inhabilitacionDTO, listaErrores);
                        if (errores.Count() == 0)
                        {
                            await RegistrarInhabilitado(inhabilitacion);
                        }
                    }
                    else
                        listaErrores.Add("No se recuperó ningun registro del RFC " + RegistrarNuevaInhabilitacion.RFC);
                }
                else
                    listaErrores.Add("No se recuperaron datos del servicio");
            }
            else
                listaErrores.Add("El RFC " + RegistrarNuevaInhabilitacion.RFC + " no es valido");
            return listaErrores;
        }

        private async Task<List<string>> ValidarDatos(InhabilitacionDTO inhabilitacion, List<string> errores)
        {
            try
            {
                if (inhabilitacion.FechaInicio.Equals("") || inhabilitacion.FechaTermino.Equals("") || inhabilitacion.IdInhabilitado == 0 || inhabilitacion.Cargo.Equals("") ||
                    inhabilitacion.Periodo.Equals("") || inhabilitacion.FechaResolucion.Equals("") || inhabilitacion.Autoridad.Equals("") || inhabilitacion.IdTipoInhabilitacion == -1 ||
                    inhabilitacion.IdOrigenInhabilitacion == -1 || inhabilitacion.IdCausaInhabilitacion == -1 || inhabilitacion.Descripcion.Equals("") || inhabilitacion.ExpedienteLegal.Equals("") ||
                    inhabilitacion.IdDependencia == -1)
                {
                    errores.Add("Todos los campos son necesarios para proceder con el registro.");
                }
                else
                {
                    try
                    {
                        if (inhabilitacion.EnProceso == 0)
                        {
                            DateTime fi = DateTime.Parse(inhabilitacion.FechaInicio);
                            DateTime ft = DateTime.Parse(inhabilitacion.FechaTermino);
                            DateTime fr = DateTime.Parse(inhabilitacion.FechaResolucion);
                            int result = DateTime.Compare(fi, ft);
                            if (result >= 0)
                            {
                                errores.Add("La Fecha de Inicio debe ser menor a la Fecha de Termino.");
                            }
                        }
                    }
                    catch
                    {
                        errores.Add("Las fechas no tienen el formato correcto.");
                    }

                    if (inhabilitacion.ExpedienteLegal.Trim().Equals(""))
                        errores.Add("El expediente legal no puede estar vacio");

                    if (!Security.EsAlfaNumerico(inhabilitacion.Descripcion))
                    {
                        errores.Add("La descripción de la inhabilitación solo debe tener letras y números");
                    }

                    if (!Security.EsAlfaNumerico(inhabilitacion.Periodo))
                    {
                        errores.Add("El periodo solo puede tener letras y números");
                    }

                    if (inhabilitacion.EnProceso == 0)
                    {
                        DateTime fecha1 = DateTime.Parse(inhabilitacion.FechaInicio);
                        DateTime fecha2 = DateTime.Parse(inhabilitacion.FechaTermino);

                        if (DateTime.Compare(fecha1, fecha2) >= 0)
                        {
                            errores.Add("La fecha de inicio debe ser mayor a la fecha de termino");
                        }
                    }
                }

                return errores;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return errores;
            }

        }

        private async Task<string> formatoFecha(string fecha)
        {
            try
            {
                string[] v = fecha.Split(new char[] { '-' });
                return v[2] + "/" + v[1] + "/" + v[0];
            }
            catch
            {
                return fecha;
            }
        }

        private async Task<string> ConvertirFecha(DateTime? fecha)
        {
            string fechaConvertida = string.Empty;
            DateTime dateTime = (DateTime)fecha!;
            fechaConvertida = dateTime.ToString("yyyy-MM-dd");
            return fechaConvertida;
        }

        public async Task RegistrarInhabilitado(Inhabilitacion inhabilitacion)
        {
            try
            {
                List<Inhabilitado> inhabilitado = new();

                HttpResponseMessage inhabilitadoResponde = await httpClient.PostAsJsonAsync<Inhabilitacion>($"api/AgregarInhabilitación/CreateInhabilitacion", inhabilitacion);

                if (inhabilitadoResponde.IsSuccessStatusCode)
                {
                    Inhabilitacion? responde = await inhabilitadoResponde.Content.ReadFromJsonAsync<Inhabilitacion>();
                    if (responde != null)
                    {
                        if (responde.IdInhabilitacion > 0)
                        {
                            Snackbar.Add($"RFC {RegistrarNuevaInhabilitacion.RFC} se registro correctamente.", Severity.Success);
                            //Existoso
                            ReiniciarCampos();
                        }
                        else
                        {
                            Snackbar.Add($"Error con el RFC {RegistrarNuevaInhabilitacion.RFC} al registrar.", Severity.Error);
                        }

                    }
                    else
                    {
                        Snackbar.Add($"Error con el RFC {RegistrarNuevaInhabilitacion.RFC} al registrar.", Severity.Error);
                    }
                }
                else
                {
                    ErroresEncontrados.Add("Error: no se encontro el metodo en la busqueda de api.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Inhabilitado>> GetInhabilitado(string criterio)
        {
            List<Inhabilitado> inhabilitado = new();

            HttpResponseMessage inhabilitadoResponde = await httpClient.GetAsync($"api/AgregarInhabilitación/GetInhabilitado?rfc={criterio}");

            if (inhabilitadoResponde.IsSuccessStatusCode)
            {
                Inhabilitado? dependenciasResponde = await inhabilitadoResponde.Content.ReadFromJsonAsync<Inhabilitado>();
                if (dependenciasResponde != null)
                {
                    inhabilitado.Add(dependenciasResponde);
                }
            }
            return inhabilitado;
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
