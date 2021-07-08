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
    public class TransacaoController : ControllerBase
    {
        private readonly ITransacaoService _transacaoService;
        private readonly IAreaService _areaService;

        public TransacaoController(ITransacaoService transacaoService, IAreaService areaService)
        {
            _transacaoService = transacaoService;
            _areaService = areaService;
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
        public async Task<IActionResult> GetTransacao([FromQuery] long id)
        {
            try
            {
                Transacao newTransacao = await _transacaoService.GetTransacaoById(id);

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
                // verificando se a área existe
                Area area = await _areaService.GetAreaById(areaId);

                if (area != null)
                {
                    transacao.Data = DateTime.Now; // salva a data de quando foi feita a transação
                    transacao.AreaId = areaId;
                    transacao.Area = area;
                    Transacao newTransacao = await _transacaoService.CreateTransacao(transacao);

                    return Ok(newTransacao);
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

        [HttpPut]
        [Route("put-by-id")]
        public async Task<IActionResult> PutTransacao([FromQuery] long id, [FromBody] Transacao transacao)
        {
            try
            {
                Area area = await _areaService.GetAreaById(transacao.AreaId);

                if (area != null)
                {
                    transacao.Id = id;
                    transacao.Data = DateTime.Now; // salva a data de quando foi feita a transação

                    Transacao newTransacao = await _transacaoService.UpdateTransacao(transacao);

                    return Ok(newTransacao);
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
        public async Task<IActionResult> DeleteArea([FromQuery] long id)
        {
            try
            {
                bool transacao = await _transacaoService.DeleteTransacao(id);
                if (transacao)
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
