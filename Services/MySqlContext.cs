using Microsoft.EntityFrameworkCore;
using TutorAppAPI.Models;

namespace TutorAppAPI.Services
{
    public class MySqlContext : DbContext
    {
        public DbSet<TutorLocations> TutorLocations { get; set; }
        public DbSet<TutorGrade> TutorGrade { get; set; }
        public DbSet<EducationLevel> EducationLevel { get; set; }
        public DbSet<TutorLevel> TutorLevel { get; set; }
        public DbSet<TutorCategory> TutorCategory { get; set; }
        public DbSet<ParentDetails> ParentDetails { get; set; }
        public DbSet<Tutors> Tutors { get; set; }
        public DbSet<Admins> Admins { get; set; }
        public DbSet<Assignment> Assignment { get; set; }
        public DbSet<TutorSubject> TutorSubject { get; set; }
        public DbSet<TutorSchools> TutorSchools { get; set; }
        public DbSet<TutorGradesSubject> TutorGradesSubject { get; set; }
        public DbSet<TutorGradeValues> TutorGradeValues { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<AssignmentApplied> AssignmentApplied { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure your entity mappings here
        }
    }
}
