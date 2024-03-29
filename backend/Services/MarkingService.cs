using backend.Domain.Entities;
using backend.Exceptions;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;

namespace backend.Services
{
    public class MarkingService : IMarkingService
    {
        private readonly IRepository<Marking> _repository;

        public MarkingService(IRepository<Marking> repository)
        {
                _repository = repository;
        }

        public async Task<IEnumerable<Marking>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Marking?> GetMarkingAsync(int id)
        {
            var marking = await _repository.GetAsync(m => m.Id == id);
            if (marking == null)
            {
                throw new ErrorServiceException("Não foi Possivel encontrar a marcação do ID informado");
            }
            return marking;
        }
    }
}
