using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EMC.BuildingBlocks.Context
{
    // BuildingBlocks / Infrastructure / Auth
    public interface IInternalTokenService
    {
        string GenerarTokenInterno(Guid companyId);
    }

    public class InternalTokenService : IInternalTokenService
    {
        private readonly IConfiguration _configuration;

        public InternalTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerarTokenInterno(Guid companyId)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, "estetica-service"),
            new Claim("CompanyId", companyId.ToString()),
            new Claim(ClaimTypes.Role, "InternalService"),
        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Authentication:jwtKey"]!));
            var issuer = _configuration["Authentication:Issuer"]!;
            var audience = _configuration["Authentication:Audience"]!;

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15), // corto, es M2M
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
