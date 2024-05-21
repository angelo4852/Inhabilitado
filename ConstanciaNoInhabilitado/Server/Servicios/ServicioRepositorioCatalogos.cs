using ConstanciaNoInhabilitado.Server.Interfaces;
using ConstanciaNoInhabilitado.Shared.Entities;
using ConstanciaNoInhabilitado.Shared.Entities.Catalogos;
using ConstanciaNoInhabilitado.Shared.Entities.Login;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ConstanciaNoInhabilitado.Server.Servicios
{
    public class ServicioRepositorioCatalogos : IServicioRepositorioCatalogos
    {
        private readonly string connectionString;
        public ServicioRepositorioCatalogos(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<CausaInhabilitacion>> ObtenerCatalogosCausas()
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                var data = await connection.QueryAsync(@"SELECT * FROM CausaInhabilitacion");
                var jsonResult = JsonConvert.SerializeObject(data);
                var causa = JsonConvert.DeserializeObject<List<CausaInhabilitacion>>(jsonResult);
                CausaInhabilitacion CausaInhabilitacion = new CausaInhabilitacion
                {
                    IdCausaInhabilitacion = 0,
                    Descripcion = "--Seleccione--"
                };
                List<CausaInhabilitacion> causas = new();
                causas.Add(CausaInhabilitacion);
                causas.AddRange(causa);
                return causas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<List<Dependencia>> ObtenerCatalogosDependencias()
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                var data = await connection.QueryAsync(@"SELECT * FROM DEPENDENCIA");
                var jsonResult = JsonConvert.SerializeObject(data);
                var dependencia = JsonConvert.DeserializeObject<List<Dependencia>>(jsonResult);
                Dependencia dependenciaDefault = new Dependencia
                {
                    IdDependencia = 0,
                    Descripcion = "--Seleccione--"
                };
                List<Dependencia> dependencias = new();
                dependencias.Add(dependenciaDefault);
                dependencias.AddRange(dependencia!);
                return dependencias;
            }
            catch (Exception ex)
            {
                throw ex;
            }   
        }

        public async Task<List<Moneda>> ObtenerCatalogosMoneda()
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                var data = await connection.QueryAsync(@"SELECT * FROM Moneda");
                var jsonResult = JsonConvert.SerializeObject(data);
                var moneda = JsonConvert.DeserializeObject<List<Moneda>>(jsonResult);
                Moneda MonedaDefault = new Moneda
                {
                    idMoneda = 0,
                    moneda_valor = "--Seleccione--"
                };
                List<Moneda> monedas = new();
                monedas.Add(MonedaDefault);
                monedas.AddRange(moneda);
                return monedas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Nivel>> ObtenerCatalogosNivel()
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                var data = await connection.QueryAsync(@"SELECT * FROM Nivel");
                var jsonResult = JsonConvert.SerializeObject(data);
                var nivel = JsonConvert.DeserializeObject<List<Nivel>>(jsonResult);
                List<Nivel> niveles = new();
                Nivel NivelDefault = new Nivel
                {
                    idNivel = 0,
                    Descripcion = "--Seleccione--"
                };
                niveles.Add(NivelDefault);
                niveles.AddRange(nivel);
                return niveles;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<OrigenInhabilitacion>> ObtenerCatalogosOrigen()
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                var data = await connection.QueryAsync(@"SELECT * FROM OrigenInhabilitacion");
                var jsonResult = JsonConvert.SerializeObject(data);
                var origen = JsonConvert.DeserializeObject<List<OrigenInhabilitacion>>(jsonResult);
                List<OrigenInhabilitacion> origenes = new();
                OrigenInhabilitacion OrigenInhabilitacionDefault = new OrigenInhabilitacion
                {
                    IdOrigenInhabilitacion = 0,
                    Descripcion = "--Seleccione--"
                };
                origenes.Add(OrigenInhabilitacionDefault);
                origenes.AddRange(origen);
                return origenes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
         
        }

        public async Task<List<TipoFalta>> ObtenerCatalogosTipoFalta()
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                var data = await connection.QueryAsync(@"SELECT * FROM TipoFalta");
                var jsonResult = JsonConvert.SerializeObject(data);
                var tipoFalta = JsonConvert.DeserializeObject<List<TipoFalta>>(jsonResult);
                List<TipoFalta> tiposFaltas = new();
                TipoFalta TipoFaltaDefault = new TipoFalta
                {
                    idTipoFalta = 0,
                    tipoFalta_valor = "--Seleccione--"
                };
                tiposFaltas.Add(TipoFaltaDefault);
                tiposFaltas.AddRange(tipoFalta);
                return tiposFaltas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<TipoInhabilitacion>> ObtenerCatalogosTipoInhabilitacion()
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                var data = await connection.QueryAsync(@"SELECT * FROM TipoInhabilitacion");
                var jsonResult = JsonConvert.SerializeObject(data);
                var TipoInhabilitacion = JsonConvert.DeserializeObject<List<TipoInhabilitacion>>(jsonResult);
                TipoInhabilitacion TipoInhabilitacionDefault = new TipoInhabilitacion
                {
                    IdTipoInhabilitacion = 0,
                    Descripcion = "--Seleccione--"
                };
                List<TipoInhabilitacion> TipoInhabilitaciones = new();
                TipoInhabilitaciones.Add(TipoInhabilitacionDefault);
                TipoInhabilitaciones.AddRange(TipoInhabilitacion!);
                return TipoInhabilitaciones;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<TipoSancion>> ObtenerCatalogosTipoSancion()
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                var data = await connection.QueryAsync(@"SELECT * FROM TipoSancion");
                var jsonResult = JsonConvert.SerializeObject(data);
                var TipoSancion = JsonConvert.DeserializeObject<List<TipoSancion>>(jsonResult);
                List<TipoSancion> TipoSanciones = new();
                TipoSancion TipoSancionDefault = new TipoSancion
                {
                    idTipoSancion = 0,
                    tipoSancion_valor = "--Seleccione--"
                };
                TipoSanciones.Add(TipoSancionDefault);
                TipoSanciones.AddRange(TipoSancion);
                return TipoSanciones;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
