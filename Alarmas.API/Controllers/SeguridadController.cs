using Alarmas.Core.BL.Seguridad;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alarmas.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SeguridadController : Controller
    {
        #region PROPIEDADES

        private readonly ISeguridad _SeguridadService;


        #endregion

        #region CONTRUCTOR
        public SeguridadController(ISeguridad SeguridadService)
        {
            _SeguridadService = SeguridadService;
        }
        #endregion
        #region Metodos 
        [HttpGet]
        public async Task<IActionResult> GetListaAlarmas()
        {
            try
            {
                var Result = await _SeguridadService.GetListaTipoAlarmas();
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
