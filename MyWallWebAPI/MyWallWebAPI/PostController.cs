using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallWebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly MySQLContext _context;

        public PostController(MySQLContext context)
        {
            _context = context;
        }

        [HttpGet("list-posts")]
        public async Task<ActionResult> ListPosts()
        {
            List<Post> list = await _context.Post.ToListAsync();

            return Ok(list);
        }

        [HttpGet("get-post")]
        public async Task<ActionResult> GetPost([FromQuery] int postId)
        {
            Post item = await _context.Post.FindAsync(postId);

            if (item == null)
            {
                return NoContent();
            }
            return Ok(item);
        }

        [HttpPost("new-post")]
        public async Task<ActionResult> NewPost([FromBody] Post post)
        {
            post.Data = DateTime.Now;

            var ret = await _context.Post.AddAsync(post);

            await _context.SaveChangesAsync();

            ret.State = EntityState.Detached;

            return Ok(ret.Entity);
        }

        [HttpPost("update-post")]
        public async Task<ActionResult> UpdatePost([FromBody] Post post)
        {
            _context.Entry(post).State = EntityState.Modified;

            return Ok(await _context.SaveChangesAsync());
        }

        [HttpPost("delete-post")]
        public async Task<ActionResult> DeletePost([FromBody] int id)
        {
            var item = await _context.Post.FindAsync(id);
            if (item == null)
            {
                return BadRequest();
            }

            _context.Post.Remove(item);
            await _context.SaveChangesAsync();

            return Ok(true);
        }
    }
}
