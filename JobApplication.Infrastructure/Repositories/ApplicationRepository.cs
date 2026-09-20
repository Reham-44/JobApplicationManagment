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
        public async Task<JobCandidateApplication?> GetByIdWithJobAsync(int id)
        {
            return await _context.JobCandidateApplications
                .Include(a => a.Job)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<IEnumerable<JobCandidateApplication>> GetByCandidateIdAsync(int candidateId)
        {
            return await _context.JobCandidateApplications
                .Include(a => a.Job)
                .Where(a => a.CandidateId == candidateId)
                .OrderByDescending(a => a.AppliedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<JobCandidateApplication>> GetByRecruiterIdAsync(int recruiterId)
        {
            return await _context.JobCandidateApplications
                .Include(a => a.Job)
                .Include(a => a.Candidate)
                .Where(a => a.Job.RecruiterId == recruiterId)
                .OrderByDescending(a => a.AppliedAt)
                .ToListAsync();
        }
    }
}
