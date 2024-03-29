using backend.Domain.Entities;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;

namespace backend.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IRepository<Schedule> _repository;

        public ScheduleService(IRepository<Schedule> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Schedule>> GetSchedulesAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
