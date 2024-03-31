using AutoMapper;
using backend.Domain.Entities;
using backend.DTOs;
using backend.Exceptions;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;

namespace backend.Services
{
    public class EmployeeMarkingService : IEmployeeMarkingService
    {
        private readonly IEmployeeMarkingRepository _repository;
        private readonly IMapper _mapper;
        private readonly IEmployeeService _employeeService;
        private readonly IMarkingService _markingService;
        public EmployeeMarkingService(IEmployeeMarkingRepository repository,IMapper mapper, IEmployeeService employeeService, IMarkingService markingService)
        {
            _repository = repository;
            _mapper = mapper;
            _employeeService = employeeService;
            _markingService = markingService;
        }

        public async Task<EmployeeMarking> Add(EmployeMarkingDTO employeeDTO)
        {
            await ValidateInsert(employeeDTO);
            var employeeMarking = _mapper.Map<EmployeeMarking>(employeeDTO);
            var employeeMarkingCreate = _repository.Create(employeeMarking);
            return employeeMarkingCreate;
        }

        public EmployeeMarking Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EmployeeMarking>> GetAllAsync()
        {
            return await _repository.GetAllEmployeesMarkingAsync();
        }

        public async Task<IEnumerable<EmployeeMarking?>> GetAsync(int id)
        {
            return await _repository.GetEmployeeMarkingAsync(e=> e.EmployeeId == id);
        }

        public async Task<EmployeeMarking> Update(EmployeMarkingDTO employeeDTO)
        {
            //await ValidateInsert(employeeDTO);

            var EmployeMarking = _mapper.Map<EmployeeMarking>(employeeDTO);
            return _repository.Update(EmployeMarking);
        }

        private async Task<bool> ValidateInsert(EmployeMarkingDTO employeeDTO)
        {
            var employee = await _employeeService.GetAsync(employeeDTO.EmployeeId);
            var marking = await _markingService.GetMarkingAsync(employeeDTO.MarkingId);

            // Obter as marcações do funcionário para a data especificada
            var employeeMarkings = await GetEmployeeMarkingsAsync(employeeDTO.EmployeeId, employeeDTO.DateTime.Date);


            // Verificar se o número de marcações excede o limite máximo
            if (employeeMarkings.Count() >= 2)
            {
                throw new ErrorServiceException("A quantidade máxima de batidas é duas por dia");
            }

            // Se não houver nenhuma batida e a marcação enviada for de entrada
            if (employeeMarkings.Count() < 1 && employeeDTO.MarkingId == 2)
            {
                throw new ErrorServiceException("Só pode haver uma saída se tiver uma entrada");
            }

            // Se já houver uma marcação e esta for uma marcação de entrada e se a marcação enviada for de entrada
            if (employeeMarkings.Any(m => m.MarkingId == 1) && employeeDTO.MarkingId == 1)
            {
                throw new ErrorServiceException("Só pode haver uma entrada por dia");
            }

            // Validar se a marcação está dentro do intervalo permitido conforme cadastrado na esclada
            if (!IsWithinWorkingHours(employeeDTO.DateTime, employee))
            {
                throw new ErrorServiceException("A marcação deve está de acordo com sua escala de trabalho");
            }

            //Valida se o dia da semana está na escala

            if (!IsDayOfWork(employeeDTO.DateTime,employee))
            {
                throw new ErrorServiceException("Dia da semana não condiz com a sua escala");
            }

            return true;
        }

        private async Task<IEnumerable<EmployeeMarking?>> GetEmployeeMarkingsAsync(int employeeId, DateTime date)
        {
            var result = await _repository.GetEmployeeMarkingAsync(e =>
                e.EmployeeId == employeeId &&
                e.DateTime.Date == date);
            return result;
        }

        private bool IsWithinWorkingHours(DateTime dateTime, Employee employee)
        {

            TimeSpan startTime = (TimeSpan)employee.Schedule.Start; // 08:00
            TimeSpan endTime = (TimeSpan)employee.Schedule.End;  // 18:00

            TimeSpan markingTime = dateTime.TimeOfDay;

            return markingTime >= startTime && markingTime <= endTime;
        }

        private bool IsDayOfWork(DateTime dateTime, Employee employee)
        {
            //Soma mais 1 dia para ficar de acordo com o banco de dados
            int markingDay = (int) dateTime.DayOfWeek + 1;
            foreach (var item in employee.Schedule.ScheduleDays)
             {
                if (markingDay == item.DayId)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<IEnumerable<EmployeeMarking?>> GetAsync(int id, DateTime initialDate, DateTime finalDate, int profileId,int userId)
        {
            int employeeId = (profileId != 1) ? userId : id;

            if (initialDate == DateTime.MinValue || finalDate == DateTime.MinValue)
            {
                return await _repository.GetAllEmployeesMarkingAsync(employee => employee.EmployeeId == employeeId);
            }

            if (finalDate < initialDate)
            {
                throw new ErrorServiceException("Verifique se as datas foram informadas corretamente.");
            }

            DateTime startDate = initialDate.Date;
            DateTime endDate = finalDate.Date.AddDays(1);

            return await _repository.GetAllEmployeesMarkingAsync(
                em => em.DateTime >= startDate && em.DateTime < endDate && em.EmployeeId == employeeId);
        }
    }
}
