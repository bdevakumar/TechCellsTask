using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechCellsTaskApi.Core.Helpers
{
    public class AppSettingOption
    {
        public string UploadPath { get; set; }
        public string[] AllowedExtensions { get; set; }
        public long AllowedFileSize { get; set; }
    }
}
