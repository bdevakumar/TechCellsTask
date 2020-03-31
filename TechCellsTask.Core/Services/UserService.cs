using System;
using System.Linq;
using TechCellsTask.Core.Entities;
using TechCellsTask.Core.Interfaces;

namespace TechCellsTask.Core.Services
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
        public UserService(IEntityRepository<User> repository)
           : base(repository)
        {
        }

        public User GetByUserName(string username)
        {
            return GetAllAsQueryable().FirstOrDefault(f => f.UserName.Equals(username));
        }
    }
}
