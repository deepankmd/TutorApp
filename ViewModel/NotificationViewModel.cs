using TutorAppAPI.Models;

namespace TutorAppAPI.ViewModel
{
    public class NotificationViewModel
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public string TypeID { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public NotificationType NotificationType { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedDate { get; set; }
        public ScreenType ScreenType { get; set; }
    }
}
