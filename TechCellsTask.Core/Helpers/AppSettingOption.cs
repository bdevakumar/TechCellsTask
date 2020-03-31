using System;

namespace TechCellsTask.Core.Helpers
{
    public class AppSettingOption
    {
        public string UploadPath { get; set; }
        public string[] AllowedExtensions { get; set; }
        public long AllowedFileSize { get; set; }
    }
}
