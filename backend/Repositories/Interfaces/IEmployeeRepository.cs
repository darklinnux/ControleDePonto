using backend.Domain.Entities;
using System.Linq.Expressions;

namespace backend.Repositories.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();

        Task<Employee?> GetEmployeeAsync(Expression<Func<Employee, bool>> predicate);
    }
}
