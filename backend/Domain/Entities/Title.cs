using System.ComponentModel.DataAnnotations;

namespace backend.Domain.Entities
{
    public class Title
    {
        public int Id { get; set; }

        [StringLength(80)]
        [Required]
        public string? Name { get; set; }
    }
}
