using JobApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Interfaces.RepositoryInterfaces
{
    public interface IApplicationRepository
        : IGenericRepository<JobCandidateApplication>
    {
        Task<JobCandidateApplication?> GetByCandidateAndJobAsync(
            int candidateId,
            int jobId);
        Task<JobCandidateApplication?> GetByIdWithJobAsync(int id);
        Task<IEnumerable<JobCandidateApplication>> GetByCandidateIdAsync(
    int candidateId);
        Task<IEnumerable<JobCandidateApplication>> GetByRecruiterIdAsync(int recruiterId);
    }
}
