using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MyWallWebAPI.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        [JsonIgnore]
        public List<Post> Posts { get; set; }
    }
}
