using ConstanciaNoInhabilitado.Shared.Entities.Login;
using MudBlazor;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using static MudBlazor.CategoryTypes;

namespace ConstanciaNoInhabilitado.Client.Componentes.Admin.ComponentesAdmin
{
    partial  class EditarInhabilitados
    {
        //private HttpClient? _httpClient = new(); esta no es una variable es un inject 

        public string TextValue { get; set; }
        public string selectValueCategoria { get; set; }
        HttpResponseMessage? _http = new HttpResponseMessage();
        private BuscarServidorPublico BuscarServidorPublico { get; set; } = new();
        public List<ServidorPublico>? Inhabilitado { get; set; }
        public ServidorPublico servidorPublico { set; get; } = new ServidorPublico();

        private List<CriterioDeBusqueda> ListCriterio = new List<CriterioDeBusqueda>
        {
        new CriterioDeBusqueda { Value = "1", Text = "R.F.C." },
        new CriterioDeBusqueda { Value = "2", Text = "Nombre" },
        new CriterioDeBusqueda { Value = "3", Text = "Apellido Paterno" },
        new CriterioDeBusqueda { Value = "4", Text = "Apellido Materno" }
        };

        private string _searchString;
        private bool _sortNameByLength;
        private List<string> _events = new();
        private List<string> ErorresEncontrados { get; set; } = new();
        private int totalItems;
        private string searchString = null;
        private IEnumerable<ServidorPublico> pagedData;
        private MudTable<ServidorPublico> table;
        private bool _hidePosition;
        private bool _loading;
        private List<ServidorPublico> Elements { set; get; }
        private TableState? _table;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await CargarServidoresPublicos();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex}");
            }

        }

        private async Task CargarServidoresPublicos() 
        {
            //Elements = await Http!.GetFromJsonAsync<List<ServidorPublico>>($"/api/AdminRegistraInhabilitado/SelectInhabilitado");
            //_http = await Http!.GetAsync($"/api/AdminRegistraInhabilitado/SelectInhabilitado");
            //Inhabilitado = await _http.Content.ReadFromJsonAsync<List<ServidorPublico>>();
            var responde_Servidores_publicos = await Http!.PostAsync($"/api/AdminRegistraInhabilitado/SelectInhabilitado", null);
            //_http = await Http!.PostAsync($"/api/AdminRegistraInhabilitado/SelectInhabilitado", null);

            if (responde_Servidores_publicos.IsSuccessStatusCode)
            {
                List<ServidorPublico> process = await responde_Servidores_publicos.Content.ReadFromJsonAsync<List<ServidorPublico>>();

                if (process.Count > 0)
                {
                    Elements = process;
                }
            }
        }

        private async Task ValidarInformacion()
        {
            ErorresEncontrados.Clear();
            if (BuscarServidorPublico.TipoCriterioBúsqueda == string.Empty) ErorresEncontrados.Add("Selecciona un criterio de búsqueda");
            if (BuscarServidorPublico.Criterio == string.Empty) ErorresEncontrados.Add("Introduce el criterio de búsqueda");
            if (BuscarServidorPublico.TipoCriterioBúsqueda == "R.F.C.")
            {
                if (BuscarServidorPublico.Criterio.Length != 13) ErorresEncontrados.Add("El RFC introducido no es valido");
            }
            if (ErorresEncontrados.Count() == 0) await BuscarServidor();
        }

        private async Task BuscarServidor()
        {

        }
        private bool FilterFunc(ServidorPublico element)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.Nombre.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.ApellidoPaterno.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if ($"{element.RFC} {element.CURP} ".Contains(searchString))
                return true;
            return false;
        }



        private void OnSearch(string text)
        {
            searchString = text;
            table.ReloadServerData();
        }

        private async Task Update(ServidorPublico _servidorPublico) 
        {
            Console.WriteLine(_servidorPublico.IdInhabilitado);
            try 
            {

                Console.WriteLine($"Nombre:{_servidorPublico.Nombre}");
                Console.WriteLine($"ApellidoPaterno:{_servidorPublico.ApellidoPaterno}");
                Console.WriteLine($"ApellidoMaterno:{_servidorPublico.ApellidoMaterno}");
                Console.WriteLine($"CURP:{_servidorPublico.CURP}");
                Console.WriteLine($"RFC:{_servidorPublico.RFC}");
                Console.WriteLine($"Tipo:{_servidorPublico.Tipo}");
                Console.WriteLine($"FechaCreacion:{_servidorPublico.FechaCreacion}");
                Console.WriteLine($"FechaUltimaModificacion:{_servidorPublico.FechaUltimaModificacion}");
                Console.WriteLine($"IdUsuario:{_servidorPublico.IdUsuario}");
                Console.WriteLine($"idGenero:{_servidorPublico.idGenero}");
                Console.WriteLine($"IdInhabilitado:{_servidorPublico.IdInhabilitado}");

                if(_servidorPublico.CURP == null) _servidorPublico.CURP=string.Empty;
                if(_servidorPublico.idGenero == null) _servidorPublico.idGenero = 0;


                string data = JsonSerializer.Serialize(_servidorPublico, new JsonSerializerOptions { WriteIndented = true });
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                Console.WriteLine($"Json:{data}");

                //HttpResponseMessage response = await _httpClient!.PostAsync($"/api/AdminRegistraInhabilitado/SelectInhabilitadoUpdate", content);
                HttpResponseMessage response = await Http!.PostAsync($"/api/AdminRegistraInhabilitado/SelectInhabilitadoUpdate", content);

                //var logueoResponse = await _httpClient!.PostAsJsonAsync<ServidorPublico>("/api/AdminRegistraInhabilitado/SelectInhabilitadoUpdate", _servidorPublico);
                //_http = await Http!.PostAsync($"/api/AdminRegistraInhabilitado/SelectInhabilitado", null);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($" Entro al response.IsSuccessStatusCode:true");
                }
                //var logueoResponse = await _httpClient!.PostAsJsonAsync<ServidorPublico>("/api/AdminRegistraInhabilitado/SelectInhabilitadoUpdate", servidorPublico);

                //_http = await _httpClient!.GetAsync($"/api/AdminRegistraInhabilitado/SelectInhabilitadoUpdate?IdInhabilitado={id}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            
            }

            
        
        }
    }
}
