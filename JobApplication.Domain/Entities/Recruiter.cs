using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Domain.Entities
{
 public class Recruiter
        {
            public int Id { get; set; }

            public string UserId { get; set; } = null!;

            public string Name { get; set; } = null!;

            public string? CompanyName { get; set; }
        }
    }
