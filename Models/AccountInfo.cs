namespace TutorAppAPI.Models
{
    public class AccountInfo
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string NRIC { get; set; }
        public string Citizenship { get; set; }
        public DateTime DateofBirth { get; set; } = DateTime.UtcNow;
        public string Gender { get; set; }
        public string Race { get; set; }
    }
}
