using AutoMapper;
using backend.Domain.Entities;
using backend.DTOs;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace backend.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<User> _repository;

        public UserService(IMapper mapper, IRepository<User> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public UserDTO Add(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            if (userDTO.password != null)
            {
                using var hmac = new HMACSHA512();
                byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.password));
                byte[] passwordSalt = hmac.Key;

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }
            var userCreated = _repository.Create(user);
            return _mapper.Map<UserDTO>(userCreated);
        }

        public UserDTO Add(string login, string password, int profileId)
        {
            var user = new User 
            { 
                Login = login,
                ProfileId = profileId
            };
            if (password != null)
            {
                using var hmac = new HMACSHA512();
                byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                byte[] passwordSalt = hmac.Key;

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }
            var userCreated = _repository.Create(user);
            return _mapper.Map<UserDTO>(userCreated);
        }

        public async Task<UserDTO?> Delete(UserDTO userDTO)
        {
            var user = await _repository.GetAsync(t => t.Id == userDTO.Id);
            if (user is null)
            {
                throw new ArgumentException("Dados do usuários informado não existe");
            }

            _repository.Delete(user);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);

        }

        public async Task<UserDTO?> GetAsync(int id)
        {
            var user = await _repository.GetAsync(u => u.Id == id);
            return _mapper.Map<UserDTO>(user);
        }

        public UserDTO Update(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            _repository.Update(user);
            return _mapper.Map<UserDTO>(user);
        }
    }
}
