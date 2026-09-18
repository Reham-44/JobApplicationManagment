using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.DTOs.Auth
{
    public class AuthResponseDTO
    {

        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string Message { get; set; } = null!;
        public bool Success { get; set; }
    }
}
