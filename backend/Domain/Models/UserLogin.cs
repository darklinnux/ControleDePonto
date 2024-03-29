using System.ComponentModel.DataAnnotations;

namespace backend.Domain.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "O e-mail é obriatório")]
        public string? Login { get; set; }
        [Required(ErrorMessage = "A senha é obrigatória")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
