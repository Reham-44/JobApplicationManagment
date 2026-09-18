using JobApplication.Application.DTOs.Job;
using JobApplication.Application.Interfaces;
using JobApplication.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobApplication.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobService _JobService;

        public JobsController(IJobService jobService)
        {
            _JobService = jobService;
        }

        [Authorize(Roles = "Recruiter")]
        [HttpPost]
        public async Task<IActionResult> Create(JobRequestDTO dto)
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            var result =
                await _JobService.CreateAsync(dto, userId!);

            return Ok(result);
        }
    }
}
