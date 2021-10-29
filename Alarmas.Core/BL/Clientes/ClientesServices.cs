using Alarmas.Core.Models;
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
    }
}
