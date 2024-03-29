using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.Domain.Models
{
    public class EmployeeCreate
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Registration { get; set; }
        public int TitleId { get; set; }
        public int ScheduleId { get; set; }
        public string? login { get; set; }
        public string? password { get; set; }
        public int ProfileId { get; set; }
    }
}
