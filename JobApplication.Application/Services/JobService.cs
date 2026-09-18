using JobApplication.Application.DTOs.JobDtos;
using JobApplication.Application.Interfaces;
using JobApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;

        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<Job> CreateAsync(CreateJobDto createJobDto)
        {   
            var job = new Job()
            {
                Title = createJobDto.Title,
                Description = createJobDto.Description,
                IsActive = true
            };
            await _jobRepository.InsertAsync(job);
            await _jobRepository.SaveChangesAsync();

            return job; 
        }
    }
}
