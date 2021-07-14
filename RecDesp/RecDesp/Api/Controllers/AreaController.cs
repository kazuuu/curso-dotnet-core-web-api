using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecDesp.Domain.Services;
using RecDesp.Models;

namespace RecDesp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AreaController : ControllerBase
    {
        private readonly IAreaService _areaService;

        public AreaController(IAreaService areaService)
        {
            _areaService = areaService;
        }

        [HttpGet]
        [Route("list-areas")]
        public async Task<IActionResult> ListAreas()
        {
            try
            {
                List<Area> areas = await _areaService.ListAreas();
 
                return Ok(areas);
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
        [Route("list-by-saldo")]
        public async Task<IActionResult> ListAreasBySaldo([FromQuery] double min, [FromQuery] double max)
        {
            try
            {
                List<Area> areas = await _areaService.ListAreasBySaldo(min, max);

                return Ok(areas);
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
        public async Task<IActionResult> GetArea([FromQuery] long areaId)
        {
            try
            {
                Area newArea = await _areaService.GetAreaById(areaId);
                
                return Ok(newArea);
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
        public async Task<IActionResult> PostArea([FromBody] Area area)
        {
            try
            {
                Area newArea = await _areaService.CreateArea(area);

                return Ok(newArea);
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
        public async Task<IActionResult> PutArea([FromQuery] long areaId, [FromBody] Area area)
        {
            try
            {
                area.Id = areaId;
                Area newArea = await _areaService.UpdateArea(area);

                return Ok(newArea);
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
        public async Task<IActionResult> DeleteArea([FromQuery] long areaId)
        {
            try
            {
                bool area = await _areaService.DeleteArea(areaId);

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
        [Route("add-user")]
        public async Task<IActionResult> AddUserToArea([FromQuery] long areaId, [FromQuery] string userName)
        {
            try
            {
                return Ok(await _areaService.AddUserToArea(areaId, userName));
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
