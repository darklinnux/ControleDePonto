using backend.Domain.Entities;
using backend.DTOs;
using System.Linq.Expressions;

namespace backend.Services.Interfaces
{
    public interface IEmployeeMarkingService
    {
        Task<EmployeeMarking> Add(EmployeMarkingDTO employeeDTO);
        Task<EmployeeMarking> Update(EmployeMarkingDTO employeeDTO);
        EmployeeMarking Delete(int id);
        Task<IEnumerable<EmployeeMarking?>> GetAsync(int id);
        Task<IEnumerable<EmployeeMarking?>> GetAsync(int id, DateTime initialDate, DateTime finalDate, int profileId, int userId);
        Task<IEnumerable<EmployeeMarking>> GetAllAsync();
    }
}
