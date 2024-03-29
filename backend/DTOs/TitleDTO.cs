using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class TitleDTO
    {
        public int Id { get; set; }

        [StringLength(80)]
        [Required]
        public String? Name { get; set; }
    }
}
