using JobApplication.Application.DTOs.JobDtos;
using JobApplication.Application.Interfaces;
using JobApplication.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateJobDto createJobDto)
        {
            var job = await _JobService.CreateAsync(createJobDto);
            return Ok(job); 
        }
    }
}
