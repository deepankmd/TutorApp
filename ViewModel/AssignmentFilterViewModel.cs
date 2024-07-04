using TutorAppAPI.Models;

namespace TutorAppAPI.ViewModel
{
    public class AssignmentFilterViewModel
    {

        public string TutorType { get; set; }
        public string Gender { get; set; }
        public List<string> Locations { get; set; } = new List<string>();
        public List<string> Subjects { get; set; } = new List<string>();
        public List<string> AvailableZones { get; set; }
        public List<TutorSubject> AvailableSubjects { get; set; }
    }



}
