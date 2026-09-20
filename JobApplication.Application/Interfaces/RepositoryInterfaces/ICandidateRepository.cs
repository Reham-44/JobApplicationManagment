using JobApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Interfaces.RepositoryInterfaces
{
    public interface ICandidateRepository
    {
        Task<Candidate?> GetByUserIdAsync(string userId);

    }
}
