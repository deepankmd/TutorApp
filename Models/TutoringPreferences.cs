namespace TutorAppAPI.Models
{
    public class TutoringPreferences 
    {
        public Guid ID { get; set; } 
        public List<TutorSubject> TutorSubject { get; set; }
        public List<TutorLevel> TutorLevels { get; set; }
        public List<TutorLocations> Locations { get; set; }
        public List<string> SpecialNeedsExperience { get; set; }
        public string SpecialNeedsExperienceDescription { get; set; }
        public List<string> PreferredLocations { get; set; }
        public List<string> SelectedSubjects { get; set; }
    }
}
