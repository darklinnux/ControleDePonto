using backend.Domain.Entities;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;

namespace backend.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IRepository<Profile> _repository;

        public ProfileService(IRepository<Profile> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Profile>> GetProfilesAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
