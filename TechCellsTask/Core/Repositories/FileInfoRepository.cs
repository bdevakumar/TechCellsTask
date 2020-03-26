using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechCellsTaskApi.Core.Data;
using TechCellsTaskApi.Core.Data.Entities;

namespace TechCellsTaskApi.Core.Repositories
{
    /// <summary>
    /// An FileInfo Repository, that used to perform CRUD operations on table FileInfo
    /// </summary>
    public class FileInfoRepository : EntityRepository<FileInfo>
    {
        private readonly ApplicationDbContext dbcontext;

        /// <summary>
        /// Initializes an instance of DbContext
        /// </summary>
        /// <param name="context">An instance of DbContext</param>
        public FileInfoRepository(ApplicationDbContext context)
            : base(context)
        {
            this.dbcontext = context;
        }
    }
}
