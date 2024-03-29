using backend.Context;
using backend.Domain.Entities;
using backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace backend.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            //Pesquisa personalizada para trazer algumas coisas a mais na consulta do Employee
           var employees = await _context.Employee
                .Include(t => t.Title) 
                .Include(s => s.Schedule)
                .Include(u => u.User.Profile)
                .ToListAsync();
            
            return employees;
        }

        public async Task<Employee?> GetEmployeeAsync(Expression<Func<Employee, bool>> predicate)
        {
            return await _context.Set<Employee>()
                .Include(t => t.Title)
                .Include(u => u.User)
                .Include(u => u.Schedule.ScheduleDays)
                .FirstOrDefaultAsync(predicate);
        }
    }
}
