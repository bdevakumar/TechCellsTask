using Microsoft.EntityFrameworkCore;
using TechCellsTask.Core.Entities;

namespace TechCellsTask.Infrastructure.Data
{
    /// <summary>
    /// A ApplicationDbContext represents a session with the database and 
    /// can be used to query and save instances of your entities.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Initializes options for ApplicationDbContext
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Users table
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// FileInfos table
        /// </summary>
        public DbSet<FileInfo> FileInfos { get; set; }
    }
}
