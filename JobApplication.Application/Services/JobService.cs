using JobApplication.Application.DTOs.Job;
using JobApplication.Application.Interfaces.RepositoryInterfaces;
using JobApplication.Application.Interfaces.ServiceInterfaces;
using JobApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Services
{
    public class JobService : IJobService
    {
        private readonly IGenericRepository<Job> _jobRepository;
        private readonly IRecruiterRepository _recruiterRepository;

        public JobService(
            IGenericRepository<Job> jobRepository,
            IRecruiterRepository recruiterRepository)
        {
            _jobRepository = jobRepository;
            _recruiterRepository = recruiterRepository;
        }


        public async Task<JobResponseDTO> CreateAsync(JobRequestDTO dto,string userId)
        {
            var recruiter = await _recruiterRepository.GetByUserIdAsync(userId);

            if (recruiter == null)
            {
                throw new KeyNotFoundException(
                    "Recruiter profile not found.");
            }

            var job = new Job
            {
                Title = dto.Title,
                Description = dto.Description,
                IsActive = true,
                RecruiterId = recruiter.Id
            };

            await _jobRepository.AddAsync(job);
            await _jobRepository.SaveChangesAsync();

            return new JobResponseDTO
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                IsActive = job.IsActive,
                ClosedAt = job.ClosedAt
            };
        }

        public async Task<IEnumerable<JobResponseDTO>> GetAll()
        {
          var jobs= await _jobRepository.GetAllAsync();
    
            return jobs.Select(job => new JobResponseDTO
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                IsActive=job.IsActive,
                ClosedAt = job.ClosedAt
            });
        }

        public async Task<JobResponseDTO> GetById(int id)
        {
           var job= await _jobRepository.GetByIdAsync(id);
            if (job == null)
            {
                throw new KeyNotFoundException("Job not found.");
            }
            return new JobResponseDTO()
            {
                Id = job.Id,
                Description = job.Description,
                Title = job.Title,
                IsActive = job.IsActive,
                ClosedAt = job.ClosedAt
            };
        }


        public async Task<JobResponseDTO> Update(int id, JobRequestDTO jobDto, string userId)
        {
            var job = await _jobRepository.GetByIdAsync(id);

            if (job == null)
            {
                throw new KeyNotFoundException(
                    $"Job with id {id} not found.");
            }

            var recruiter =
                await _recruiterRepository.GetByUserIdAsync(userId);

            if (recruiter == null)
            {
                throw new KeyNotFoundException(
                    "Recruiter profile not found.");
            }

            if (job.RecruiterId != recruiter.Id)
            {
                throw new UnauthorizedAccessException(
                    "You are not the owner of this job.");
            }

            if (!job.IsActive)
            {
                throw new InvalidOperationException(
                    "Closed jobs cannot be updated.");
            }

            job.Title = jobDto.Title;
            job.Description = jobDto.Description;

            _jobRepository.Update(job);
            await _jobRepository.SaveChangesAsync();

            return new JobResponseDTO
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                IsActive = job.IsActive,
                ClosedAt = job.ClosedAt
            };
        }

        public async Task Delete(int id)
        {
            var job = await _jobRepository.GetByIdAsync(id);

            if (job == null)
            {
                throw new KeyNotFoundException("Job not found.");
            }

            _jobRepository.Delete(job);
            await _jobRepository.SaveChangesAsync();
        }
        public async Task CloseAsync(int id, string userId)
        {
            var job = await _jobRepository.GetByIdAsync(id);

            if (job == null)
            {
                throw new KeyNotFoundException(
                    $"Job with id {id} not found.");
            }

            var recruiter =
                await _recruiterRepository.GetByUserIdAsync(userId);

            if (recruiter == null)
            {
                throw new KeyNotFoundException(
                    "Recruiter profile not found.");
            }

            if (job.RecruiterId != recruiter.Id)
            {
                throw new UnauthorizedAccessException(
                    "You are not the owner of this job.");
            }

            if (!job.IsActive)
            {
                throw new InvalidOperationException(
                    "Job is already closed.");
            }

            job.IsActive = false;
            job.ClosedAt = DateTime.UtcNow;

            _jobRepository.Update(job);
            await _jobRepository.SaveChangesAsync();
        }

    }
}
