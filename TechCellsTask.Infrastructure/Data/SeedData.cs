using System.Linq;
using TechCellsTask.Core.Entities;

namespace TechCellsTask.Infrastructure.Data
{
    /// <summary>
    /// Initializes primary data when database created
    /// </summary>
    public static class SeedData
    {
        /// <summary>
        /// Method for initialization of primary data
        /// </summary>
        public static void Initialize(ApplicationDbContext context)
        {
            // Look for any file infos.
            if (!context.FileInfos.Any())
            {
                // TODO: Seed file info data
            }

            // Look for any users.
            if (!context.Users.Any())
            {
                context.Users.Add(new User
                {
                    UserName = "admin",
                    Email = "admin@test.com",
                    PhoneNumber = "123456789"
                });
            }

            context.SaveChanges();
        }
    }
}
