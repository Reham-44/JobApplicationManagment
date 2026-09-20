using JobApplication.Application.DTOs.Job;
using JobApplication.Application.Interfaces.ServiceInterfaces;
using JobApplication.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobApplication.API.Controllers
{
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

            if (userId == null)
            {
                return Unauthorized();
            }
            var result =
                await _JobService.CreateAsync(dto, userId!);

            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _JobService.GetAll();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _JobService.GetById(id);
             return Ok(result);
        }

        [Authorize(Roles = "Recruiter")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
             int id,
             JobRequestDTO dto)
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var result = await _JobService.Update(id, dto, userId);

            return Ok(result);
        }
        [Authorize(Roles = "Recruiter")]
        [HttpPut("{id}/close")]
        public async Task<IActionResult> Close(int id)
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            await _JobService.CloseAsync(id, userId);

            return NoContent();
        }

    }
}
