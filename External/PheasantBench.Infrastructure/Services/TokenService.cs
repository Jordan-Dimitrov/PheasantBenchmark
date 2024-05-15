using Microsoft.IdentityModel.Tokens;
using PheasantBench.Application.Abstractions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PheasantBench.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private const string _Secret = "67Gew42tTllA9bWe4afnH4kcIZRq6BigEVoIi5YLVlg8xSMZlRAAxHbU35U4W9SX";
        public string CreateToken(string username)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_Secret));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddYears(999),
                signingCredentials: creds);

            string jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public string GetUsername(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(token);

            string username = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;

            return username;
        }
    }
}
