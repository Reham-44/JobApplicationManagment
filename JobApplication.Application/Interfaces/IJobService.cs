using JobApplication.Application.DTOs.JobDtos;
using JobApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Interfaces
{
    public interface IJobService
    {
        public Task<Job> CreateAsync(CreateJobDto createJobDto);

    }
}
