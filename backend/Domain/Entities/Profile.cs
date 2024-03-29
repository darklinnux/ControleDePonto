using System.ComponentModel.DataAnnotations;

namespace backend.Domain.Entities
{
    public class Profile
    {
        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        public string? Name { get; set; }
    }
}
