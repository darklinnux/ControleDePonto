using backend.DTOs;
using FluentValidation;

namespace backend.Validations
{
    public class EmployeeMarkingDTOValidation : AbstractValidator<EmployeMarkingDTO>
    {
        public EmployeeMarkingDTOValidation()
        {
            
                RuleFor(dto => dto.EmployeeId)
                    .NotEmpty().WithMessage("ID do funcionário é obrigatório");

                RuleFor(dto => dto.MarkingId)
                    .NotEmpty().WithMessage("ID de marcação é obrigatório");

                RuleFor(dto => dto.DateTime)
                    .NotEmpty().WithMessage("Data e hora são obrigatórias")
                    .Must(BeAValidDateTime).WithMessage("Formato de data e hora inválido");
            }

            private bool BeAValidDateTime(DateTime dateTime)
            {
                // Verificar se a data e hora estão em um formato válido
                return dateTime != DateTime.MinValue && dateTime != DateTime.MaxValue;
            }
        
    }
}
