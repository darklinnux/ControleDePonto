using backend.DTOs;
using FluentValidation;

namespace backend.Validations
{
    public class EmployeeDTOValidation : AbstractValidator<EmployeeDTO>
    {
        public EmployeeDTOValidation()
        {
            RuleFor(employee => employee.Name)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MaximumLength(255).WithMessage("Nome deve ter no máximo 255 caracteres");

            RuleFor(employee => employee.Registration)
                .NotEmpty().WithMessage("Matrícula é obrigatória");

            RuleFor(employee => employee.TitleId)
                .NotEmpty().WithMessage("Title ID é obrigatório");

            RuleFor(employee => employee.ScheduleId)
                .NotEmpty().WithMessage("ID da Escala é obrigatório");
        }
    }
}
