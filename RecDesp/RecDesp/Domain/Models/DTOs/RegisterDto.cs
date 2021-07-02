using RecDesp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecDesp.Domain.Models.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "O Nome de Usuário é obrigatório")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "O email é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Password { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        public string PasswordConfirm { get; set; }

        //public ICollection<Area> Areas { get; set; }
    }
}
