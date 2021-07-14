using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecDesp.Domain.Models;
using RecDesp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecDesp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CreditoController : ControllerBase
    {
        private readonly ICreditoService _creditoService;

        public CreditoController(ICreditoService creditoService)
        {
            _creditoService = creditoService;
        }

        [HttpGet]
        [Route("list-creditos")]
        public async Task<IActionResult> ListCreditos()
        {
            try
            {
                List<Credito> creditos = await _creditoService.ListCreditos();

                return Ok(creditos);
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
        public async Task<IActionResult> ListCreditosByValor([FromQuery] double min, [FromQuery] double max)
        {
            try
            {
                List<Credito> creditos = await _creditoService.ListCreditosByValor(min, max);

                return Ok(creditos);
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
        public async Task<IActionResult> GetCredito([FromQuery] long creditoId)
        {
            try
            {
                Credito newCredito = await _creditoService.GetCreditoById(creditoId);

                return Ok(newCredito);
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
        public async Task<IActionResult> PostCredito([FromQuery] long areaId, [FromBody] Credito credito)
        {
            try
            {
                credito.AreaId = areaId;
                Credito newCredito = await _creditoService.CreateCredito(credito);

                return Ok(newCredito);
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
        public async Task<IActionResult> DeleteCredito([FromQuery] long creditoId)
        {
            try
            {
                bool credito = await _creditoService.DeleteCredito(creditoId);

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