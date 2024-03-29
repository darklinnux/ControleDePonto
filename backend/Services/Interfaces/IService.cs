using backend.DTOs;

namespace backend.Services.Interfaces
{
    public interface IService<E, S>
    {
        E Add(S entity);
        E Update(S entity);
        Task<E> Delete(S entity);
        Task<E> GetAsync(int id);
        Task<IEnumerable<E>> GetAllAsync();

    }
}
