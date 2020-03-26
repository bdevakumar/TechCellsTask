using Microsoft.EntityFrameworkCore;
using TechCellsTaskApi.Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechCellsTaskApi.Core.Data
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
