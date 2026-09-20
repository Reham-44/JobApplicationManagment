using JobApplication.Application.DTOs.Job;

namespace JobApplication.Application.Interfaces.ServiceInterfaces
{
    public interface IJobService
    {
        public Task<JobResponseDTO> CreateAsync(JobRequestDTO jobRequestDTO,string userId);
        Task<JobResponseDTO> Update(int id,JobRequestDTO jobDto,string userId);
        public Task Delete(int id);
        public Task<IEnumerable<JobResponseDTO>> GetAll();
        public Task<JobResponseDTO> GetById(int id);
        public Task CloseAsync(int id, string userId);

    }
}
