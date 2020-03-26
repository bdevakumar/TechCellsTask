using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TechCellsTaskApi.Core.Data.Entities;
using TechCellsTaskApi.Core.Helpers;
using TechCellsTaskApi.Core.Repositories;

namespace TechCellsTaskApi.Core.Services
{
    /// <summary>
    /// Performs CRUD and other operations on the FileInfo table
    /// </summary>
    public class FileInfoService : EntityService<FileInfo>
    {
        private readonly AppSettingOption _options;

        /// <summary>
        /// Initializes new instances of dependencies that are used in this service
        /// </summary>
        /// <param name="repository">An instances of FileInfoRepository</param>
        /// <param name="options">An instances of IOptions<AppSettingOption></param>
        public FileInfoService(
            FileInfoRepository repository,
            IOptions<AppSettingOption> options
        )
           : base(repository)
        {
            _options = options.Value;
        }

        /// <summary>
        /// Gets AppSettingOption from appsetting.json file
        /// </summary>
        /// <returns>Returns an instance of AppSettingOption</returns>
        public AppSettingOption GetAppSettingOption()
        {
            return _options;
        }

        /// <summary>
        /// Copy file to uploads folder
        /// </summary>
        /// <param name="filePath">file that is being copy</param>
        /// <returns>Returns new file name</returns>
        public string CopyToUploads(IFormFile file)
        {
            var ext = System.IO.Path.GetExtension(file.FileName).ToLowerInvariant();
            var path = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot", _options.UploadPath);
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            var fileName = System.IO.Path.GetRandomFileName();
            var filePath = System.IO.Path.Combine(path, fileName + ext);
            using (var stream = System.IO.File.Create(filePath))
            {
                file.CopyTo(stream);
            }

            return fileName + ext;
        }

        /// <summary>
        /// Checks if the file extension contains in AppSettingOpion.AllowedExtensions
        /// </summary>
        /// <param name="ext">Extension of the file</param>
        /// <returns>true if the extension contains in appsetting, else false</returns>
        public bool FileExtensionIsValid(string ext)
        {
            return !string.IsNullOrEmpty(ext) &&
                   _options.AllowedExtensions.Any(a => a.ToLowerInvariant().Equals(ext));
        }

        /// <summary>
        /// Checks if the file size matches with AppSettingOpion.AllowedFileSize
        /// </summary>
        /// <param name="length">Length of the file</param>
        /// <returns>true if the size less then AppSettingOpion.AllowedFileSize, else false</returns>
        public bool FileSizeIsValid(long length)
        {
            return length <= _options.AllowedFileSize;
        }
    }
}
