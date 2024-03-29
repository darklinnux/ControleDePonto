
using backend.Domain.Models;
using backend.DTOs;
using FluentValidation;
using System.Data;

namespace backend.Validations
{
    public class EmployeeDTOCreateValidation : AbstractValidator<EmployeeDTOCreate>
    {
        public EmployeeDTOCreateValidation()
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

            RuleFor(employee => employee.login)
                .MinimumLength(3).WithMessage("Login dever conter no minimo 3 caracteres")
                .MaximumLength(20).WithMessage("Login deve ter no máximo 20 caracteres")
                .NotEmpty().WithMessage("Login é obrigatório");

            RuleFor(employee => employee.password)
                .MinimumLength(6).WithMessage("Password deve ter no minimo 6 caracteres")
                .MaximumLength(16).WithMessage("Password dever conter no máximo 16 caracteres")
                .NotEmpty().WithMessage("Senha é obrigatória");

            RuleFor(employee => employee.ProfileId)
                .NotEmpty().WithMessage("ID do Perfil é obrigatório");

        }
    }
}
