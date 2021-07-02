using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecDesp.Domain.Models;
using RecDesp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecDesp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InstituicaoFinanceiraController : ControllerBase
    {
        private readonly IInstituicaoFinanceiraService _instituicaoService;

        public InstituicaoFinanceiraController(IInstituicaoFinanceiraService instituicaoService)
        {
            _instituicaoService = instituicaoService;
        }

        [HttpGet]
        [Route("list-ifs")]
        public async Task<IActionResult> ListInstituicoes()
        {
            try
            {
                List<InstituicaoFinanceira> instituicoes = await _instituicaoService.ListInstituicoes();

                return Ok(instituicoes);
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
        public async Task<IActionResult> GetInstituicoes([FromQuery] int id)
        {
            try
            {
                InstituicaoFinanceira instituicao = await _instituicaoService.GetInstituicaoById(id);
                return Ok(instituicao);
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
