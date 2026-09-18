using JobApplication.Application.DTOs.Job;
using JobApplication.Application.Interfaces;
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
                Title = job.Title,
                Description = job.Description,
                IsActive = job.IsActive,
            };
        }

        public async Task<IEnumerable<JobResponseDTO>> GetAll()
        {
          var jobs= await _jobRepository.GetAllAsync();
            if (!jobs.Any())
            {
                throw new Exception($"No jobs found.");
            }
            return jobs.Select(job => new JobResponseDTO
            {
                Title = job.Title,
                Description = job.Description,
                IsActive=job.IsActive
            });
        }

        public async Task<JobResponseDTO> GetById(int id)
        {
           var job= await _jobRepository.GetByIdAsync(id);
            if (job == null)
            {
                throw new Exception($"Job with id {id} not found.");
            }
            return new JobResponseDTO()
            {
                Description = job.Description,
                Title = job.Title,
                IsActive = job.IsActive
            };
        }


        public async Task<JobResponseDTO> Update(int id, JobRequestDTO jobDto)
        {
            var job = await _jobRepository.GetByIdAsync(id);

            if (job == null)
            {
                throw new Exception($"Job with id {id} not found.");
            }

            job.Title = jobDto.Title;
            job.Description = jobDto.Description;

            _jobRepository.Update(job);
            await _jobRepository.SaveChangesAsync();

            return new JobResponseDTO
            {
                Title = job.Title,
                Description = job.Description,
                IsActive = job.IsActive
            };
        }

        public async Task Delete(int id)
        {
            var job = await _jobRepository.GetByIdAsync(id);

            if (job == null)
            {
                throw new Exception($"Job with id {id} not found.");
            }

            _jobRepository.Delete(job);
            await _jobRepository.SaveChangesAsync();
        }

        public Task CloseAsync(int id, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
