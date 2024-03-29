using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

        [Required]
        public string? login { get; set; }

        [Required]
        [NotMapped]
        public string? password { get; set; }
        public int ProfileId { get; set; }
    }
}
