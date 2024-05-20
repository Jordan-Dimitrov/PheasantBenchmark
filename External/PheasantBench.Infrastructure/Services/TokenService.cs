using PheasantBench.Application.Abstractions;

namespace PheasantBench.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        //TODO: add some form of tokens in the future
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
