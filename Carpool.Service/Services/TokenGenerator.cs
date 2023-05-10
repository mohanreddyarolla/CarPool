using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Carpool.Models.DBModels;
using System.Security.Claims;

namespace CarPool.Services
{
    public class TokenGenerator
    {
        public IConfiguration configuration;

        public TokenGenerator(IConfiguration _configuration)
        {
            configuration= _configuration;
        }

        public string GenerateToken(string userId)
        {
            var claims = new[]
            {
                new Claim("UserId", userId)
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer:configuration["Jwt:Issuer"],
                audience:configuration["Jwt:Audience"],
                claims:claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signInCredentials

                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
    }
}
