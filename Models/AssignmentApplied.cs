namespace TutorAppAPI.Models
{
    public class AssignmentApplied
    {
        public int Id { get; set; } 
        public string YourRate { get; set; }    
        public string AllAvailableTimings { get; set; }
        public string WhenCanYouStartEarliest { get; set; }
        public string WhyShouldYouBeChosen { get; set; }
    }
}
