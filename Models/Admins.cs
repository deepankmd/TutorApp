﻿namespace TutorAppAPI.Models
{
    public class Admins
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        public string Status { get; set; }
        public string Password {  get; set; }
    }
}
