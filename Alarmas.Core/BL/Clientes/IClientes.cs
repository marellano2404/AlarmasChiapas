using Alarmas.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alarmas.Core.BL.Clientes
{
    public interface IClientes
    {
        Task<List<Cliente>> GetListaClientes();
        Task<bool> PostNuevoCliente(Cliente cliente);
        Task<bool> PostNuevoUsuario(ClienteUsuario usuario);
        Task<bool> PostNuevaInstalacion(Instalacion instalacion);
    }
}