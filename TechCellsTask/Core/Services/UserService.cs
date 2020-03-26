using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TechCellsTaskApi.Core.Data.Entities;
using TechCellsTaskApi.Core.Repositories;

namespace TechCellsTaskApi.Core.Services
{
    /// <summary>
    /// Performs CRUD and other operations on the User table
    /// </summary>
    public class UserService : EntityService<User>
    {
        /// <summary>
        /// Initializes a new instance of UserRepository class, 
        /// that performs database operations on the table User
        /// </summary>
        /// <param name="repository">A new instance of UserRepository class</param>
        public UserService(UserRepository repository)
           : base(repository)
        {
        }
    }
}
