using backend.Exceptions;
using System.Security.Claims;

namespace backend.Extension
{
    public static class ClaimsPrincipalExtension
    {
        public static int GetProfileId(this ClaimsPrincipal user) 
        {
            try
            {
                return int.Parse(user.FindFirst("profileid").Value);
            }
            catch
            {
                throw new ErrorServiceException("Ocorreu um erro ao tentar acessar o profile ID do usuário logado ");
            }
        }

        public static int GetUserId(this ClaimsPrincipal user)
        {
            return int.Parse(user.FindFirst("id").Value);
        }
    }
}

