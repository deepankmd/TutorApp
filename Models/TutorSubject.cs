namespace TutorAppAPI.Models
{
    public class TutorSubject
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public Guid TutorLevelID { get; set; }
        public TutorLevel TutorLevel { get; set; }
        
    }

}
