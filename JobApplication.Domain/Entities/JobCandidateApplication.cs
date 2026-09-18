using JobApplication.Domain.Enums;

namespace JobApplication.Domain.Entities
{
    public class JobCandidateApplication
    {
        public int Id { get; set; }

        public int CandidateId { get; set; }

        public Candidate Candidate { get; set; } = null!;

        public int JobId { get; set; }

        public Job Job { get; set; } = null!;

        public JobApplicationStatus JobApplicationStatus { get; set; }

        public DateTime AppliedAt { get; set; }

        public DateTime StatusUpdatedAt { get; set; }

        public DateTime? CancelledAt { get; set; }
    }
}