using backend.Domain.Entities;

namespace backend.Services.Interfaces
{
    public interface IScheduleService
    {
        Task<IEnumerable<Schedule>> GetSchedulesAsync();
    }
}
