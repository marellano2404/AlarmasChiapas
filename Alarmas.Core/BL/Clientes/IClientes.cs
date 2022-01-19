using Alarmas.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alarmas.Core.BL.Clientes
{
    public interface IClientes
    {
        Task<List<Cliente>> GetListaClientes();
        Task<bool> PostNuevoCliente(Cliente cliente);
        Task<bool> PutCliente(Cliente cliente);
        Task<List<Cliente>> BuscarCliente(string valorBusqueda);
        Task<bool> DeleteCliente(Guid idcliente);

        Task<bool> PostNuevoUsuario(ClienteUsuario usuario);   
        Task<List<ClienteUsuario>> GetListaUsuarios(Guid idCliente);
        Task<bool> DeleteUsuario(Guid id);

        Task<List<Instalacion>> GetListaInstalaciones(Guid idCliente);
        Task<bool> PostNuevaInstalacion(Instalacion instalacion);
        Task<bool> DeleteInstalacion(Guid id);
    }
}