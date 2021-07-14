using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class DebitoController : ControllerBase
    {
        private readonly IDebitoService _debitoService;

        public DebitoController(IDebitoService debitoService)
        {
            _debitoService = debitoService;
        }

        [HttpGet]
        [Route("list-debitos")]
        public async Task<IActionResult> ListDebitos()
        {
            try
            {
                List<Debito> debitos = await _debitoService.ListDebitos();

                return Ok(debitos);
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
        public async Task<IActionResult> ListDebitosByValor([FromQuery] double min, [FromQuery] double max)
        {
            try
            {
                List<Debito> debitos = await _debitoService.ListDebitosByValor(min, max);

                return Ok(debitos);
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
        public async Task<IActionResult> GetDebitos([FromQuery] long debitoId)
        {
            try
            {
                Debito newDebito = await _debitoService.GetDebitoById(debitoId);

                return Ok(newDebito);
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
        public async Task<IActionResult> PostDebito([FromQuery] long areaId, [FromQuery] int instituicaoId, [FromBody] Debito debito)
        {
            try
            {
                debito.AreaId = areaId;
                debito.InstituicaoId = instituicaoId;
                Debito newDebito = await _debitoService.CreateDebito(debito);

                return Ok(newDebito);
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
        public async Task<IActionResult> DeleteDebito([FromQuery] long debitoId)
        {
            try
            {
                bool debito = await _debitoService.DeleteDebito(debitoId);

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
