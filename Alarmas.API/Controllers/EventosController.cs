using Alarmas.Core.BL.Eventos;
using Alarmas.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alarmas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        #region PROPIEDADES

        private readonly IEventos _EventosService;


        #endregion

        #region CONTRUCTOR
        public EventosController(IEventos ServicesEventos)
        {
            _EventosService = ServicesEventos;
        }
        #endregion

        #region M E T O D O S
        [HttpGet("GetListaCodigosAlarmas")]
        public async Task<IActionResult> GetListaCodigosAlarmas()
        {
            try
            {
                var Result = await _EventosService.GetCodigoAlarmas();
                if (Result != null)
                {
                    return Ok(Result);
                }
                else
                {
                    return Unauthorized();
                }

            }
            catch (Exception)
            {
                return BadRequest("La Conexión no ha sido encontrado!");
            }
        }
        [HttpPost("PostClaveAlarma")]
        public async Task<IActionResult> PostClaveAlarma([FromBody] CodigosAlarma CodigoAlarma)
        {
            try
            {
                var Result = await _EventosService.PostClaveAlarma(CodigoAlarma);
                return Ok(Result);


            }
            catch (Exception)
            {
                return BadRequest("La Conexión no ha sido encontrado!");
            }
        }
        [HttpDelete("DelClaveAlarma/{Id}")]
        public async Task<IActionResult> DelClaveAlarma(int Id)
        {
            try
            {
                var Result = await _EventosService.DelClaveAlarma(Id);
                if (Result == true)
                {
                    return Ok(Result);
                }
                else
                {
                    return Conflict(Result);
                }
            }
            catch (Exception)
            {
                return BadRequest("La Conexión no ha sido encontrado!");
            }
        }
        [HttpPut("PutClaveAlarma")]
        public async Task<IActionResult> PutClaveAlarma([FromBody] CodigosAlarma CodigoAlarma)
        {
            try
            {
                var Result = await _EventosService.PutClaveAlarma(CodigoAlarma);
                return Ok(Result);


            }
            catch (Exception)
            {
                return BadRequest("La Conexión no ha sido encontrado!");
            }
        }



        [HttpGet("GetListaEventosCte/{IdCliente}")]
        public async Task<IActionResult> GetListaEventosCte(Guid IdCliente)
        {
            try
            {
                var Result = await _EventosService.GetListaEventosCte(IdCliente);
                if (Result != null)
                {
                    return Ok(Result);
                }
                else
                {
                    return Unauthorized();
                }

            }
            catch (Exception)
            {
                return BadRequest("La Conexión no ha sido encontrado!");
            }
        }
        [HttpPost("PostHistorialAlarmaCte")]
        public async Task<IActionResult> PostHistorialAlarmaCte([FromBody] HistorialAlarma historialAlarma)
        {
            try
            {
                var Result = await _EventosService.PostHistorialAlarmaCte(historialAlarma);
                return Ok(Result);
                

            }
            catch (Exception)
            {
                return BadRequest("La Conexión no ha sido encontrado!");
            }
        }
        [HttpPut("PutHistorialAlarmaCte")]
        public async Task<IActionResult> PutHistorialAlarmaCte([FromBody] HistorialAlarma historialAlarma)
        {
            try
            {
                var Result = await _EventosService.PutHistorialAlarmaCte(historialAlarma);
                return Ok(Result);


            }
            catch (Exception)
            {
                return BadRequest("La Conexión no ha sido encontrado!");
            }
        }
        
        [HttpDelete("DelHistorialAlarmaCte/{Id}")]
        public async Task<IActionResult> DelHistorialAlarmaCte(Guid Id)
        {
            try
            {
                var Result = await _EventosService.DelHistorialAlarmaCte(Id);
                return Ok(Result);
            }
            catch (Exception)
            {
                return BadRequest("La Conexión no ha sido encontrado!");
            }
        }

        [HttpGet("GetEventoAlarma/{IdhistoriaAlarma}")]
        public async Task<IActionResult> GetEventoAlarma(Guid IdhistoriaAlarma)
        {
            try
            {
                var Result = await _EventosService.GetHistoriaAlarma(IdhistoriaAlarma);
                if (Result != null)
                {
                    return Ok(Result);
                }
                else
                {
                    return Unauthorized();
                }

            }
            catch (Exception)
            {
                return BadRequest("La Conexión no ha sido encontrado!");
            }
        }
        #endregion
    }
}
