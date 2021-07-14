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
    public class TransacaoController : ControllerBase
    {
        private readonly ITransacaoService _transacaoService;

        public TransacaoController(ITransacaoService transacaoService)
        {
            _transacaoService = transacaoService;
        }

        [HttpGet]
        [Route("list-transacoes")]
        public async Task<IActionResult> ListTransacoes()
        {
            try
            {
                List<Transacao> transacoes = await _transacaoService.ListTransacao();

                return Ok(transacoes);
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
        [Route("list-by-origem")]
        public async Task<IActionResult> ListTransacaoByOrigem([FromQuery] int origem)
        {
            try
            {
                List<Transacao> transacoes = await _transacaoService.ListTransacaoByOrigem(origem);

                return Ok(transacoes);
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
        public async Task<IActionResult> GetTransacao([FromQuery] long transacaoId)
        {
            try
            {
                Transacao newTransacao = await _transacaoService.GetTransacaoById(transacaoId);

                return Ok(newTransacao);
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
        public async Task<IActionResult> PostTransacao([FromQuery] long areaId, [FromBody] Transacao transacao)
        {
            try
            { 
                transacao.AreaId = areaId;
                Transacao newTransacao = await _transacaoService.CreateTransacao(transacao);

                return Ok(newTransacao);
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

        [HttpPut]
        [Route("put-by-id")]
        public async Task<IActionResult> PutTransacao([FromQuery] long transacaoId, [FromBody] Transacao transacao)
        {
            try
            {
                transacao.Id = transacaoId;
                Transacao newTransacao = await _transacaoService.UpdateTransacao(transacao);

                return Ok(newTransacao);
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
        public async Task<IActionResult> DeleteArea([FromQuery] long transacaoId)
        {
            try
            {
                bool transacao = await _transacaoService.DeleteTransacao(transacaoId);

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
