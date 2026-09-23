using JobApplication.Application.DTOs.Job;
using JobApplication.Application.Interfaces.RepositoryInterfaces;
using JobApplication.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Features.Jobs.Commands.CreateJobByRecruiter
{
    public class CreateJobByRecruiterCommandHandler : IRequestHandler<CreateJobByRecruiterCommand, JobResponseDTO>
    {
        private readonly IGenericRepository<Job> _jobRepository;
        private readonly IRecruiterRepository _recruiterRepository;
        public CreateJobByRecruiterCommandHandler(
            IGenericRepository<Job> jobRepository,
            IRecruiterRepository recruiterRepository)
        {
            _jobRepository = jobRepository;
            _recruiterRepository = recruiterRepository;
        }
        public async Task<JobResponseDTO> Handle(CreateJobByRecruiterCommand request, CancellationToken cancellationToken)
        {
            var recruiter = await _recruiterRepository.GetByUserIdAsync(request.UserId);

            if (recruiter == null)
            {
                throw new KeyNotFoundException(
                    "Recruiter profile not found.");
            }

            var job = new Job
            {
                Title = request.Title,
                Description = request.Description,
                IsActive = true,
                RecruiterId = recruiter.Id
            };

            await _jobRepository.AddAsync(job);
            await _jobRepository.SaveChangesAsync();

            return new JobResponseDTO
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                IsActive = job.IsActive,
                ClosedAt = job.ClosedAt
            };
        }
    }
}
