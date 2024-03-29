using backend.Domain.Entities;

namespace backend.Services.Interfaces
{
    public interface IAuthenticateService
    {
        Task<bool> AuthenticateAsync(string login, string password);
        Task<bool> userExistis(string login);
        string? GenerateToken(int id, string login, int profileId);
        Task<User?> GetUserByLogin(string login);
    }
}
