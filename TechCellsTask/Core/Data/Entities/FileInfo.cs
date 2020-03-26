using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechCellsTaskApi.Core.Data.Entities
{
    /// <summary>
    /// Entity Model of FileInfos Table
    /// </summary>
    [Table("FileInfos")]
    public class FileInfo : EntityBase
    {
        /// <summary>
        /// The name of the file
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The new name of the file after saved to uploads
        /// </summary>
        public string NewFileName { get; set; }

        /// <summary>
        /// The extension of the file
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// File size in bytes
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// The Upload date of the file
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime UploadDate { get; set; }


        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
