using Alarmas.Core.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alarmas.Core.BL.Clientes
{
    public class ClientesServices : IClientes
    {
        public async Task<List<Cliente>> GetListaClientes()
        {
            using (var conexion = new CAlarmasDBContext())
            {
                var consulta = await (from e in conexion.Clientes select e).ToListAsync();
                return consulta;
            }
        }
        public async Task<bool> PostNuevoCliente(Cliente cliente)
        {
            using (var Conexion = new SqlConnection(Helpers.ContextConfiguration.ConexionString))
            {
                bool resultado = false;
                try
                {

                    var comando = new SqlCommand();
                    comando.Connection = Conexion;
                    comando.CommandText = "Procesos.[AdministracionCliente]";
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    /*Agregando los parametros*/
                    comando.Parameters.AddWithValue("@Opcion", "Agregar");
                    comando.Parameters.AddWithValue("@Empresa", cliente.Empresa.ToUpper().Trim());
                    comando.Parameters.AddWithValue("@NumCliente", cliente.NumCliente);
                    comando.Parameters.AddWithValue("@Propietario", cliente.Propietario.ToUpper().Trim());
                    comando.Parameters.AddWithValue("@Rfc", cliente.Rfc.ToUpper().Trim());
                    comando.Parameters.AddWithValue("@Direccion", cliente.Direccion.ToUpper().Trim());
                    comando.Parameters.AddWithValue("@Referencias", cliente.Referencias.ToUpper().Trim());
                    comando.Parameters.AddWithValue("@TelParticular", cliente.TelParticular.Trim());
                    comando.Parameters.AddWithValue("@Celular", cliente.Celular.Trim());
                    comando.Parameters.AddWithValue("@Correo", cliente.Correo.Trim());
                    comando.Parameters.AddWithValue("@FechaAlta", cliente.FechaAlta);
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
        public async Task<bool> PutCliente(Cliente cliente)
        {
            using (var Conexion = new SqlConnection(Helpers.ContextConfiguration.ConexionString))
            {
                bool resultado = false;
                try
                {

                    var comando = new SqlCommand();
                    comando.Connection = Conexion;
                    comando.CommandText = "Procesos.[AdministracionCliente]";
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    /*Agregando los parametros*/
                    comando.Parameters.AddWithValue("@Opcion", "Actualizar");
                    comando.Parameters.AddWithValue("@IdCliente", cliente.Id);
                    comando.Parameters.AddWithValue("@NumCliente", cliente.NumCliente);
                    comando.Parameters.AddWithValue("@Empresa", cliente.Empresa.ToUpper().Trim());
                    comando.Parameters.AddWithValue("@Propietario", cliente.Propietario.ToUpper().Trim());
                    comando.Parameters.AddWithValue("@Rfc", cliente.Rfc.ToUpper().Trim());
                    comando.Parameters.AddWithValue("@Direccion", cliente.Direccion.ToUpper().Trim());
                    comando.Parameters.AddWithValue("@Referencias", cliente.Referencias.ToUpper().Trim());
                    comando.Parameters.AddWithValue("@TelParticular", cliente.TelParticular.Trim());
                    comando.Parameters.AddWithValue("@Celular", cliente.Celular.Trim());
                    comando.Parameters.AddWithValue("@Correo", cliente.Correo.Trim());
                    comando.Parameters.AddWithValue("@FechaAlta", cliente.FechaAlta);
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
        public async Task<List<Cliente>> BuscarCliente(string valorBusqueda)
        {
            using (var Conexion = new SqlConnection(Helpers.ContextConfiguration.ConexionString))
            {
                List<Cliente> Listaclientes = new List<Cliente>();
                try
                {
                    var comando = new SqlCommand();
                    comando.Connection = Conexion;
                    comando.CommandText = "Procesos.[AdministracionCliente]";
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    /*Agregando los parametros*/
                    comando.Parameters.AddWithValue("@Opcion", "BuscarCliente");
                    comando.Parameters.AddWithValue("@Valor", valorBusqueda.Trim());
                    Conexion.Open();
                    var Lectura = await comando.ExecuteReaderAsync();
                    if (Lectura.HasRows)
                    {
                        while (Lectura.Read())
                        {
                            Listaclientes.Add(
                                new Cliente
                                {
                                    Id = Lectura.GetGuid(0),
                                    NumCliente = Lectura.GetInt32(1),
                                    Empresa = Lectura.GetString(2),
                                    Propietario = Lectura.GetString(3),
                                    Rfc = Lectura.GetString(4),
                                    Direccion = Lectura.GetString(5),
                                    Referencias = Lectura.GetString(6),
                                    TelParticular = Lectura.GetString(7),
                                    Celular = Lectura.GetString(8),
                                    Correo = Lectura.GetString(9),
                                    FechaAlta = Lectura.GetDateTime(10)
                                });
                        }
                    }
                    Conexion.Close();
                    return Listaclientes;

                }
                catch (Exception e)
                {
                    var m = e.Message.ToString();
                    return Listaclientes;
                }
            }
        }
        public async Task<bool> DeleteCliente(Guid idcliente)
        {
            using (var Conexion = new SqlConnection(Helpers.ContextConfiguration.ConexionString))
            {
                bool resultado = false;
                try
                {

                    var comando = new SqlCommand();
                    comando.Connection = Conexion;
                    comando.CommandText = "Procesos.[AdministracionCliente]";
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    /*Agregando los parametros*/
                    comando.Parameters.AddWithValue("@Opcion", "Borrar");
                    comando.Parameters.AddWithValue("@IdCliente", idcliente);
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

        public async Task<bool> PostNuevoUsuario(ClienteUsuario usuario)
        {
            using (var Conexion = new SqlConnection(Helpers.ContextConfiguration.ConexionString))
            {
                bool resultado = false;
                try
                {

                    var comando = new SqlCommand();
                    comando.Connection = Conexion;
                    comando.CommandText = "Procesos.[AdministracionUsuariosCliente]";
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    /*Agregando los parametros*/
                    comando.Parameters.AddWithValue("@Opcion", "Agregar");
                    comando.Parameters.AddWithValue("@Contraseña", usuario.Contraseña.Trim());
                    comando.Parameters.AddWithValue("@IdCliente", usuario.IdCliente);
                    comando.Parameters.AddWithValue("@NombreCompleto", usuario.NombreCompleto.ToUpper().Trim());
                    comando.Parameters.AddWithValue("@Puesto", usuario.Puesto.ToUpper().Trim());
                    comando.Parameters.AddWithValue("@NumUsuario", usuario.NumUsuario);
                    comando.Parameters.AddWithValue("@Usuario", usuario.Usuario.ToUpper().Trim());
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
        public async Task<bool> PutUsuario(ClienteUsuario usuario)
        {
            using (var Conexion = new SqlConnection(Helpers.ContextConfiguration.ConexionString))
            {
                bool resultado = false;
                try
                {

                    var comando = new SqlCommand();
                    comando.Connection = Conexion;
                    comando.CommandText = "Procesos.[AdministracionUsuariosCliente]";
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    /*Agregando los parametros*/
                    comando.Parameters.AddWithValue("@Opcion", "Actualizar");
                    comando.Parameters.AddWithValue("@Contraseña", usuario.Contraseña.Trim());
                    comando.Parameters.AddWithValue("@IdCliente", usuario.IdCliente);
                    comando.Parameters.AddWithValue("@NombreCompleto", usuario.NombreCompleto.ToUpper().Trim());
                    comando.Parameters.AddWithValue("@Puesto", usuario.Puesto.ToUpper().Trim());
                    comando.Parameters.AddWithValue("@NumUsuario", usuario.NumUsuario); 
                    comando.Parameters.AddWithValue("@Usuario", usuario.Usuario.ToUpper().Trim());
                    comando.Parameters.AddWithValue("@IdUsuarioCliente", usuario.Id);
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
        
        public async Task<bool> DeleteUsuario(Guid id)
        {
            using (var Conexion = new SqlConnection(Helpers.ContextConfiguration.ConexionString))
            {
                bool resultado = false;
                try
                {

                    var comando = new SqlCommand();
                    comando.Connection = Conexion;
                    comando.CommandText = "Procesos.[AdministracionUsuariosCliente]";
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    /*Agregando los parametros*/
                    comando.Parameters.AddWithValue("@Opcion", "Borrar");
                    comando.Parameters.AddWithValue("@IdUsuarioCliente", id);
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
        public async Task<List<ClienteUsuario>> GetListaUsuarios(Guid idCliente)
        {
            using (var conexion = new CAlarmasDBContext())
            {
                var consulta = await (from e in conexion.ClienteUsuarios where e.IdCliente == idCliente select e).ToListAsync();
                return consulta;
            }
        }

        public async Task<bool> PostNuevaInstalacion(Instalacion instalacion)
        {
            using (var Conexion = new SqlConnection(Helpers.ContextConfiguration.ConexionString))
            {
                bool resultado = false;
                try
                {

                    var comando = new SqlCommand();
                    comando.Connection = Conexion;
                    comando.CommandText = "Procesos.[AdministracionInstalacion]";
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    /*Agregando los parametros*/
                    comando.Parameters.AddWithValue("@Opcion", "Agregar");
                    comando.Parameters.AddWithValue("@Zona", instalacion.Zona);
                    comando.Parameters.AddWithValue("@IdCliente", instalacion.IdCliente);
                    comando.Parameters.AddWithValue("@Dispositivo", instalacion.Dispositivo);
                    comando.Parameters.AddWithValue("@LugarInstalacion", instalacion.LugarInstalacion.Trim());
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
        public async Task<List<Instalacion>> GetListaInstalaciones(Guid idCliente)
        {
            using (var conexion = new CAlarmasDBContext())
            {
                var consulta = await(from e in conexion.Instalacions where e.IdCliente == idCliente select e).ToListAsync();
                return consulta;
            }
        }      
        public async Task<bool> DeleteInstalacion(Guid id)
        {
            using (var Conexion = new SqlConnection(Helpers.ContextConfiguration.ConexionString))
            {
                bool resultado = false;
                try
                {

                    var comando = new SqlCommand();
                    comando.Connection = Conexion;
                    comando.CommandText = "Procesos.[AdministracionInstalacion]";
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    /*Agregando los parametros*/
                    comando.Parameters.AddWithValue("@Opcion", "Borrar");
                    comando.Parameters.AddWithValue("@IdInstalacion", id);
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
    }
}
