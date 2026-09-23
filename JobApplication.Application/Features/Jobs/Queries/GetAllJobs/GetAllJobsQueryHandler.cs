using JobApplication.Application.DTOs.Job;
using JobApplication.Application.Features.Jobs.Commands.CreateJobByRecruiter;
using JobApplication.Application.Interfaces.RepositoryInterfaces;
using JobApplication.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Features.Jobs.Queries.GetAllJobs
{
    public class GetAllJobsQueryHandler : IRequestHandler<GetAllJobsQuery, List<JobResponseDTO>>
    {
        private readonly IGenericRepository<Job> _jobRepository;

        public GetAllJobsQueryHandler(IGenericRepository<Job> jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<List<JobResponseDTO>> Handle(GetAllJobsQuery request, CancellationToken cancellationToken)
        {
            var jobs = await _jobRepository.GetAllAsync();

            return jobs.Select(job => new JobResponseDTO
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                IsActive = job.IsActive,
                ClosedAt = job.ClosedAt
            }).ToList();
        }
    }
}
