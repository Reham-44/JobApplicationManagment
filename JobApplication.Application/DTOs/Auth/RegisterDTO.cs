using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.DTOs.Auth
{
    public class RegisterDTO
    {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
    }
}
