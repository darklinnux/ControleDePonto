using backend.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.DTOs
{
    public class EmployeeDTOCreate
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public int Registration { get; set; }

        public int TitleId { get; set; }

        public int ScheduleId { get; set; }

        [NotMapped]
        public string? login { get; set; }

        [NotMapped]
        public string? password { get; set; }
        [NotMapped]
        public int ProfileId { get; set; }
    }
}
