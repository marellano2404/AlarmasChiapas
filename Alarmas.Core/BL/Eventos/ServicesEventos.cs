using Alarmas.Core.Models;
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
    }
}
