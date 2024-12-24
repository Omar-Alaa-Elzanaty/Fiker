using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SquadAsService.Application.Interfaces;
using SquadAsService.Domain.Domains.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SquadAsService.Infrastructure.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user, string role, bool rembemerMe = false)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecureKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Name, user.UserName!),
                new(ClaimTypes.Role,role)
            };

            if (user.Email is not null)
            {
                claims.Add(new Claim(ClaimTypes.Email, user.Email));
            }

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: rembemerMe == true ? DateTime.Now.AddDays(double.Parse(_configuration["Jwt:ExpireInDays"]!)) : DateTime.Now.AddHours(6),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
