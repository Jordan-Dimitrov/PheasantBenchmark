using Microsoft.IdentityModel.Tokens;
using PheasantBench.Application.Abstractions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PheasantBench.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        public string CreateToken(string username)
        {
            return username;
        }

        public string GetUsername(string token)
        {
            return token;
        }
    }
}
