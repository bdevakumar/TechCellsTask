using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechCellsTaskApi.Models
{
    public class FileInfoModel
    {
        public string FileName { get; set; }

        public string NewFileName { get; set; }

        public string FileExtension { get; set; }

        public long FileSize { get; set; }

        public DateTime UploadDate { get; set; }

        public string UserName { get; set; }
    }
}
