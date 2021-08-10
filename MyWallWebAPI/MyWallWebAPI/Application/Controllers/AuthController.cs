using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWallWebAPI.Domain.Models;
using MyWallWebAPI.Domain.Models.DTOs;
using MyWallWebAPI.Domain.Services.Implementations;
using MyWallWebAPI.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWallWebAPI.Application.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("sign-up")]
        public async Task<ActionResult> SignUp([FromBody] SignUpDTO signUpDTO)
        {
            try
            {
                bool ret = await _authService.SignUp(signUpDTO);

                return Ok(ret);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("sign-in")]
        public async Task<ActionResult> SignIn([FromBody] SignInDTO signInDTO)
        {
            try
            {
                SsoDTO ssoDTO = await _authService.SignIn(signInDTO);

                return Ok(ssoDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-current-user")]
        public async Task<ActionResult> GetCurrentUser()
        {
            try
            {
                ApplicationUser currentUser = await _authService.GetCurrentUser();

                return Ok(currentUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
