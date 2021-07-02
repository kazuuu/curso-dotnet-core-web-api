using RecDesp.Domain.Models;
using System.Collections.Generic;

namespace RecDesp.Models
{
    public class Area : BaseModel<long>
    {
        public string NomeArea { get; set; }
        public double Saldo { get; set; }

        public Area()
        {
            Users = new List<ApplicationUser>();
        }
        public ICollection<ApplicationUser> Users { get; set; }
    }
}
