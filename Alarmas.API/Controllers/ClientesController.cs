using Alarmas.Core.BL.Clientes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alarmas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : Controller
    {
        #region PROPIEDADES

        private readonly IClientes _ClientesService;


        #endregion

        #region CONTRUCTOR
        public ClientesController(IClientes ClientesService)
        {
            _ClientesService = ClientesService;
        }
        #endregion
        #region Metodos 
        [HttpGet("GetListaClientes")]
        public async Task<IActionResult> GetListaClientes()
        {
            try
            {
                var Result = await _ClientesService.GetListaClientes();
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
