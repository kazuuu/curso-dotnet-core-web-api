using MyWallWebAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallWebAPI.Domain.Services.Interfaces
{
    public interface IPostService
    {
        Task<List<Post>> ListPosts();
        Task<List<Post>> ListMeusPosts();
        Task<Post> GetPost(int postId);
        Task<Post> NovoPost(Post post);
        Task<int> UpdatePost(Post post);
        Task<bool> DeletePostAsync(int postId);
    }
}
