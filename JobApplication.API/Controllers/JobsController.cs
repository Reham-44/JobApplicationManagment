using JobApplication.Application.DTOs.Job;
using JobApplication.Application.Features.Jobs.Commands;
using JobApplication.Application.Features.Jobs.Commands.CloseJob;
using JobApplication.Application.Features.Jobs.Commands.CreateJobByRecruiter;
using JobApplication.Application.Features.Jobs.Queries.GetAllJobs;
using JobApplication.Application.Interfaces.ServiceInterfaces;
using JobApplication.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
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
        private readonly IMediator _mediator;

        public JobsController(IJobService jobService, IMediator mediator)
        {
            _JobService = jobService;
            _mediator = mediator;

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

            var command = new CreateJobByRecruiterCommand
            {
                Title = dto.Title,
                Description = dto.Description,
                UserId = userId
            };

            var result = await _mediator.Send(command);

            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllJobsQuery());    
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

            await _mediator.Send(new CloseJobCommand { JobId = id, userId = userId });

            return NoContent();
        }

    }
}
