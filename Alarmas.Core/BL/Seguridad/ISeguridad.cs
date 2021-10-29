using Alarmas.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alarmas.Core.BL.Seguridad
{
    public interface ISeguridad
    {
        Task<bool> VerificaConexion();
        Task<List<TaskModel>> GetListaTipoAlarmas();
    }
}
