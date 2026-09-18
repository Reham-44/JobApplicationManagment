using JobApplication.Application.DTOs.Auth;
using JobApplication.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register/candidate")]
        public async Task<IActionResult> RegisterCandidate(
            CandidateRegisterDTO dto)
        {
            var result = await _authService.RegisterCandidateAsync(dto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("register/recruiter")]
        public async Task<IActionResult> RegisterRecruiter(
            RecruiterRegisterDTO dto)
        {
            var result = await _authService.RegisterRecruiterAsync(dto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var result = await _authService.LoginAsync(dto);

            if (!result.Success)
            {
                return Unauthorized(result);
            }

            return Ok(result);
        }
    }
}