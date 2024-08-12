using Microsoft.EntityFrameworkCore;
using TutorAppAPI.Models;
using TutorAppAPI.Services;

namespace TutorAppAPI
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MySqlContext(
                serviceProvider.GetRequiredService<DbContextOptions<MySqlContext>>()))
            {
                // Look for any TutorLocations
                if (context.TutorLocations.Any())
                {
                    return;   // Data was already seeded
                }

                context.TutorLocations.AddRange(
                    new TutorLocations
                    {
                        ID = Guid.NewGuid(),
                        Zone = "North",
                        ShortDescription = "North Zone",
                        Location = "City A"
                    },
                    new TutorLocations
                    {
                        ID = Guid.NewGuid(),
                        Zone = "South",
                        ShortDescription = "South Zone",
                        Location = "City B"
                    },
                    new TutorLocations
                    {
                        ID = Guid.NewGuid(),
                        Zone = "East",
                        ShortDescription = "East Zone",
                        Location = "City C"
                    },
                    new TutorLocations
                    {
                        ID = Guid.NewGuid(),
                        Zone = "West",
                        ShortDescription = "West Zone",
                        Location = "City D"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
