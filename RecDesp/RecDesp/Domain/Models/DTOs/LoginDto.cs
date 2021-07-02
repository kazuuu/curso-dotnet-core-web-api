using System.ComponentModel.DataAnnotations;

namespace RecDesp.Domain.Models.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "O Nome de Usuário é obrigatório")]
        public string Username { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Password { get; set; }
    }
}
