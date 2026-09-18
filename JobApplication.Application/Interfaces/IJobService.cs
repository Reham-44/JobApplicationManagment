using JobApplication.Application.DTOs.Job;

namespace JobApplication.Application.Interfaces
{
    public interface IJobService
    {
        public Task<JobResponseDTO> CreateAsync(JobRequestDTO jobRequestDTO,string userId);
        public Task<JobResponseDTO> Update(int id, JobRequestDTO jobDto);
        public Task Delete(int id);
        public Task<IEnumerable<JobResponseDTO>> GetAll();
        public Task<JobResponseDTO> GetById(int id);
    }
}
