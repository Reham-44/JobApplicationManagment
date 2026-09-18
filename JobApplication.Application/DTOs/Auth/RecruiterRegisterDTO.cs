using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.DTOs.Auth
{
    public class RecruiterRegisterDTO
    {
            public string Name { get; set; } = null!;
            public string Email { get; set; } = null!;
            public string Password { get; set; } = null!;
            public string? CompanyName { get; set; }
        }
}
