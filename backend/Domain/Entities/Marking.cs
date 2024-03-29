using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Collections.ObjectModel;

namespace backend.Domain.Entities
{
    public class Marking
    {
        public Marking()
        {
            EmployeeMarkings = new Collection<EmployeeMarking>();
        }

        public int Id { get; set; }

        [StringLength(10)]
        [Required]
        public string? Name { get; set; }

        [JsonIgnore]
        //public ICollection<Employee> Employees { get; set; }
        public ICollection<EmployeeMarking> EmployeeMarkings { get; set; }
    }
}
