using JobApplication.Application.DTOs.Job;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Features.Jobs.Commands.CreateJobByRecruiter
{
    public class CreateJobByRecruiterCommand:IRequest<JobResponseDTO>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }

    }
}
