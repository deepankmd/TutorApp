using System.ComponentModel.DataAnnotations;

namespace TutorAppAPI.Models
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string MobileNumber { get; set; }
    }
}
