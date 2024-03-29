using backend.Domain.Entities;

namespace backend.Services.Interfaces
{
    public interface IMarkingService
    {
        Task<Marking?> GetMarkingAsync(int id);
        Task<IEnumerable<Marking>> GetAllAsync();
    }
}
