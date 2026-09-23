using JobApplication.Application.DTOs.Job;
using JobApplication.Application.Interfaces.RepositoryInterfaces;
using JobApplication.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Features.Jobs.Commands.CloseJob
{
    public class CloseJobCommandHandler : IRequestHandler<CloseJobCommand>
    {
        private readonly IGenericRepository<Job> _jobRepository;
        private readonly IRecruiterRepository _recruiterRepository;
        public CloseJobCommandHandler(
            IGenericRepository<Job> jobRepository,
            IRecruiterRepository recruiterRepository)
        {
            _jobRepository = jobRepository;
            _recruiterRepository = recruiterRepository;
        }
       async Task IRequestHandler<CloseJobCommand>.Handle(CloseJobCommand request, CancellationToken cancellationToken)
        {

            var job = await _jobRepository.GetByIdAsync(request.JobId);

            if (job == null)
            {
                throw new KeyNotFoundException(
                    $"Job with id {request.JobId} not found.");
            }

            var recruiter =
                await _recruiterRepository.GetByUserIdAsync(request.userId);

            if (recruiter == null)
            {
                throw new KeyNotFoundException(
                    "Recruiter profile not found.");
            }

            if (job.RecruiterId != recruiter.Id)
            {
                throw new UnauthorizedAccessException(
                    "You are not the owner of this job.");
            }

            if (!job.IsActive)
            {
                throw new InvalidOperationException(
                    "Job is already closed.");
            }

            job.IsActive = false;
            job.ClosedAt = DateTime.UtcNow;

            _jobRepository.Update(job);
            await _jobRepository.SaveChangesAsync();
        }
    }
}
