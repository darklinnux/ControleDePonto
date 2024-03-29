
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backend.Domain.Entities
{
    public class Employee
    {
        public Employee()
        {
            EmployeeMarkings = new Collection<EmployeeMarking>();
        }
        public int Id { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Nome e Obrigatório")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Matricula é obrigatorio")]
        public int Registration { get; set; }

        [Required(ErrorMessage = "Title ID é Obrigatório")]
        public int TitleId { get; set; }
        //[JsonIgnore]
        public Title? Title { get; set; }

        [Required(ErrorMessage = "ID do Usuário é obrigatório")]
        public int UserId { get; set; }
        //[JsonIgnore]
        public User? User { get; set; }

        [Required(ErrorMessage = "ID da Escala é obrigatório")]
        public int ScheduleId { get; set; }
        
        public Schedule? Schedule { get; set; }
        [JsonIgnore]
        public ICollection<EmployeeMarking> EmployeeMarkings { get; set; }
    }
}
