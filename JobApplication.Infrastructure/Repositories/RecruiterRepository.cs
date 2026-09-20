using JobApplication.Application.Interfaces;
using JobApplication.Domain.Entities;
using JobApplication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore; 
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Infrastructure.Repositories
{

    public class RecruiterRepository : IRecruiterRepository
    {
        private readonly ApplicationDbContext _context;
        public RecruiterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Recruiter?> GetByUserIdAsync(string userId)
        {
            return await _context.Recruiters.FirstOrDefaultAsync(r => r.UserId == userId);
        }
    }
}
