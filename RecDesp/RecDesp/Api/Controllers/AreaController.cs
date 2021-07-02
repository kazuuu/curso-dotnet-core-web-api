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
        [Route("get-by-id")]
        public async Task<IActionResult> GetArea([FromQuery] long id)
        {
            try
            {
                Area newArea = await _areaService.GetAreaById(id);
                
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
        public async Task<IActionResult> PutArea([FromQuery] long id, [FromBody] Area area)
        {
            try
            {
                area.Id = id;

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
        public async Task<IActionResult> DeleteArea(long id)
        {
            try
            {
                bool area = await _areaService.DeleteArea(id);
                if (area)
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
        [Route("add-user")]
        public async Task<IActionResult> AddUser([FromQuery] long areaId, [FromQuery] string userName)
        {
            try
            {
                return Ok(await _areaService.AddUser(areaId, userName));
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
