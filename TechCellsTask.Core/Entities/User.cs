using System.ComponentModel.DataAnnotations.Schema;

namespace TechCellsTask.Core.Entities
{
    /// <summary>
    /// Entity Model of Users Table
    /// </summary>
    [Table("Users")]
    public class User : EntityBase
    {
        /// <summary>
        /// Short name of the user
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Email of the user
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Phone Number of the user
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}
