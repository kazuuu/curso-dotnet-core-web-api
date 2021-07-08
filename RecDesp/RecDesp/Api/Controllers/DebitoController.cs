﻿using Microsoft.AspNetCore.Authorization;
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
        private readonly IAreaService _areaService;
        private readonly IInstituicaoFinanceiraService _instituicaoService;

        public DebitoController(IDebitoService debitoService, IAreaService areaService, IInstituicaoFinanceiraService instituicaoService)
        {
            _debitoService = debitoService;
            _areaService = areaService;
            _instituicaoService = instituicaoService;
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
        public async Task<IActionResult> GetDebitos([FromQuery] long id)
        {
            try
            {
                Debito newDebito = await _debitoService.GetDebitoById(id);

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
                Area area = await _areaService.GetAreaById(areaId);
                InstituicaoFinanceira instituicao = await _instituicaoService.GetInstituicaoById(instituicaoId);

                if (area != null && instituicao != null)
                {
                    debito.AreaId = area.Id;
                    debito.Area = area;
                    debito.InstituicaoId = instituicao.Id;
                    debito.Instituicao = instituicao;
                    Debito newDebito = await _debitoService.CreateDebito(debito);

                    return Ok(newDebito);
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
        public async Task<IActionResult> DeleteDebito([FromQuery] long id)
        {
            try
            {
                bool debito = await _debitoService.DeleteDebito(id);
                if (debito)
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