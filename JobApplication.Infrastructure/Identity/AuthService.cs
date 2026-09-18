using JobApplication.Application.DTOs.Auth;
using JobApplication.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace JobApplication.Infrastructure.Identity
{
    public class AuthService:IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJWTService _jwtService;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            IJWTService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDTO> RegisterAsync(RegisterDTO dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(
                user,
                dto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(
                    ", ",
                    result.Errors.Select(e => e.Description));

                throw new Exception(errors);
            }

            await _userManager.AddToRoleAsync(
                user,
                "Candidate");

            var roles = await _userManager.GetRolesAsync(user);

            var token = _jwtService.GenerateToken(
                user.Id,
                user.Email!,
                roles);

            return new AuthResponseDTO
            {
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(2)
            };
        }

        public async Task<AuthResponseDTO> LoginAsync(LoginDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                throw new Exception("Invalid email or password.");

            var validPassword =
                await _userManager.CheckPasswordAsync(
                    user,
                    dto.Password);

            if (!validPassword)
                throw new Exception("Invalid email or password.");

            var roles = await _userManager.GetRolesAsync(user);

            var token = _jwtService.GenerateToken(
                user.Id,
                user.Email!,
                roles);

            return new AuthResponseDTO
            {
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(2)
            };
        }
    }
}
