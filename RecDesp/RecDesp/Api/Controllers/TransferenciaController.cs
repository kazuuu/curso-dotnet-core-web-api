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
        private readonly IAreaService _areaService;

        public TransferenciaController(ITransferenciaService transferenciaService, IAreaService areaService)
        {
            _transferenciaService = transferenciaService;
            _areaService = areaService;
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
        public async Task<IActionResult> GetTransferencia([FromQuery] long id)
        {
            try
            {
                Transferencia newTransferencia = await _transferenciaService.GetTransferenciaById(id);

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
                Area fromArea = await _areaService.GetAreaById(fromAreaId);
                Area toArea = await _areaService.GetAreaById(toAreaId);

                if (fromArea != null && toArea != null)
                {
                    transferencia.FromAreaId = fromAreaId;
                    transferencia.FromArea = fromArea;
                    transferencia.ToAreaId = toAreaId;
                    transferencia.ToArea = toArea;

                    Transferencia newTransferencia = await _transferenciaService.CreateTransferencia(transferencia);

                    return Ok(newTransferencia);
                }
                else
                    return NotFound();
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
        public async Task<IActionResult> DeleteTransferencia([FromQuery] long id)
        {
            try
            {
                bool transferencia = await _transferenciaService.DeleteTransferencia(id);
                if (transferencia)
                    return NoContent();

                return NotFound();
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
