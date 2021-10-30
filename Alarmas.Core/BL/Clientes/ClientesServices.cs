﻿using Alarmas.Core.Models;
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
                    comando.Parameters.AddWithValue("@Empresa", cliente.Empresa.Trim());
                    comando.Parameters.AddWithValue("@Propietario", cliente.Propietario.Trim());
                    comando.Parameters.AddWithValue("@Rfc", cliente.Rfc.Trim());
                    comando.Parameters.AddWithValue("@Direccion", cliente.Direccion.Trim());
                    comando.Parameters.AddWithValue("@Referencias", cliente.Referencias.Trim());
                    comando.Parameters.AddWithValue("@TelParticular", cliente.TelParticular.Trim());
                    comando.Parameters.AddWithValue("@Celular", cliente.Celular.Trim());
                    comando.Parameters.AddWithValue("@Correo", cliente.Correo.Trim());
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