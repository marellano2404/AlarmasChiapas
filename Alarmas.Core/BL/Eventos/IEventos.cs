using Alarmas.Core.Models;
using Alarmas.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alarmas.Core.BL.Eventos
{
    public interface IEventos
    {
        Task<List<CodigosAlarma>> GetCodigoAlarmas();
        Task<bool> PutClaveAlarma(CodigosAlarma codigoAlarma);
        Task<bool> DelClaveAlarma(int id);
        Task<bool> PostClaveAlarma(CodigosAlarma codigoAlarma);

        Task<List<AlarmasEmitidasVM>> GetListaEventosCte(Guid idCliente);
        Task<bool> PostHistorialAlarmaCte(HistorialAlarma historialAlarma);
        Task<bool> PutHistorialAlarmaCte(HistorialAlarma historialAlarma);
        Task<bool> DelHistorialAlarmaCte(Guid IdHistorialA);
        Task<HistorialAlarma> GetHistoriaAlarma(Guid idhistoriaAlarma);
        Task<List<ViewModelReportAlarmas>> GetDatosReporte(Guid idCliente, string fechaInicial, string fechaFinal);
    }
}
