using JobApplication.Application.Interfaces.RepositoryInterfaces;
using JobApplication.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Features.Jobs.Commands.AutoCloseExpiredJobs
{
    public class AutoCloseExpiredJobsCommandHandler : IRequestHandler<AutoCloseExpiredJobsCommand>
    {
        private readonly IGenericRepository<Job> _jobRepository;

        public AutoCloseExpiredJobsCommandHandler(IGenericRepository<Job> jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task Handle(AutoCloseExpiredJobsCommand request, CancellationToken cancellationToken)
        {
            var jobs = await _jobRepository.GetAllAsync();

            var expirationDate = DateTime.UtcNow.AddDays(-30);

            var expiredJobs = jobs
                .Where(j =>
                    j.IsActive &&
                    j.CreatedAt <= expirationDate)
                .ToList();

            foreach (var job in expiredJobs)
            {
                job.IsActive = false;
                job.ClosedAt = DateTime.UtcNow;

                _jobRepository.Update(job);
            }

            await _jobRepository.SaveChangesAsync();
        }
    
    }
}
