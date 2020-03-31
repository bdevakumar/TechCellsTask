using TechCellsTask.Core.Entities;

namespace TechCellsTask.Infrastructure.Data.Repositories
{
    /// <summary>
    /// An UserRepository, that used to perform CRUD operations on table User
    /// </summary>
    public class UserRepository : EntityRepository<User>
    {
        /// <summary>
        /// Initializes an instance of DbContext
        /// </summary>
        /// <param name="context">An instance of DbContext</param>
        public UserRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
