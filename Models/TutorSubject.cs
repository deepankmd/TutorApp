namespace TutorAppAPI.Models
{
    public class TutorSubject
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public TutorLevel TutorLevel { get; set; }
        public string TutorLevelID { get; set; }
    }

}
