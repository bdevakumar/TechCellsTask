using System;
using System.Collections.Generic;

namespace TechCellsTask.Core.Dto
{
    public class FileInfoGroup
    {
        public FileInfoGroup()
        {
            Files = new List<FileInfos>();
        }

        public string Extension { get; set; }
        public IEnumerable<FileInfos> Files { get; set; }
    }
}
