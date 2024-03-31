using backend.Domain.Models;
using backend.DTOs;
using backend.Exceptions;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;

namespace backend.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly IUserService _userService;
        public UserController(IAuthenticateService authenticateService, IUserService userService)
        {
            _authenticateService = authenticateService;
            _userService = userService;
        }

        [HttpPost("registrer")]
        public async Task<ActionResult<UserToken>> Add(UserDTO userDTO)
        {
            try
            {
                if (userDTO == null)
                {
                    return BadRequest("Dados invalidos");
                }

                var userExist = await _authenticateService.userExistis(userDTO.login);

                if (userExist)
                {
                   throw new ErrorServiceException("Usuário já Existe");
                }

                var user = _userService.Add(userDTO);
                if (user == null)
                {
                    throw new ErrorServiceException("Ocorreu um erro ao cadastrar o usuário");
                }

                var token =  await _authenticateService.GenerateToken(user.Id, user.login, user.ProfileId);


                return new UserToken
                {
                    Token = token
                };
            }
            catch (ErrorServiceException e)
            {

                return BadRequest (new {error = e.Message});
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserToken>> Login(UserLogin userLogin)
        {
            try
            {
                var isExists = await _authenticateService.userExistis(userLogin.Login);
                if (!isExists)
                {
                    new ErrorServiceException ("Usuário ou senha incorretos");
                }
    
                var result = await _authenticateService.AuthenticateAsync(userLogin.Login, userLogin.Password);
                if (!result)
                {
                    new ErrorServiceException ("Usuário ou senha incorretos");
                }
    
                var user = await _authenticateService.GetUserByLogin(userLogin.Login);
    
                var token = await _authenticateService.GenerateToken(user.Id, user.Login, user.ProfileId);
                return new UserToken
                {
                    Token = token
                };
            }
            catch (ErrorServiceException e)
            {
                
                return BadRequest(new {error = e.Message});
            }
        }
    }
}
