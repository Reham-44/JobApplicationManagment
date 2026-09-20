using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Interfaces.ServiceInterfaces
{
    public interface IApplicationService
    {
        Task<int> ApplyAsync(int jobId, string userId);
        Task CancelApplicationAsync(int applicationId, string userId);

    }
}
