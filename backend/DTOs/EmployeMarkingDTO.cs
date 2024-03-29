using backend.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class EmployeMarkingDTO
    {
        public int Id { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int MarkingId { get; set; }
        [Required]
        public DateTime DateTime { get; set; }

    }
}
