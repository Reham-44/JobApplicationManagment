using JobApplication.Application.Interfaces.RepositoryInterfaces;
using JobApplication.Domain.Entities;
using JobApplication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Infrastructure.Repositories
{
    public class CandidateRepository: ICandidateRepository
    {

        private readonly ApplicationDbContext _context;

        public CandidateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Candidate?> GetByUserIdAsync(string userId)
        {
            return await _context.Candidates
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }
    }
}
