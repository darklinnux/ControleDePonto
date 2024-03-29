using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backend.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        [StringLength(80)]
        [Required]
        public string? Login { get; set; }

        [JsonIgnore]
        public byte[]? PasswordHash { get; set; }

        [JsonIgnore]
        public byte[]? PasswordSalt { get; set; }

        [Required]
        public int ProfileId { get; set; }
        
        public Profile? Profile { get; set; }
    }
}
