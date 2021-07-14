using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecDesp.Domain.Models;
using RecDesp.Domain.Services;

namespace RecDesp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CobrancaController : ControllerBase
    {
        private readonly ICobrancaService _cobrancaService;

        public CobrancaController(ICobrancaService cobrancaService)
        {
            _cobrancaService = cobrancaService;
        }

        [HttpGet]
        [Route("list-cobrancas")]
        public async Task<IActionResult> ListCobrancas()
        {
            try
            {
                List<Cobranca> cobrancas = await _cobrancaService.ListCobrancas();

                return Ok(cobrancas);
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
        [Route("list-by-status")]
        public async Task<IActionResult> ListCobrancasByStatus([FromQuery] int status)
        {
            try
            {
                List<Cobranca> cobrancas = await _cobrancaService.ListCobrancasByStatus(status);

                return Ok(cobrancas);
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
        public async Task<IActionResult> GetCobranca([FromQuery] long cobrancaId)
        {
            try
            {
                Cobranca newCobranca = await _cobrancaService.GetCobrancaById(cobrancaId);

                return Ok(newCobranca);
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
        public async Task<IActionResult> PostCobranca([FromQuery] long fromArea, long toArea, [FromBody] Cobranca cobranca)
        {
            try
            {
                cobranca.FromAreaId = fromArea;
                cobranca.ToAreaId = toArea;

                Cobranca newCobranca = await _cobrancaService.CreateCobranca(cobranca);

                return Ok(newCobranca);
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
        public async Task<IActionResult> DeleteCobranca([FromQuery] long cobrancaId)
        {
            try
            {
                bool cobranca = await _cobrancaService.DeleteCobranca(cobrancaId);

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

        [HttpPost]
        [Route("cobranca-response")]
        public async Task<IActionResult> CobrancaResponse([FromQuery] long cobrancaId, [FromQuery] int status)
        {
            try
            {
                Cobranca newCobranca = await _cobrancaService.CobrancaResponse(cobrancaId, status);

                return Ok(newCobranca);
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
