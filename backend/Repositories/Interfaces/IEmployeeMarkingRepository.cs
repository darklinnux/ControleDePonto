using backend.Domain.Entities;
using System.Linq.Expressions;

namespace backend.Repositories.Interfaces
{
    public interface IEmployeeMarkingRepository : IRepository<EmployeeMarking>
    {
        Task<IEnumerable<EmployeeMarking>> GetAllEmployeesMarkingAsync();
        Task<IEnumerable<EmployeeMarking?>> GetEmployeeMarkingAsync(Expression<Func<EmployeeMarking, bool>> predicate);
        Task<IEnumerable<EmployeeMarking>> GetAllEmployeesMarkingAsync(Expression<Func<EmployeeMarking, bool>> predicate);


    }
}
