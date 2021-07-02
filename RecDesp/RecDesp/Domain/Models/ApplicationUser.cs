using Microsoft.AspNetCore.Identity;
using RecDesp.Models;
using System.Collections.Generic;

namespace RecDesp.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Area> Areas { get; set; }
    }
}
