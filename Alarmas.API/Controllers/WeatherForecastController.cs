﻿using Alarmas.Core.BL.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alarmas.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        #region PROPIEDADES

        private readonly ISeguridad _SeguridadService;


        #endregion

        #region CONTRUCTOR
        public WeatherForecastController(ISeguridad SeguridadService)
        {
            _SeguridadService = SeguridadService;
        }
        #endregion        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var Result = await _SeguridadService.VerificaConexion();
                if (Result == true)
                {
                    return Ok("La conexion es Exitosa");
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
    }
}
