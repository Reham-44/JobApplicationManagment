using JobApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Interfaces
{
    public interface IRecruiterRepository
    {
        Task<Recruiter?> GetByUserIdAsync(string userId);

    }
}
