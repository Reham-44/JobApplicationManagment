using JobApplication.Application.DTOs.Job;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Features.Jobs.Commands.CloseJob
{
    public class CloseJobCommand:IRequest
    {
        public int JobId { get; set; }
        public string userId { get; set; }
    }
}
