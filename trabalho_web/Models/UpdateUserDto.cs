using System.ComponentModel.DataAnnotations;

namespace trabalho_web.Models
{
    public class UpdateUserDto
    {
        [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        public string Email { get; set; } = string.Empty;

        public string? Password { get; set; }
    }
}
