using backend.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Nome e Obrigatório")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Matricula é obrigatorio")]
        public int Registration { get; set; }

        [Required(ErrorMessage = "Title ID é Obrigatório")]
        public int TitleId { get; set; }

        [Required(ErrorMessage = "ID do Usuário é obrigatório")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "ID da Escala é obrigatório")]
        public int ScheduleId { get; set; }

    }
}
