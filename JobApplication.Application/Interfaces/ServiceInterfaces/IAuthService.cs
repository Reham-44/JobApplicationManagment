using JobApplication.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Interfaces.ServiceInterfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> RegisterCandidateAsync(
            CandidateRegisterDTO dto);

        Task<AuthResponseDTO> RegisterRecruiterAsync(
            RecruiterRegisterDTO dto);

        Task<AuthResponseDTO> LoginAsync(
            LoginDTO dto);
    }
}
