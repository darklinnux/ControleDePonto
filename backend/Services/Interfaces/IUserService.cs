using backend.DTOs;
namespace backend.Services.Interfaces
{
    public interface IUserService
    {
        UserDTO Add(UserDTO userDTO);
        UserDTO Add(string login, string password, int profileId);
        UserDTO Update(UserDTO userDTO);
        Task<UserDTO?> Delete(UserDTO userDTO);
        Task<UserDTO?> GetAsync(int id);
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<bool> userExistis(string login);


    }
}
