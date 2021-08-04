﻿using MyWallWebAPI.Domain.Models;
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

        private readonly AuthService _authService;

        public PostService(PostRepository postRepository, AuthService authService)
        {
            _authService = authService;
            _postRepository = postRepository;
        }

        public async Task<List<Post>> ListPosts()
        {
            List<Post> list = await _postRepository.ListPosts();

            return list;
        }

        public async Task<List<Post>> ListMeusPosts()
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            List<Post> list = await _postRepository.ListPostsByUserId(currentUser.Id);

            return list;
        }

        public async Task<Post> GetPost(int postId)
        {
            Post post = await _postRepository.GetPostById(postId);

            if (post == null)
                throw new ArgumentException("Post não existe!");

            return post;
        }

        public async Task<Post> NovoPost(Post post)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            Post novoPost = new Post();

            novoPost.ApplicationUserId = currentUser.Id;
            novoPost.Data = DateTime.Now;
            novoPost.Conteudo = post.Conteudo;
            novoPost.Titulo = post.Titulo;

            novoPost = await _postRepository.CreatePost(novoPost);

            return novoPost;
        }

        public async Task<int> UpdatePost(Post post)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            Post findPost = await _postRepository.GetPostById(post.Id);
            if (findPost == null)
                throw new ArgumentException("Post não existe!");

            if (!findPost.ApplicationUserId.Equals(currentUser.Id))
                throw new ArgumentException("Sem permissão.");

            findPost.Titulo = post.Titulo;
            findPost.Conteudo = post.Conteudo;

            return await _postRepository.UpdatePost(findPost);
        }

        public async Task<bool> DeletePost(int postId)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            Post findPost = await _postRepository.GetPostById(postId);
            if (findPost == null)
                throw new ArgumentException("Post não existe!");

            if (!findPost.ApplicationUserId.Equals(currentUser.Id))
                throw new ArgumentException("Sem permissão.");

            await _postRepository.DeletePost(postId);

            return true;
        }
    }
}
