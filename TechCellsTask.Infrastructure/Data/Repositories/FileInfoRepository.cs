using TechCellsTask.Core.Entities;

namespace TechCellsTask.Infrastructure.Data.Repositories
{
    /// <summary>
    /// An FileInfo Repository, that used to perform CRUD operations on table FileInfo
    /// </summary>
    public class FileInfoRepository : EntityRepository<FileInfo>
    {
        /// <summary>
        /// Initializes an instance of DbContext
        /// </summary>
        /// <param name="context">An instance of DbContext</param>
        public FileInfoRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
