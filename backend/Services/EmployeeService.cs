using AutoMapper;
using backend.Domain.Entities;
using backend.DTOs;
using backend.Exceptions;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;

namespace backend.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAuthenticateService _authenticateService;
        private readonly IUserService _userService;

        public EmployeeService(IEmployeeRepository repository, IMapper mapper, IAuthenticateService authenticateService, IUserService userService)
        {
            _repository = repository;
            _mapper = mapper;
            _authenticateService = authenticateService;
            _userService = userService;
        }

        public async Task<Employee> Add(EmployeeDTOCreate employeeDTO)
        {

            if (await _authenticateService.userExistis(employeeDTO.login))
            {
                throw new ErrorServiceException("Usuário já existe");
            }

            var userDTO = new UserDTO
            {
                login = employeeDTO.login,
                password = employeeDTO.password,
                ProfileId = employeeDTO.ProfileId
            };
            try
            {
                var employee = _mapper.Map<Employee>(employeeDTO);
                var user = _userService.Add(userDTO);
                employee.UserId = user.Id;
                var employCreated = _repository.Create(employee);
                return employCreated;   
            }
            catch 
            {

                throw new ErrorServiceException("Ocorreu um erro ao tentar criar um funcionario");
            }
            
        }

        public async Task<Employee> Delete(int id)
        {
            var employee = await GetAsync(id);
            var deleted = _repository.Delete(employee);
            return deleted;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            var employees = await _repository.GetAllEmployeesAsync();
            return employees;
        }

        public async Task<Employee?> GetAsync(int id)
        {
            var employee = await _repository.GetEmployeeAsync(e => e.Id == id)?? 
                throw new ErrorServiceException("Não foi Possivel encontrar o funcionario do ID informado");
            return employee;
        }

        public async Task<Employee> Update(EmployeeDTO employeeDTO)
        {
            await GetAsync(employeeDTO.Id);
            var employeeUpdate = _repository.Update(_mapper.Map<Employee>(employeeDTO));
            return employeeUpdate;
        }
    }
}
