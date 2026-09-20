using JobApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Interfaces.RepositoryInterfaces
{
    public interface IRecruiterRepository
    {
        Task<Recruiter?> GetByUserIdAsync(string userId);

    }
}
