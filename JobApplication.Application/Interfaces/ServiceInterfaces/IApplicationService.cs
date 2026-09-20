using JobApplication.Application.DTOs.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Interfaces.ServiceInterfaces
{
    public interface IApplicationService
    {
        Task<int> ApplyAsync(int jobId, string userId);
        Task UpdateStatusAsync(int applicationId,ApplicationStatusDTO dto,string userId);
        Task CancelApplicationAsync(int applicationId, string userId);
        Task<IEnumerable<ApplicationResponseDTO>> GetMyApplicationsAsync(string userId);
        Task<IEnumerable<ApplicationRecruiterResponseDTO>> GetMyJobApplicationsAsync(string userId);
    }
}
