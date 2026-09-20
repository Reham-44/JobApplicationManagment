using JobApplication.Application.DTOs.Application;
using JobApplication.Application.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationsController(
            IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [Authorize(Roles = "Candidate")]
        [HttpPost("{jobId}/applications")]
        public async Task<IActionResult> Apply(int jobId)
        {
            var userId =
                User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var applicationId =
                await _applicationService.ApplyAsync(
                    jobId,
                    userId);

          return Created("Application submitted successfully",  applicationId);
        }
        [Authorize(Roles = "Candidate")]
        [HttpGet("my")]
        public async Task<IActionResult> GetMyApplications()
        {
            var userId =
                User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var applications =
                await _applicationService
                    .GetMyApplicationsAsync(userId);

            return Ok(applications);
        }
        [Authorize(Roles = "Recruiter")]
        [HttpGet("recruiter")]
        public async Task<IActionResult> GetMyJobApplications()
        {
            var userId =
                User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var applications =
                await _applicationService
                    .GetMyJobApplicationsAsync(userId);

            return Ok(applications);
        }

        [Authorize(Roles = "Recruiter")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatus(
            int id,
            ApplicationStatusDTO dto)
        {
            var userId =
                User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            await _applicationService.UpdateStatusAsync(
                id,
                dto,
                userId);

            return Ok(new
            {
                message = "Application status updated successfully."
            });
        }


        [Authorize(Roles = "Candidate")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelApplication(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
            {
                return Unauthorized();
            }

            await _applicationService.CancelApplicationAsync(
                id,
                userId);

            return NoContent();
        }
    }
}
