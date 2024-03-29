using backend.Domain.Entities;

namespace backend.Services.Interfaces
{
    public interface IProfileService
    {
        Task<IEnumerable<Profile>> GetProfilesAsync();
    }
}
