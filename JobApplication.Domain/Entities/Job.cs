namespace JobApplication.Domain.Entities
{
    public class Job
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public bool IsActive { get; set; }

        public int RecruiterId { get; set; }

        public Recruiter Recruiter { get; set; } = null!;

        public DateTime? ClosedAt { get; set; }
    }
}