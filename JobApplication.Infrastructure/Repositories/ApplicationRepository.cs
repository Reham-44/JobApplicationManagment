using JobApplication.Application.Interfaces.RepositoryInterfaces;
using JobApplication.Domain.Entities;
using JobApplication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Infrastructure.Repositories
{
    public class ApplicationRepository
          : GenericRepository<JobCandidateApplication>,
            IApplicationRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<JobCandidateApplication?> GetByCandidateAndJobAsync(
            int candidateId,
            int jobId)
        {
            return await _context.JobCandidateApplications
                .FirstOrDefaultAsync(a =>
                    a.CandidateId == candidateId &&
                    a.JobId == jobId);
        }
    }
}
