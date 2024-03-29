using backend.Domain.Entities;
using backend.DTOs;

namespace backend.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<Employee> Add(EmployeeDTOCreate employeeDTO);
        Task<Employee> Update(EmployeeDTO employeeDTO);
        Task<Employee> Delete(int id);
        Task<Employee?> GetAsync(int id);
        Task<IEnumerable<Employee>> GetAllAsync();
    }
}
