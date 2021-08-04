using MyWallWebAPI.Domain.Models;
using System;


namespace MyWallWebAPI
{
    public class Post
    {

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public DateTime Data { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
