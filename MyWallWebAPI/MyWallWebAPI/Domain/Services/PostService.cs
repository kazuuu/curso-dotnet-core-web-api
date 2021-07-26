using MyWallWebAPI.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallWebAPI.Domain.Services
{
    public class PostService
    {
        private readonly PostRepository _postRepository;

        public PostService(PostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<List<Post>> ListPosts()
        {
            List<Post> list = await _postRepository.ListPosts();

            return list;
        }

        public async Task<Post> GetPost(int postId)
        {
            Post post = await _postRepository.GetPost(postId);

            if (post == null)
                throw new ArgumentException("Post não existe!");

            return post;
        }

        public async Task<Post> CreatePost(Post post)
        {
            post.Data = DateTime.Now;

            post = await _postRepository.CreatePost(post);

            return post;
        }

        public async Task<int> UpdatePost(Post post)
        {
            return await _postRepository.UpdatePost(post);
        }

        public async Task<bool> DeletePost(int postId)
        {
            Post fndPost = await _postRepository.GetPost(postId);
            if (fndPost == null)
                throw new ArgumentException("Post não existe!");

            await _postRepository.DeletePost(postId);

            return true;
        }
    }
}
