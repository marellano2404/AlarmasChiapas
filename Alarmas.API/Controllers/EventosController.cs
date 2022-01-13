using Alarmas.Core.BL.Eventos;
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

        #endregion
    }
}
