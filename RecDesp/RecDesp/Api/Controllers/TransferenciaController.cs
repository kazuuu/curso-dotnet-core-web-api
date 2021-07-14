using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecDesp.Domain.Models;
using RecDesp.Domain.Services;
using RecDesp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecDesp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferenciaController : ControllerBase
    {
        private readonly ITransferenciaService _transferenciaService;

        public TransferenciaController(ITransferenciaService transferenciaService)
        {
            _transferenciaService = transferenciaService;
        }

        [HttpGet]
        [Route("list-transferencias")]
        public async Task<IActionResult> ListTransferencias()
        {
            try
            {
                List<Transferencia> transferencias = await _transferenciaService.ListTransferencias();

                return Ok(transferencias);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("list-by-valor")]
        public async Task<IActionResult> ListTransferenciasByValor([FromQuery] double min, [FromQuery] double max)
        {
            try
            {
                List<Transferencia> transferencias = await _transferenciaService.ListTransferenciasByValor(min, max);

                return Ok(transferencias);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("get-by-id")]
        public async Task<IActionResult> GetTransferencia([FromQuery] long transferenciaId)
        {
            try
            {
                Transferencia newTransferencia = await _transferenciaService.GetTransferenciaById(transferenciaId);

                return Ok(newTransferencia);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostTransferencia([FromQuery] long fromAreaId, [FromQuery] long toAreaId, 
                                          [FromBody] Transferencia transferencia)
        {
            try
            {
                transferencia.FromAreaId = fromAreaId;
                transferencia.ToAreaId = toAreaId;

                Transferencia newTransferencia = await _transferenciaService.CreateTransferencia(transferencia);

                return Ok(newTransferencia);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete]
        [Route("delete-by-id")]
        public async Task<IActionResult> DeleteTransferencia([FromQuery] long transferenciaId)
        {
            try
            {
                bool transferencia = await _transferenciaService.DeleteTransferencia(transferenciaId);

                return NoContent();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
