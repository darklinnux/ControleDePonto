using backend.Context;
using backend.Domain.Entities;
using backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace backend.Repositories
{
    public class EmployeeMarkingRepository : Repository<EmployeeMarking>, IEmployeeMarkingRepository
    {
        public EmployeeMarkingRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<EmployeeMarking>> GetAllEmployeesMarkingAsync()
        {
            var employeesMarking = await _context.EmployeeMarkings
                .Include(e => e.Employee)
                .Include(m => m.Marking)
                .Include(s => s.Employee.Schedule)
                .Include(t => t.Employee.Title)
                .ToListAsync();

            return employeesMarking;
        }

        public async Task<IEnumerable<EmployeeMarking?>> GetEmployeeMarkingAsync(Expression<Func<EmployeeMarking, bool>> predicate)
        {
            var employeMarkings = await _context.EmployeeMarkings
                .Include(e => e.Employee)
                .Include(m => m.Marking)
                .Include(s => s.Employee.Schedule)
                .Include(t => t.Employee.Title)
                .Where(predicate).ToListAsync();
            return employeMarkings;
        }

        public async Task<IEnumerable<EmployeeMarking>> GetAllEmployeesMarkingAsync(Expression<Func<EmployeeMarking, bool>> predicate) 
        {
            return await _context.EmployeeMarkings
                .Include(e => e.Employee)
                .Include(e => e.Employee.User)
                .Include(e => e.Marking)
                .Where(predicate)
                .ToListAsync();
        }
    }
}
