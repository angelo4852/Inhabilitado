using ConstanciaNoInhabilitado.Shared.Entities.Login;
using ConstanciaNoInhabilitado.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using ConstanciaNoInhabilitado.Shared.Entities.Catalogos;

namespace ConstanciaNoInhabilitado.Server.Interfaces
{
	public interface IServicioRepositorioCatalogos
	{
		Task<List<Dependencia>> ObtenerCatalogosDependencias();
		Task<List<CausaInhabilitacion>> ObtenerCatalogosCausas();
		Task<List<OrigenInhabilitacion>> ObtenerCatalogosOrigen();
		Task<List<Nivel>> ObtenerCatalogosNivel();
		Task<List<Moneda>> ObtenerCatalogosMoneda();
		Task<List<TipoFalta>> ObtenerCatalogosTipoFalta();
		Task<List<TipoInhabilitacion>> ObtenerCatalogosTipoInhabilitacion();
		Task<List<TipoSancion>> ObtenerCatalogosTipoSancion();
	}
}
