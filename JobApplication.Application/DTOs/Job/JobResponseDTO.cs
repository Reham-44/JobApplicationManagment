using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.DTOs.Job
{
    public class JobResponseDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
