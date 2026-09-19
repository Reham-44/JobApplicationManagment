using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.DTOs.Job
{
    public class JobResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ClosedAt { get; set; }

    }
}
