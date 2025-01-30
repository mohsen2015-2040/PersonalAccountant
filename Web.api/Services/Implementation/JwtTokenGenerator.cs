using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Contract;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration _configuration;
        public JwtTokenGenerator(IConfiguration configuration) => _configuration = configuration;

        public string GenerateToken(string userName, string role, string Id)
        {

            var secretKey = Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Get<string>());
            var audience = _configuration.GetSection("Jwt:Audience").Get<string>();
            var issuer = _configuration.GetSection("Jwt:Issuer").Get<string>();

            var signingCredential = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256);

            var descriptor = new SecurityTokenDescriptor
            {
                Audience = audience,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, role),
                    new Claim(ClaimTypes.NameIdentifier, Id)
                }),
                Issuer = issuer,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = signingCredential
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(descriptor);

            return tokenHandler.WriteToken(securityToken);

        }
    }
}
