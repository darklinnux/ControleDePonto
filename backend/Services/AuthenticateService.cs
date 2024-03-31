using backend.Domain.Entities;
using backend.Exceptions;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace backend.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<User> _repository;

        private readonly IEmployeeService _employeeService;

        public AuthenticateService(IConfiguration configuration, IRepository<User> repository, IEmployeeService employeeService)
        {
            _configuration = configuration;
            _repository = repository;
            _employeeService = employeeService;
        }

        public async Task<bool> AuthenticateAsync(string login, string password)
        {
            var user = await _repository.GetAsync(u => u.Login.ToLower() == login.ToLower());
            if (user is null)
            {
                throw new ErrorServiceException ("Usuário ou senha incorretos");
            }
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computerHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computerHash.Length; i++)
            {
                if (computerHash[i] != user.PasswordHash[i])
                {
                    throw new ErrorServiceException("Usuário ou Senha incorretos");
                }
            }
            return true;
        }

        public async Task<string?> GenerateToken(int id, string login, int profileId)
        {
            var claims = new[]
            {
                new Claim("id", id.ToString()),
                new Claim("login", login),
                new Claim("profileid", profileId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            
            if (await _employeeService.isEmployeUser(id))
            {
                var employee = await _employeeService.GetEmployeByUserAsync(id);

                var extendedClaims = new Claim[claims.Length + 1];
                Array.Copy(claims, extendedClaims, claims.Length);
                extendedClaims[claims.Length] = new Claim("employeeid", employee.Id.ToString());
                claims = extendedClaims;
            }

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:secretKey"]));
            var credentils = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(10);
            JwtSecurityToken token = new JwtSecurityToken
                (
                    issuer: _configuration["Jwt:issuer"],
                    audience: _configuration["Jwt:audience"],
                    claims: claims,
                    expires: expiration,
                    signingCredentials: credentils
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<User?> GetUserByLogin(string login)
        {
            return await _repository.GetAsync(u => u.Login == login);
        }

        public async Task<bool> userExistis(string login)
        {
            var user = await _repository.GetAsync(u => u.Login.ToLower() == login.ToLower());
            if (user is null)
            {
               return false;
            }

            return true;
        }

    }
}
