namespace TutorAppAPI.Models
{
    public class Assignment
    {
        public Guid ID { get; set; }
        public string SubjectsToBeTutored { get; set; }
        public string StudentLevel { get; set; }
        public string FrequencyOfLessons { get; set; }
        public string YourTuitionBudget { get; set; }
        public DateTime TuitionStartDate { get; set; } = DateTime.UtcNow;
        public string LengthOfCommitment { get; set; }
        public string AvailableTimings { get; set; }
        public string PreferredTutorGender { get; set; }
        public string DescriptionOfNeeds { get; set; }
        public string ParentId { get; set; }
        public string TutorId { get; set; }
        public string TutorAvailability { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime DueDate { get; set; } = DateTime.UtcNow;
        public bool IsVerified { get; set; } = false;
        public AssignmentStatus AssignmentStatus { get; set; }
        public string AppliedCount { get; set; }
    }
    public enum WorkingType
    {
        Online,
        InPerson
    }

    public enum AssignmentStatus
    {
        Pending,
        Applied,
        Confirmed,
        Closed
    }
}
