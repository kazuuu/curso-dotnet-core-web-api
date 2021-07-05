using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecDesp.Domain.Models;
using RecDesp.Domain.Services;
using RecDesp.Models;

namespace RecDesp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CobrancaController : ControllerBase
    {
        private readonly ICobrancaService _cobrancaService;
        private readonly IAreaService _areaService;

        public CobrancaController(ICobrancaService cobrancaService, IAreaService areaService)
        {
            _cobrancaService = cobrancaService;
            _areaService = areaService;
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
        [Route("get-by-id")]
        public async Task<IActionResult> GetCobranca([FromQuery] long id)
        {
            try
            {
                Cobranca newCobranca = await _cobrancaService.GetCobrancaById(id);

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
                // pegando as áreas para serem salvas na cobrança
                Area fromNewArea = await _areaService.GetAreaById(fromArea);
                Area toNewArea = await _areaService.GetAreaById(toArea);

                if (fromNewArea != null && toNewArea != null)
                {
                    cobranca.FromAreaId = fromNewArea.Id;
                    cobranca.ToAreaId = toNewArea.Id;
                    cobranca.Data = DateTime.Now; // salva a data de quando foi feita a cobrança

                    Cobranca newCobranca = await _cobrancaService.CreateCobranca(cobranca);

                    return Ok(newCobranca);
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
        public async Task<IActionResult> DeleteCobranca([FromQuery] long id)
        {
            try
            {
                bool cobranca = await _cobrancaService.DeleteCobranca(id);
                if (cobranca)
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

        [HttpPost]
        [Route("cobranca-response")]
        public async Task<IActionResult> CobrancaResponse([FromQuery] long cobrancaId, [FromQuery] int status)
        {
            try
            {
                Cobranca newCobranca = await _cobrancaService.CobrancaResponse(cobrancaId, status);

                if (newCobranca != null)
                    return Ok(newCobranca);
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
    }
}
