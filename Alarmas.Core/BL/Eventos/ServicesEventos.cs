using Alarmas.Core.Models;
using Alarmas.Core.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alarmas.Core.BL.Eventos
{
    public class ServicesEventos : IEventos
    {
        public async Task<List<CodigosAlarma>> GetCodigoAlarmas()
        {
            using (var conexion = new CAlarmasDBContext())
            {
                var consulta = await(from e in conexion.CodigosAlarmas select e).ToListAsync();
                return consulta;
            }
        }
        public async Task<bool> PostClaveAlarma(CodigosAlarma codigoAlarma)
        {
            using (var Conexion = new SqlConnection(Helpers.ContextConfiguration.ConexionString))
            {
                bool resultado = false;
                try
                {

                    var comando = new SqlCommand();
                    comando.Connection = Conexion;
                    comando.CommandText = "Catalogos.[AdmonCodigosdeAlarmas]";
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    /*Agregando los parametros*/
                    comando.Parameters.AddWithValue("@Opcion", "Agregar");
                    comando.Parameters.AddWithValue("@Clave", codigoAlarma.Clave.ToUpper().Trim());
                    comando.Parameters.AddWithValue("@Descripcion", codigoAlarma.Descripcion.ToUpper().Trim());
                    Conexion.Open();
                    var Lectura = await comando.ExecuteReaderAsync();
                    if (Lectura.HasRows)
                    {
                        while (Lectura.Read())
                        {
                            resultado = Lectura.GetBoolean(0);
                        }
                    }
                    Conexion.Close();
                    return resultado;

                }
                catch (Exception e)
                {
                    var m = e.Message.ToString();
                    return resultado;
                }
            }
        }
        public async Task<bool> PutClaveAlarma(CodigosAlarma codigoAlarma)
        {
            using (var Conexion = new SqlConnection(Helpers.ContextConfiguration.ConexionString))
            {
                bool resultado = false;
                try
                {

                    var comando = new SqlCommand();
                    comando.Connection = Conexion;
                    comando.CommandText = "Catalogos.[AdmonCodigosdeAlarmas]";
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    /*Agregando los parametros*/
                    comando.Parameters.AddWithValue("@Opcion", "Actualizar");
                    comando.Parameters.AddWithValue("@Clave", codigoAlarma.Clave.ToUpper().Trim());
                    comando.Parameters.AddWithValue("@Descripcion", codigoAlarma.Descripcion.ToUpper().Trim());
                    comando.Parameters.AddWithValue("@IdCodigo", codigoAlarma.Id);
                    Conexion.Open();
                    var Lectura = await comando.ExecuteReaderAsync();
                    if (Lectura.HasRows)
                    {
                        while (Lectura.Read())
                        {
                            resultado = Lectura.GetBoolean(0);
                        }
                    }
                    Conexion.Close();
                    return resultado;

                }
                catch (Exception e)
                {
                    var m = e.Message.ToString();
                    return resultado;
                }
            }
        }
        public async Task<bool> DelClaveAlarma(int id)
        {
            using (var Conexion = new SqlConnection(Helpers.ContextConfiguration.ConexionString))
            {
                bool resultado = false;
                try
                {

                    var comando = new SqlCommand();
                    comando.Connection = Conexion;
                    comando.CommandText = "Catalogos.[AdmonCodigosdeAlarmas]";
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    /*Agregando los parametros*/
                    comando.Parameters.AddWithValue("@Opcion", "Borrar");
                    comando.Parameters.AddWithValue("@IdCodigo", id);
                    Conexion.Open();
                    var Lectura = await comando.ExecuteReaderAsync();
                    if (Lectura.HasRows)
                    {
                        while (Lectura.Read())
                        {
                            resultado = Lectura.GetBoolean(0);
                        }
                    }
                    Conexion.Close();
                    return resultado;

                }
                catch (Exception e)
                {
                    var m = e.Message.ToString();
                    return resultado;
                }
            }
        }
        public async Task<List<AlarmasEmitidasVM>> GetListaEventosCte(Guid idCliente)
        {
            using (var Conexion = new SqlConnection(Helpers.ContextConfiguration.ConexionString))
            {
                List<AlarmasEmitidasVM> ListaEventos = new List<AlarmasEmitidasVM>();
                try
                {
                    var comando = new SqlCommand();
                    comando.Connection = Conexion;
                    comando.CommandText = "Procesos.[AdministracionhistorialAlarmas]";
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    /*Agregando los parametros*/
                    comando.Parameters.AddWithValue("@Opcion", "ListarAlarmas");
                    comando.Parameters.AddWithValue("@IdCliente", idCliente);
                    Conexion.Open();
                    var Lectura = await comando.ExecuteReaderAsync();
                    if (Lectura.HasRows)
                    {
                        while (Lectura.Read())
                        {
                            ListaEventos.Add(
                                new AlarmasEmitidasVM
                                {
                                    Id = Lectura.GetGuid(0),
                                    NumCliente = Lectura.GetInt32(1),
                                    Empresa = Lectura.GetString(2),
                                    Alarma = Lectura.GetString(3),
                                    ClaveAlarma = Lectura.GetString(4),
                                    Usuario = Lectura.GetString(5),
                                    DetalleAlarma = Lectura.GetString(6),
                                    Fecha = Lectura.GetDateTime(7),
                                    Hora = Lectura.GetString(8)
                                });
                        }
                    }
                    Conexion.Close();
                    return ListaEventos;

                }
                catch (Exception e)
                {
                    var m = e.Message.ToString();
                    return ListaEventos;
                }
            }
        }
        public async Task<bool> PostHistorialAlarmaCte(HistorialAlarma historialAlarma)
        {
            using (var Conexion = new SqlConnection(Helpers.ContextConfiguration.ConexionString))
            {
                bool resultado = false;
                try
                {

                    var comando = new SqlCommand();
                    comando.Connection = Conexion;
                    comando.CommandText = "Procesos.[AdministracionhistorialAlarmas]";
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    /*Agregando los parametros*/
                    comando.Parameters.AddWithValue("@Opcion", "Agregar");
                    comando.Parameters.AddWithValue("@Descripcion", historialAlarma.Descripcion.ToUpper().Trim());
                    comando.Parameters.AddWithValue("@Fecha", historialAlarma.Fecha);
                    comando.Parameters.AddWithValue("@Hora", historialAlarma.Hora.Trim());
                    comando.Parameters.AddWithValue("@IdClaveAlarma", historialAlarma.IdClaveAlarma);
                    comando.Parameters.AddWithValue("@IdCliente", historialAlarma.IdCliente);
                    comando.Parameters.AddWithValue("@IdUsuario", historialAlarma.IdUsuario);
                    comando.Parameters.AddWithValue("@IdZona", historialAlarma.IdZonaInstalacion);
                    Conexion.Open();
                    var Lectura = await comando.ExecuteReaderAsync();
                    if (Lectura.HasRows)
                    {
                        while (Lectura.Read())
                        {
                            resultado= Lectura.GetBoolean(0);
                        }
                    }
                    Conexion.Close();
                    return resultado;

                }
                catch (Exception e)
                {
                    var m = e.Message.ToString();
                    return resultado;
                }
            }
        }
        public async Task<bool> PutHistorialAlarmaCte(HistorialAlarma historialAlarma)
        {
            using (var Conexion = new SqlConnection(Helpers.ContextConfiguration.ConexionString))
            {
                bool resultado = false;
                try
                {

                    var comando = new SqlCommand();
                    comando.Connection = Conexion;
                    comando.CommandText = "Procesos.[AdministracionhistorialAlarmas]";
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    /*Agregando los parametros*/
                    comando.Parameters.AddWithValue("@Opcion", "Actualizar");
                    comando.Parameters.AddWithValue("@Descripcion", historialAlarma.Descripcion.ToUpper().Trim());
                    comando.Parameters.AddWithValue("@Fecha", historialAlarma.Fecha);
                    comando.Parameters.AddWithValue("@Hora", historialAlarma.Hora.Trim());
                    comando.Parameters.AddWithValue("@IdClaveAlarma", historialAlarma.IdClaveAlarma);
                    comando.Parameters.AddWithValue("@IdUsuario", historialAlarma.IdUsuario);
                    comando.Parameters.AddWithValue("@Id", historialAlarma.Id);
                    comando.Parameters.AddWithValue("@IdCliente", historialAlarma.IdCliente);
                    comando.Parameters.AddWithValue("@IdZona", historialAlarma.IdZonaInstalacion);
                    Conexion.Open();
                    var Lectura = await comando.ExecuteReaderAsync();
                    if (Lectura.HasRows)
                    {
                        while (Lectura.Read())
                        {
                            resultado = Lectura.GetBoolean(0);
                        }
                    }
                    Conexion.Close();
                    return resultado;

                }
                catch (Exception e)
                {
                    var m = e.Message.ToString();
                    return resultado;
                }
            }
        }
        public async Task<bool> DelHistorialAlarmaCte(Guid IdHistorialA)
        {
            using (var Conexion = new SqlConnection(Helpers.ContextConfiguration.ConexionString))
            {
                bool resultado = false;
                try
                {

                    var comando = new SqlCommand();
                    comando.Connection = Conexion;
                    comando.CommandText = "Procesos.[AdministracionhistorialAlarmas]";
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    /*Agregando los parametros*/
                    comando.Parameters.AddWithValue("@Opcion", "Borrar");
                    comando.Parameters.AddWithValue("@IdHistorialAlarma", IdHistorialA);
                    Conexion.Open();
                    var Lectura = await comando.ExecuteReaderAsync();
                    if (Lectura.HasRows)
                    {
                        while (Lectura.Read())
                        {
                            resultado = Lectura.GetBoolean(0);
                        }
                    }
                    Conexion.Close();
                    return resultado;

                }
                catch (Exception e)
                {
                    var m = e.Message.ToString();
                    return resultado;
                }
            }
        }
        public async Task<HistorialAlarma> GetHistoriaAlarma(Guid idhistoriaAlarma)
        {
            using (var Conexion = new SqlConnection(Helpers.ContextConfiguration.ConexionString))
            {
                HistorialAlarma resultado = new HistorialAlarma();
                try
                {

                    var comando = new SqlCommand();
                    comando.Connection = Conexion;
                    comando.CommandText = "Procesos.[AdministracionhistorialAlarmas]";
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    /*Agregando los parametros*/
                    comando.Parameters.AddWithValue("@Opcion", "ObtenerAlarma");
                    comando.Parameters.AddWithValue("@IdHistorialAlarma", idhistoriaAlarma);
                    Conexion.Open();
                    var Lectura = await comando.ExecuteReaderAsync();
                    if (Lectura.HasRows)
                    {
                        while (Lectura.Read())
                        {
                            resultado.IdClaveAlarma = Lectura.GetInt32(0);
                            resultado.IdCliente = Lectura.GetGuid(1);
                            resultado.IdUsuario = Lectura.GetGuid(2);
                            resultado.Fecha = Lectura.GetDateTime(3);
                            resultado.Descripcion = Lectura.GetString(4);
                            resultado.Hora = Lectura.GetString(5);
                            resultado.Id = Lectura.GetGuid(6);
                            resultado.IdZonaInstalacion = Lectura.GetGuid(7);
                        }
                    }
                    Conexion.Close();
                    return resultado;

                }
                catch (Exception e)
                {
                    var m = e.Message.ToString();
                    return resultado;
                }
            }
        }

        public async Task<List<ViewModelReportAlarmas>> GetDatosReporte(Guid idCliente, string fechaInicial, string fechaFinal)
        {
            using (var Conexion = new SqlConnection(Helpers.ContextConfiguration.ConexionString))
            {
                List<ViewModelReportAlarmas> ListaEventos = new List<ViewModelReportAlarmas>();
                try
                {
                    var comando = new SqlCommand();
                    comando.Connection = Conexion;
                    comando.CommandText = "Procesos.[AdministracionhistorialAlarmas]";
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    /*Agregando los parametros*/
                    comando.Parameters.AddWithValue("@Opcion", "ListarAlarmasxFecha");
                    comando.Parameters.AddWithValue("@IdCliente", idCliente);
                    comando.Parameters.AddWithValue("@FechaInicial", fechaInicial);
                    comando.Parameters.AddWithValue("@FechaFinal", fechaFinal);
                    Conexion.Open();
                    var Lectura = await comando.ExecuteReaderAsync();
                    if (Lectura.HasRows)
                    {
                        while (Lectura.Read())
                        {
                            ListaEventos.Add(
                                new ViewModelReportAlarmas
                                {
                                    ClaveAlarma = Lectura.GetString(0),
                                    Alarma = Lectura.GetString(1),
                                    NumUsuario = Lectura.GetInt32(2),
                                    Usuario = Lectura.GetString(3),
                                    DetalleAlarma = Lectura.GetString(4),
                                    Zona = Lectura.GetInt32(5),
                                    LugarInstalacion = Lectura.GetString(6),
                                    Dispositivo = Lectura.GetString(7),
                                    Fecha = Lectura.GetDateTime(8),
                                    Hora = Lectura.GetString(9)
                                });
                        }
                    }
                    Conexion.Close();
                    return ListaEventos;

                }
                catch (Exception e)
                {
                    var m = e.Message.ToString();
                    return ListaEventos;
                }
            }
        }
    }
}
