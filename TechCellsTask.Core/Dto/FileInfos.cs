using System;

namespace TechCellsTask.Core.Dto
{
    public class FileInfos
    {
        public string FileName { get; set; }

        public string FileExtension { get; set; }

        public long FileSize { get; set; }

        public string DownloadLink { get; set; }

        public DateTime UploadDate { get; set; }

        public string UserName { get; set; }
    }
}
