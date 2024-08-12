using Microsoft.EntityFrameworkCore;
using TutorAppAPI.Models;

namespace TutorAppAPI.Services
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbContextOptions<MySqlContext> options)
       : base(options)
        {
        }
        public DbSet <AccountInfo> AccountInfo {  get; set; }
        public DbSet<Admins> Admins { get; set; }
        public DbSet<AgencyManagers> AgencyManagers { get; set; }
        public DbSet<TutorLocations> TutorLocations { get; set; }
        public DbSet<TutorGrade> TutorGrade { get; set; }
        public DbSet<EducationLevel> EducationLevel { get; set; }
        public DbSet<EducationAndQualifications> EducationAndQualifications { get; set; }
        public DbSet<TutorLevel> TutorLevel { get; set; }
        public DbSet<TutorCategory> TutorCategory { get; set; }
        public DbSet<ParentDetails> ParentDetails { get; set; }
        public DbSet<ProfilesAndExperinces> ProfilesAndExperinces { get; set; }
        public DbSet<Tutors> Tutors { get; set; }
        public DbSet<Assignment> Assignment { get; set; }
        public DbSet<TutorSubject> TutorSubject { get; set; }
        public DbSet<TutorSchools> TutorSchools { get; set; }
        public DbSet<TutoringPreferences> TutoringPreferences { get; set; }
        public DbSet<TutorGradesSubject> TutorGradesSubject { get; set; }
        public DbSet<TutorGradeValues> TutorGradeValues { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<AssignmentApplied> AssignmentApplied { get; set; }

    }
}
