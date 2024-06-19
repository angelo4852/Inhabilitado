using ConstanciaNoInhabilitado.Client.Auth;
using ConstanciaNoInhabilitado.Shared.Entities.Login;

namespace ConstanciaNoInhabilitado.Client.Componentes.Menu
{
    partial class Menu
    {
        public List<MenuOpciones> MenuGeneral { set; get; } = new();
        private Session SessionUser { set; get; }
        protected override async Task OnInitializedAsync()
        {
            SessionUser = await _sessionStorage.GetItemAsync<Session>("sesionUser");
            await CargarInformacion();
           
        }

        private async Task CargarInformacion()
        {
            Console.WriteLine($"{SessionUser.Rol}");
            int tipoRol = Convert.ToInt32(SessionUser.Rol);
            var menuInicial = new List<MenuOpciones> {
                 new MenuOpciones {TipoUser = new List<TipoUsuario>{TipoUsuario.Supervisor  } ,TipoMenu = TipoMenu.Admin, Image = "img/Menu/1.png", Title ="Registrar Inhabilitado", Description ="Agrega una nueva persona física, moral o servidor público.", Url ="ConstanciaNoInhabilitado/Admin/RegistrarInhabilitado"},
                 new MenuOpciones {TipoUser = new List<TipoUsuario>{TipoUsuario.Supervisor } ,TipoMenu = TipoMenu.Admin, Image = "img/Menu/2.png", Title ="Editar Inhabilitados", Description ="Edita los registros de una persona física, moral o servidor público", Url ="ConstanciaNoInhabilitado/Admin/EditarInhabilitado"},
                 new MenuOpciones {TipoUser = new List<TipoUsuario>{TipoUsuario.Supervisor  } ,TipoMenu = TipoMenu.Admin, Image = "img/Menu/3.png", Title ="Agregar Inhabilitación", Description ="Registra una nueva inhabilitación a una persona fisica, moral o servidor público", Url ="ConstanciaNoInhabilitado/Admin/RegistrarInhabilitacion"},
                 new MenuOpciones {TipoUser = new List<TipoUsuario>{TipoUsuario.Supervisor  } ,TipoMenu = TipoMenu.Admin, Image = "img/Menu/4.png", Title ="Editar Inhabilitación", Description ="Selecciona y edita las inhabilitaciones asociadas a un inhabilitado.", Url ="ConstanciaNoInhabilitado/Admin/EditarInhabilitacion"},
                 new MenuOpciones {TipoUser = new List<TipoUsuario>{TipoUsuario.Supervisor  } ,TipoMenu = TipoMenu.Admin, Image = "img/Menu/6.png", Title ="Asistente de Registro", Description ="Secuencia para dar de alta una inhabilitación.", Url ="ConstanciaNoInhabilitado/Admin/Asistente"},
                 new MenuOpciones {TipoUser = new List<TipoUsuario>{TipoUsuario.Administrador, TipoUsuario.Supervisor ,TipoUsuario.Analista,TipoUsuario.Ninguno } ,TipoMenu = TipoMenu.Admin, Image = "img/Menu/7.png", Title ="Ver Inhabilitaciones", Description ="Muestra las inhabilitaciones de un servidor público.", Url ="ConstanciaNoInhabilitado/Admin/VerInhabilitacion"},
                 new MenuOpciones {TipoUser = new List<TipoUsuario>{TipoUsuario.Administrador, TipoUsuario.Supervisor ,TipoUsuario.Analista,TipoUsuario.Ninguno } ,TipoMenu = TipoMenu.Reporte, Image = "img/Menu/11.png", Title ="Reportes de Constancias", Description ="Genera informes de las constancias de no inhabilitado.", Url ="ConstanciaNoInhabilitado/Admin/ReporteConstancias"},
                 new MenuOpciones {TipoUser = new List<TipoUsuario>{TipoUsuario.Administrador, TipoUsuario.Supervisor ,TipoUsuario.Analista,TipoUsuario.Ninguno } ,TipoMenu = TipoMenu.Reporte, Image = "img/Menu/8.png", Title ="Reportes Inhabilitaciones", Description ="Genera informes de altas y bajas de inhabilitaciones", Url ="ConstanciaNoInhabilitado/Admin/ReporteInhabilitaciones"},
                 // new MenuOpciones {TipoMenu = TipoMenu.Reporte, Image = "img/Menu/5.png", Title ="Consultar Constancias", Description ="Lista las constancias expedidas a un R.F.C.", Url ="ConstanciaNoInhabilitado/Admin/ConsultarConstancias"},
                 new MenuOpciones {TipoUser = new List<TipoUsuario>{TipoUsuario.Supervisor  } ,TipoMenu = TipoMenu.Catalogo, Image = "img/Menu/10.png", Title ="Registrar Dependencia ", Description ="Registra una nueva dependencia de gobierno", Url ="ConstanciaNoInhabilitado/Admin/RegistrarDependencia"},
                 new MenuOpciones {TipoUser = new List<TipoUsuario>{TipoUsuario.Supervisor } ,TipoMenu = TipoMenu.Catalogo, Image = "img/Menu/3.png", Title ="Registrar Causa ", Description ="Registra una nueva causa de inhabilitación", Url ="ConstanciaNoInhabilitado/Admin/RegistrarCausa"},
                 new MenuOpciones {TipoUser = new List<TipoUsuario>{TipoUsuario.Supervisor  } ,TipoMenu = TipoMenu.Catalogo, Image = "img/Menu/3.png", Title ="Registrar Origen ", Description ="Registra un nuevo origen de inhabilitación", Url ="ConstanciaNoInhabilitado/Admin/RegistrarOrigen"},
                 new MenuOpciones {TipoUser = new List<TipoUsuario>{TipoUsuario.Supervisor  } ,TipoMenu = TipoMenu.Catalogo, Image = "img/Menu/4.png", Title ="Editar Causas ", Description ="Edita los registros de causas de inhabilitación", Url ="ConstanciaNoInhabilitado/Admin/EditarCausa"},
                 new MenuOpciones {TipoUser = new List<TipoUsuario>{TipoUsuario.Supervisor  } ,TipoMenu = TipoMenu.Catalogo, Image = "img/Menu/4.png", Title ="Editar Origen ", Description ="Edita los registros de origen de inhabilitacion", Url ="ConstanciaNoInhabilitado/Admin/EditarOrigen"},
                 new MenuOpciones {TipoUser = new List<TipoUsuario>{TipoUsuario.Supervisor  } ,TipoMenu = TipoMenu.Admin, Image = "img/Menu/17.png", Title ="Cargar desde Excel", Description ="Carga la lista de inhabilitados de la federación.", Url ="ConstanciaNoInhabilitado/Admin/CargarExcel"},
                 new MenuOpciones {TipoUser = new List<TipoUsuario>{TipoUsuario.Administrador } ,TipoMenu = TipoMenu.Admin, Image = "img/Menu/15.png", Title ="Registrar Usuarios", Description ="Dar de alta nuevos usuarios del sistema.", Url ="ConstanciaNoInhabilitado/Admin/AgregaUsuario"},
                 new MenuOpciones {TipoUser = new List<TipoUsuario>{TipoUsuario.Administrador } ,TipoMenu = TipoMenu.Admin, Image = "img/Menu/16.png", Title ="Editar Usuarios", Description ="Modifica las Propiedades de los usuarios del sistema.", Url ="ConstanciaNoInhabilitado/Admin/EditaUsuario"},
                 new MenuOpciones {TipoUser = new List<TipoUsuario>{TipoUsuario.Supervisor  } ,TipoMenu = TipoMenu.Catalogo, Image = "img/Menu/13.png", Title ="Editar Dependencias ", Description ="Edita los registros de dependencias de gobierno", Url ="ConstanciaNoInhabilitado/Admin/EditarDependencia"}
           };          

            var menuFiltado = menuInicial.Where(m => m.TipoUser.Any(u => (int)u == tipoRol)).ToList();

            MenuGeneral = menuFiltado;
            Console.WriteLine($"menuFiltado {menuFiltado.Count()}");
        }
    }
}
