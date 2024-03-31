using System.Reflection.Metadata.Ecma335;
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
        private readonly IUserService _userService;

        public EmployeeService(IEmployeeRepository repository, IMapper mapper, IUserService userService)
        {
            _repository = repository;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<Employee> Add(EmployeeDTOCreate employeeDTO)
        {

            if (await _userService.userExistis(employeeDTO.login))
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

        public async Task<Employee?> GetEmployeByUserAsync(int userId)
        {
            var employee = await _repository.GetEmployeeAsync(e => e.UserId == userId)?? 
                throw new ErrorServiceException("Não foi Possivel encontrar o funcionario do ID de usuário informado");
            return employee;
        }

        public async Task<Employee> Update(EmployeeDTO employeeDTO)
        {
            await GetAsync(employeeDTO.Id);
            var employeeUpdate = _repository.Update(_mapper.Map<Employee>(employeeDTO));
            return employeeUpdate;
        }

        public async Task<bool> isEmployeUser(int userId)
        {
            var user = await _repository.GetAsync(emp => emp.UserId == userId);
            if(user is null)
            {
                return false;
            }

            return true;
            
        }
    }
}
