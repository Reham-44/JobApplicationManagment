using JobApplication.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.DTOs.Application
{
        public class ApplicationRecruiterResponseDTO
        {
            public int Id { get; set; }

            public int JobId { get; set; }

            public string JobTitle { get; set; } = null!;

            public int CandidateId { get; set; }

            public string CandidateName { get; set; } = null!;

            public JobApplicationStatus Status { get; set; }

            public DateTime AppliedAt { get; set; }

            public DateTime StatusUpdatedAt { get; set; }

            public DateTime? CancelledAt { get; set; }
        }
    }
