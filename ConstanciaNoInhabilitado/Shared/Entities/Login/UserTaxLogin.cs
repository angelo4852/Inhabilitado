﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ConstanciaNoInhabilitado.Shared.Entities.Login
{
    public class UserTaxLogin
    {
        /// <summary>
        /// Nombre de Usuario o RFC
        /// </summary>
        [Required(ErrorMessage = "El usuario es requerido"), DisplayName("Usuario")]
        public string Usuario { get; set; }

        /// <summary>
        /// Numero de control, parecido a la contraseña
        /// </summary>
        [Required(ErrorMessage = "La contraseña es requerida"), DisplayName("Contraseña")]
        public string Contrasena { get; set; }
      
    }

    public class SearchReference
    {
        /// <summary>
        /// Referencia que se va a buscar
        /// </summary>
        [Required(ErrorMessage = "La referencia es requerida"), DisplayName("Referencia")]
        public string Reference { get; set; }

        /// <summary>
        /// Captcha para seguridad
        /// </summary>
        [Required(ErrorMessage = "El captcha es requerida"), DisplayName("Captcha")]
        public string Captcha { get; set; }
    }

    public class SearchReferenceReponse
    {
        /// <summary>
        /// Link de la referencia generada
        /// </summary>
        public string ReferenceSearch { get; set; }        
    }
    public class Usuarios
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }
        public string CorreoElectronico { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int IdUsuarioModifica { get; set; }
        public int IdRolUsuario { get; set; }


    }
    public class RolUsuario
    {
        /// <summary>
        /// Link de la referencia generada
        /// </summary>
        public int IdRolUsuario { get; set; }
        public string Descripcion { get; set; }

    }


    public class Session 
    {

        /// <summary>
        /// Id de usuario BD
        /// </summary>       
        public int IdUser { get; set; }

        /// <summary>
        /// Nombre de Usuario o RFC
        /// </summary>       
        public string User { get; set; }

        /// <summary>
        /// Correo del usuario
        /// </summary>      
        public string Correo { get; set; }

        /// <summary>
        /// Correo del usuario
        /// </summary>      
        public string Rol { get; set; }

        /// <summary>
        /// Tiempo de expiracion de la sesion
        /// </summary>      
        public string ExpiryTime { get; set; }
    }

    public class MenuOpciones 
    {
        /// <summary>
        /// Ruta de la imagen que se colocar en la card
        /// </summary>   
        public string Image { get; set; }
        /// <summary>
        /// Titulo de la card
        /// </summary> 
        public string Title { get; set; }
        /// <summary>
        /// Descripcion de la card
        /// </summary>   
        public string Description { get; set; }
        /// <summary>
        /// Url que se a dirigir al dar click
        /// </summary>   
        public string Url { get; set; }

        /// <summary>
        /// Tipo Menu Admin, Catalogos, Reporte
        /// </summary>   
        public TipoMenu TipoMenu { get; set; }

        /// <summary>
        /// Tipo Usuario  Administrador = 1,Supervisor ,Analista , Ninguno
        /// </summary>  
        public List<TipoUsuario> TipoUser { get; set; }
    }

    public class CategoriaRegistrarInhabilitado 
    {
        /// <summary>
        /// Id de la categoria
        /// </summary>   
        public string Value { get; set; }
        /// <summary>
        /// Nombre de la categoria
        /// </summary>   
        public string Text { get; set; }       
    }

    public class ServidorPublico 
    {
        /// <summary>
        /// Nombre de Servido Publico
        /// </summary>   
        public int IdInhabilitado { get; set; } 
        /// <summary>
        /// Nombre de Servido Publico
        /// </summary>   
        public string Nombre { get; set; }

        /// <summary>
        /// Apellido paterno de Servido Publico
        /// </summary>   
        public string ApellidoPaterno { get; set; }

        /// <summary>
        ///  Apellido materno de Servido Publico
        /// </summary>   
        public string ApellidoMaterno { get; set; }

        /// <summary>
        ///  rfc de Servido Publico
        /// </summary>   
        public string RFC { get; set; }

        /// <summary>
        ///  Curp de Servido Publico
        /// </summary>   
        public string CURP { get; set; }


        public int Tipo { get; set; }
        /// <summary>
        ///  Sexo de Servido Publico
        /// </summary>   
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        ///  Sexo de Servido Publico
        /// </summary>   
        public DateTime FechaUltimaModificacion { get; set; }
        /// <summary>
        ///  Sexo de Servido Publico
        /// </summary>   
        public int? IdUsuario { get; set; }
        /// <summary>
        ///  Sexo de Servido Publico
        /// </summary>   
        public int? idGenero { get; set; } = 0;

        public bool banderaExiste { get; set; } = false;
        public string? TypePersonProovedor { get; set; } = string.Empty;

        public string genero_valor { get; set; }

        public int? idBandera { get; set; } 


    }

    public class ProvedorContratista
    {
        /// <summary>
        /// Nombre de ProovedorContratista
        /// </summary>   
        public string TypePersonProovedor { get; set; } = string.Empty;
        /// <summary>
        /// Nombre de ProovedorContratista
        /// </summary>   
        public string Name { get; set; }

        /// <summary>
        /// Apellido paterno de ProovedorContratista
        /// </summary>   
        public string LastName { get; set; }

        /// <summary>
        ///  Apellido materno de ProovedorContratista
        /// </summary>   
        public string SecondLastName { get; set; }

        /// <summary>
        ///  rfc de ProovedorContratista
        /// </summary>   
        public string RFC { get; set; }

        /// <summary>
        ///  Curp de ProovedorContratista
        /// </summary>   
        public string Curp { get; set; }

        /// <summary>
        ///  Sexo de ProovedorContratista
        /// </summary>   
        public string Sexo { get; set; }
    }

    public class BuscarServidorPublico
    {
        /// <summary>
        /// Criterio de búsqueda
        /// </summary>   
        public string TipoCriterioBúsqueda { get; set; } = string.Empty;
        /// <summary>
        /// Criterio
        /// </summary>   
        public string Criterio { get; set; } = string.Empty;
    }

	public class RegistrarNuevaInhabilitacion
	{

		/// <summary>
		/// idUsuario
		/// </summary>   
		public int idInhabilitacion { get; set; } = 0;

		/// <summary>
		/// idUsuario
		/// </summary>   
		public int idUsuario { get; set; }

		/// <summary>
		/// Estatus
		/// </summary>   
		public int Estatus { get; set; } = 1;


		/// <summary>
		/// inhabilitación en proceso de Servidor Publico
		/// </summary>   
		public int InhabilitacionProceso { get; set; }

        /// <summary>
        /// RFC de Servidor Publico
        /// </summary>   
        public string RFC { get; set; }

		/// <summary>
		/// Autoridad Sancionadora de Servidor Publico
		/// </summary>   
		public string AutoridadSancionadora { get; set; }

		/// <summary>
		/// Dependencia de Servidor Publico
		/// </summary>   
		public int Dependencia { get; set; }

		/// <summary>
		/// Puesto de Servidor Publico
		/// </summary>   
		public string Puesto { get; set; }

		/// <summary>
		/// Fecha de Inicio de Servidor Publico
		/// </summary>   
		public DateTime? FechaInicio { get; set; }

		/// <summary>
		/// Fecha de Término de Servidor Publico
		/// </summary>   
		public DateTime? FechaTermino { get; set; }

		/// <summary>
		/// Periodo de Servidor Publico
		/// </summary>   
		public string Periodo { get; set; }

		/// <summary>
		/// Fecha de Resolución de Servidor Publico
		/// </summary>   
		public DateTime? FechaResolución { get; set; }

		/// <summary>
		/// Expediente Legal de Servidor Publico
		/// </summary>   
		public string ExpedienteLegal { get; set; }

		/// <summary>
		/// Tipo de Inhabilitación de Servidor Publicoq
		/// </summary>   
		public int TipoInhabilitación { get; set; }

		/// <summary>
		/// Origen de Inhabilitación de Servidor Publico
		/// </summary>   
		public int OrigenInhabilitación { get; set; }

		/// <summary>
		/// Causa de la Inhabilitación de Servidor Publico
		/// </summary>   
		public int CausaInhabilitación { get; set; }

		/// <summary>
		/// Falta Cometida de Servidor Publico
		/// </summary>   
		public int FaltaCometida { get; set; }

		/// <summary>
		/// Tipo de la Sanción de Servidor Publico
		/// </summary>   
		public int TipoSanción { get; set; }

		/// <summary>
		/// Tipo Moneda de Servidor Publico
		/// </summary>   
		public int TipoMoneda { get; set; }

		/// <summary>
		/// Monto de Servidor Publico
		/// </summary>   
		public decimal Monto { get; set; }

		/// <summary>
		/// Clave del puesto del nivel del Servidor Público de Servidor Publico
		/// </summary>   
		public int ClaveNivelServidorPúblico { get; set; }

		/// <summary>
		/// Descripción de la Inhabilitación de Servidor Publico
		/// </summary>   
		public string DescripciónInhabilitación { get; set; }
	}
	public class TypePerson
    {
        /// <summary>
        /// Id TypePerson
        /// </summary>   
        public string Value { get; set; }
        /// <summary>
        /// TypePerson
        /// </summary>   
        public string Text { get; set; }
    }


    public class Sexo 
    {
        /// <summary>
        /// Id sexo
        /// </summary>   
        public int? idGenero { get; set; }
        /// <summary>
        /// Sexo
        /// </summary>   
        public string genero_valor { get; set; }
    }

    public class CriterioDeBusqueda
    {
        /// <summary>
        /// Id sexo
        /// </summary>   
        public string Value { get; set; }
        /// <summary>
        /// Sexo
        /// </summary>   
        public string Text { get; set; }
    }

    public class CriterioDeReportes 
    {
        /// <summary>
        /// Fecha de Inicio de Reportes
        /// </summary>   
        public DateTime? FechaInicio { get; set; }

        /// <summary>
        /// Fecha de Término de Reportes
        /// </summary>   
        public DateTime? FechaTermino { get; set; }

        /// <summary>
        /// Tipo de Reporte
        /// </summary>   
        public string TipoDeReporte { get; set; }
    }

    public class BuscarConstancia
    {

        /// <summary>
        /// RFC para buscar la constancia
        /// </summary>   
        public string RFC { get; set; }
    }

    public class RegistraDependencia
    {
        /// <summary>
        /// Descripcion de ka dependencia
        /// </summary>   
        public string Descripcion { get; set; }
    }

    public class RegistrarCausaInhabilitacion
    {
        /// <summary>
        /// Descripcion de la causa
        /// </summary>   
        public string Descripcion { get; set; }
    }

    public class RegistrarOrigenInhabilitacion
    {
        /// <summary>
        /// Descripcion de la causa
        /// </summary>   
        public string Descripcion { get; set; }
    }

    public class CausasInhabilitacion
    {
        public int IdCausaInhabilitacion { get; set; } 

        /// <summary>
        /// Id de la causa
        /// </summary>   
        public string? Value { get; set; } = string.Empty;

        /// <summary>
        /// Descripcion de la causa
        /// </summary>   
        public string? Descripcion { get; set; } 

        /// <summary>
        /// Muestra la opcion de actualizar y cancelar
        /// </summary>   
        public bool Acciones { get; set; } = false;

        public int? idBandera { get; set; } = 3;

        
    }

    public class OrigenesInhabilitacion
    {
        /// <summary>
        /// Id de la Origenes
        /// </summary>   
        public string Value { get; set; }

        /// <summary>
        /// Descripcion de la Origenes
        /// </summary>   
        public string Descripcion { get; set; }

        /// <summary>
        /// Muestra la opcion de actualizar y cancelar
        /// </summary>   
        public bool Acciones { get; set; }
    }

    public class DependenciasInhabilitacion
    {
        /// <summary>
        /// Id de la Dependencias
        /// </summary>   
        public string Value { get; set; }

        /// <summary>
        /// Descripcion de la Dependencias
        /// </summary>   
        public string Descripcion { get; set; }

        /// <summary>
        /// Muestra la opcion de actualizar y cancelar
        /// </summary>   
        public bool Acciones { get; set; }
    }
    public enum TipoMenu 
    {
        Admin = 1,
        Catalogo,
        Reporte
    }

    public enum TipoUsuario
    {
        Administrador = 1,
        Supervisor,
        Analista,
        Ninguno
    }
}
