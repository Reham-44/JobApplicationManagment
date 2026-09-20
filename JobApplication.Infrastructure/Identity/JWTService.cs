using JobApplication.Application.Interfaces.ServiceInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JobApplication.Infrastructure.Identity
{
    public class JWTService : IJWTService
    {
        private readonly IConfiguration _configuration;

        public JWTService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public string GenerateToken(
            string userId,
            string email,
            IEnumerable<string> roles)
        {
            var jwtSettings = _configuration.GetSection("Jwt");

            var key = jwtSettings["Key"]
                ?? throw new InvalidOperationException("JWT Key is not configured.");

            var issuer = jwtSettings["Issuer"]
                ?? throw new InvalidOperationException("JWT Issuer is not configured.");

            var audience = jwtSettings["Audience"]
                ?? throw new InvalidOperationException("JWT Audience is not configured.");

            var duration = int.Parse(
                jwtSettings["DurationInMinutes"] ?? "60");

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Email, email)
        };

            claims.AddRange(
                roles.Select(role =>
                    new Claim(ClaimTypes.Role, role)));

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key));

            var credentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256);

            var expiresAt = DateTime.UtcNow.AddMinutes(duration);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }

    }
}