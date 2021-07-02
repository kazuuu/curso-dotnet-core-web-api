using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecDesp.Domain.Models.DTOs;
using RecDesp.Domain.Services;
using System;
using System.Threading.Tasks;

namespace RecDesp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            SsoDto ssoDto = await _userService.Login(loginDto);

            if (ssoDto == null)
                return Unauthorized();

            return Ok(ssoDto);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                bool ret = await _userService.Register(registerDto);

                return Ok(ret);
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
        [Route("current-user")]
        [Authorize]
        public async Task<IActionResult> getCurrentUser()
        {
            try
            {
                return Ok(await _userService.GetCurrentUser());
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

        // lista os usuários
        [HttpGet]
        [Route("list-users")]
        [Authorize]
        public async Task<IActionResult> ListUsers()
        {
            return Ok(await _userService.ListUsers());
        }
    }
}
