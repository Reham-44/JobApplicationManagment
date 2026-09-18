using JobApplication.Application.DTOs.Auth;
using JobApplication.Application.Interfaces;
using JobApplication.Domain.Entities;
using JobApplication.Infrastructure.Persistence;
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
        private readonly ApplicationDbContext _context;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            IJWTService jwtService,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _context = context;
        }

        public async Task<AuthResponseDTO> RegisterCandidateAsync(
     CandidateRegisterDTO dto)
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


                return new AuthResponseDTO
                {
                    Success = false,
                    Message = errors
                };
            }

            var roleResult = await _userManager.AddToRoleAsync(
                user,
                "Candidate");

            if (!roleResult.Succeeded)
            {

                return new AuthResponseDTO
                {
                    Success = false,
                    Message = "Failed to assign candidate role."
                };
            }

            var candidate = new Candidate
            {
                UserId = user.Id,
                Name = dto.Name
            };

            await _context.Candidates.AddAsync(candidate);
            await _context.SaveChangesAsync();

            return await GenerateAuthResponseAsync(user);
        }
        public async Task<AuthResponseDTO> RegisterRecruiterAsync(
    RecruiterRegisterDTO dto)
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


                return new AuthResponseDTO
                {
                    Success = false,
                    Message = errors
                };
            }

            var roleResult = await _userManager.AddToRoleAsync(
                user,
                "Recruiter");

            if (!roleResult.Succeeded)
            {

                return new AuthResponseDTO
                {
                    Success = false,
                    Message = "Failed to assign candidate role."
                };
            }

            var recruiter = new Recruiter
            {
                UserId = user.Id,
                Name = dto.Name,
                CompanyName = dto.CompanyName
            };

            await _context.Recruiters.AddAsync(recruiter);
            await _context.SaveChangesAsync();

            return await GenerateAuthResponseAsync(user);
        }
        public async Task<AuthResponseDTO> LoginAsync(LoginDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
            {
                return new AuthResponseDTO
                {
                    Success = false,
                    Message = "Invalid email or password."
                };
            }

            var validPassword =
                await _userManager.CheckPasswordAsync(
                    user,
                    dto.Password);

            if (!validPassword)
            {
                return new AuthResponseDTO
                {
                    Success = false,
                    Message = "Invalid email or password."
                };
            }

            return await GenerateAuthResponseAsync(user);
        }
        private async Task<AuthResponseDTO> GenerateAuthResponseAsync(
    ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var token = _jwtService.GenerateToken(
                user.Id,
                user.Email!,
                roles);

            return new AuthResponseDTO
            {
                Success = true,
                Message = "Authentication successful.",
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(2),
            };
        }
    }
}
