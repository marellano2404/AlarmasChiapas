using Alarmas.Core.BL.Clientes;
using Alarmas.Core.Models;
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
        [HttpPost("PostNuevoCliente")]
        public async Task<IActionResult>  PostNuevoCliente([FromBody]Cliente cliente)
        {
            try
            {
                var Result = await _ClientesService.PostNuevoCliente(cliente);
                return Ok(Result);              

            }
            catch (Exception)
            {
                return BadRequest("La Conexión no ha sido encontrado!");
            }
        }
        [HttpPut("PutCliente")]
        public async Task<IActionResult> PutCliente([FromBody] Cliente cliente)
        {
            try
            {
                var Result = await _ClientesService.PutCliente(cliente);
                return Ok(Result);

            }
            catch (Exception)
            {
                return BadRequest("La Conexión no ha sido encontrado!");
            }
        }
        [HttpDelete("DeleteCliente/{Id}")]
        public async Task<IActionResult> DeletetCliente(Guid Id)
        {
            try
            {
                var Result = await _ClientesService.DeleteCliente(Id);
                return Ok(Result);

            }
            catch (Exception)
            {
                return BadRequest("La Conexión no ha sido encontrado!");
            }
        }
        [HttpPost("PostNuevoUsuario")]
        public async Task<IActionResult> PostNuevoUsuario([FromBody] ClienteUsuario usuario)
        {
            try
            {
                var Result = await _ClientesService.PostNuevoUsuario(usuario);
                return Ok(Result);

            }
            catch (Exception)
            {
                return BadRequest("La Conexión no ha sido encontrado!");
            }
        }
        [HttpPost("PostNuevaInstalacion")]
        public async Task<IActionResult> PostNuevaInstalacion([FromBody] Instalacion instalacion)
        {
            try
            {
                var Result = await _ClientesService.PostNuevaInstalacion(instalacion);
                return Ok(Result);

            }
            catch (Exception)
            {
                return BadRequest("La Conexión no ha sido encontrado!");
            }
        }
        #endregion
    }
}
