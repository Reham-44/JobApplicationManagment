using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Interfaces.ServiceInterfaces
{
    public interface IJWTService
    {
        string GenerateToken(
      string userId,
      string email,
      IEnumerable<string> roles);
    }
}
