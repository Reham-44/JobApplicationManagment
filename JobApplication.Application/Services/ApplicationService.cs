using JobApplication.Application.Interfaces.RepositoryInterfaces;
using JobApplication.Application.Interfaces.ServiceInterfaces;
using JobApplication.Domain.Entities;
using JobApplication.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IGenericRepository<Job> _jobRepository;

        public ApplicationService(
            IApplicationRepository applicationRepository,
            ICandidateRepository candidateRepository,
            IGenericRepository<Job> jobRepository)
        {
            _applicationRepository = applicationRepository;
            _candidateRepository = candidateRepository;
            _jobRepository = jobRepository;
        }

        public async Task<int> ApplyAsync(
            int jobId,
            string userId)
        {
            var candidate =
                await _candidateRepository.GetByUserIdAsync(userId);

            if (candidate == null)
            {
                throw new KeyNotFoundException(
                    "Candidate profile not found.");
            }

            var job = await _jobRepository.GetByIdAsync(jobId);

            if (job == null)
            {
                throw new KeyNotFoundException(
                    $"Job with id {jobId} not found.");
            }

            if (!job.IsActive)
            {
                throw new InvalidOperationException(
                    "You cannot apply to a closed job.");
            }

            var existingApplication =
                await _applicationRepository
                    .GetByCandidateAndJobAsync(
                        candidate.Id,
                        jobId);

            if (existingApplication != null)
            {
                throw new InvalidOperationException(
                    "You have already applied to this job.");
            }

            var application = new JobCandidateApplication
            {
                CandidateId = candidate.Id,
                JobId = jobId,
                JobApplicationStatus = JobApplicationStatus.Applied,
                AppliedAt = DateTime.UtcNow,
                StatusUpdatedAt = DateTime.UtcNow
            };

            await _applicationRepository.AddAsync(application);
            await _applicationRepository.SaveChangesAsync();

            return application.Id;
        }

        public async Task CancelApplicationAsync(int applicationId,string userId)
        {
            var candidate = await _candidateRepository
                .GetByUserIdAsync(userId);

            if (candidate is null)
            {
                throw new UnauthorizedAccessException(
                    "Candidate profile not found.");
            }

            var application = await _applicationRepository
                .GetByIdAsync(applicationId);

            if (application is null)
            {
                throw new KeyNotFoundException(
                    "Application not found.");
            }

            if (application.CandidateId != candidate.Id)
            {
                throw new UnauthorizedAccessException(
                    "You can only cancel your own applications.");
            }

            if (application.JobApplicationStatus != JobApplicationStatus.Applied &&
                application.JobApplicationStatus != JobApplicationStatus.UnderReview)
            {
                throw new InvalidOperationException(
                    "Application cannot be cancelled in its current status.");
            }

            application.JobApplicationStatus = JobApplicationStatus.Cancelled;
            application.CancelledAt = DateTime.UtcNow;
            application.StatusUpdatedAt = DateTime.UtcNow;

            _applicationRepository.Update(application);

            await _applicationRepository.SaveChangesAsync();
        }
    }
}
