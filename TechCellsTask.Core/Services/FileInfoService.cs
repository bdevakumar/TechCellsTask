using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TechCellsTask.Core.Dto;
using TechCellsTask.Core.Entities;
using TechCellsTask.Core.Helpers;
using TechCellsTask.Core.Interfaces;

namespace TechCellsTask.Core.Services
{
    /// <summary>
    /// Performs CRUD and other operations on the FileInfo table
    /// </summary>
    public class FileInfoService : EntityService<FileInfo>
    {
        private readonly AppSettingOption _options;
        private readonly IEntityRepository<FileInfo> _repository;

        /// <summary>
        /// Initializes new instances of dependencies that are used in this service
        /// </summary>
        /// <param name="repository">An instances of FileInfoRepository</param>
        /// <param name="options">An instances of IOptions<AppSettingOption></param>
        public FileInfoService(
            IEntityRepository<FileInfo> repository,
            IOptions<AppSettingOption> options
        )
           : base(repository)
        {
            _options = options.Value;
            _repository = repository;
        }

        /// <summary>
        /// Gets FileInfos grouping by Extension field
        /// </summary>
        /// <returns>Returns grouped list of FileInfos</returns>
        public IEnumerable<FileInfoGroup> GetGroupByExtension()
        {
            return GetAllAsQueryable()
                .Include(i => i.User)
                .AsEnumerable()
                .GroupBy(g => g.FileExtension)
                .Select(s => new FileInfoGroup
                {
                    Extension = s.Key,
                    Files = s.Select(ss => new FileInfos
                    {
                        FileName = ss.FileName,
                        FileExtension = ss.FileExtension,
                        FileSize = ss.FileSize,
                        DownloadLink = System.IO.Path.Combine(_options.UploadPath, ss.NewFileName),
                        UploadDate = ss.UploadDate,
                        UserName = ss.User.UserName
                    })
                });
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
        /// Creates FileInfo from the file
        /// </summary>
        /// <param name="file">The file that being saved</param>
        /// <param name="newFileName">The New File Name for the file</param>
        /// <param name="userId">Id of the user</param>
        /// <returns></returns>
        public bool CreateFileInfo(IFormFile file, string newFileName, int userId)
        {
            string ext = System.IO.Path.GetExtension(file.FileName).ToLowerInvariant();
            FileInfo fileInfo = new FileInfo
            {
                FileName = file.FileName,
                FileSize = file.Length,
                FileExtension = ext.Replace(".", ""),
                NewFileName = newFileName,
                UploadDate = DateTime.Now,
                UserId = userId
            };
            _repository.Add(fileInfo);
            return _repository.Commit();
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
