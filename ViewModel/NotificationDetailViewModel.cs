using TutorAppAPI.Models;

namespace TutorAppAPI.ViewModel
{
    public class NotificationDetailViewModel
    {
        public string ID { get; set; }
        public string UserName { get; set; }
        public string TypeID { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public NotificationType NotificationType { get; set; }
        public ScreenType ScreenType { get; set; }
        public Tutors Tutorṣ { get; set; }
        public Assignment Assignment { get; set; }
    }

}
