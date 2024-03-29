using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class ProfileDTO
    {
        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        public string? Name { get; set; }
    }
}
