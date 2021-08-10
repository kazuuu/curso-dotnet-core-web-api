using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWallWebAPI.Domain.Models;
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
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("list-posts")]
        public async Task<ActionResult> ListPosts()
        {
            try
            {
                List<Post> list = await _postService.ListPosts();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("list-meus-posts")]
        public async Task<ActionResult> ListMeusPosts()
        {
            try
            {
                List<Post> list = await _postService.ListMeusPosts();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-post")]
        public async Task<ActionResult> GetPost([FromQuery] int postId)
        {
            try
            {
                Post post = await _postService.GetPost(postId);

                return Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("novo-post")]
        public async Task<ActionResult> NovoPost([FromBody] Post post)
        {
            try
            {
                post = await _postService.NovoPost(post);

                return Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("update-post")]
        public async Task<ActionResult> UpdatePost([FromBody] Post post)
        {
            try
            {
                return Ok(await _postService.UpdatePost(post));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("delete-post")]
        public async Task<ActionResult> DeletePost([FromBody] int postId)
        {
            try
            {
                return Ok(await _postService.DeletePostAsync(postId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
