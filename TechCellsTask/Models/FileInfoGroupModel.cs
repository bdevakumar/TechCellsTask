using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechCellsTaskApi.Models
{
    public class FileInfoGroupModel
    {
        public FileInfoGroupModel()
        {
            Files = new List<FileInfoModel>();
        }

        public string Extension { get; set; }
        public IEnumerable<FileInfoModel> Files { get; set; }
    }
}
