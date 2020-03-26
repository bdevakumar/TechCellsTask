using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TechCellsTaskApi.Core.Data.Entities;
using TechCellsTaskApi.Core.Helpers;
using TechCellsTaskApi.Core.Services;
using TechCellsTaskApi.Models;

namespace TechCellsTaskApi.Controllers
{
    /// <summary>
    /// Performs operations on the FileInfo table
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FileInfoController : Controller
    {
        private readonly FileInfoService _service;
        private readonly UserService _userService;

        /// <summary>
        /// Initializes new instances of dependencies that are used in this controller
        /// </summary>
        /// <param name="service">An instances of FileInfoService</param>
        /// <param name="userService">An instances of UserService</param>
        /// <param name="options">An instances of IOptions<AppSettingOption></param>
        public FileInfoController(
            FileInfoService service,
            UserService userService
        )
        {
            _service = service;
            _userService = userService;
        }

        /// <summary>
        /// Gets list of FileInfos grouped by file extension and ordered by FileName
        /// </summary>
        /// <returns>
        /// Returns list of fileinfos
        /// </returns>
        [HttpGet("GetFileInfos")]
        public IActionResult GetFileInfos()
        {
            var query = _service.GetAllAsQueryable()
                                .Include(i => i.User)
                                .AsEnumerable()
                                .GroupBy(g => g.FileExtension)
                                .Select(s => new FileInfoGroupModel
                                {
                                    Extension = s.Key,
                                    Files = s.Select(ss => new FileInfoModel
                                    {
                                        FileName = ss.FileName,
                                        NewFileName = ss.NewFileName,
                                        FileExtension = ss.FileExtension,
                                        FileSize = ss.FileSize,
                                        UploadDate = ss.UploadDate,
                                        UserName = ss.User.UserName
                                    })
                                });
            return Ok(query);
        }

        /// <summary>
        /// Gets allowed file extensions and size from appsettings
        /// </summary>
        /// <returns>
        /// Returns file extensions and size
        /// </returns>
        [HttpGet("GetFileRequirements")]
        public IActionResult GetFileRequirements()
        {
            return Ok(_service.GetAppSettingOption());
        }

        /// <summary>
        /// Uploads file and save file infos to db
        /// </summary>
        /// <param name="file">file that is being upload</param>
        /// <returns>If uploads file successfully returns OkResult, else returns BadRequestResult</returns>
        [HttpPost("Upload")]
        public IActionResult Upload()
        {
            try
            {
                var files = Request?.Form?.Files;
                if (files == null || files.Count() == 0)
                    return BadRequest();

                var file = files[0];
                string ext = System.IO.Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!_service.FileExtensionIsValid(ext))
                    return BadRequest(new { Message = "File extension is not valid" });

                if (!_service.FileSizeIsValid(file.Length))
                    return BadRequest(new { Message = "File size is not valid" });

                var user = _userService.GetAllAsQueryable().FirstOrDefault();
                if (user == null)
                    return BadRequest(new { Message = "User not found" });

                string newFileName = _service.CopyToUploads(file);

                FileInfo fileInfo = new FileInfo
                {
                    FileName = file.FileName,
                    FileSize = file.Length,
                    FileExtension = ext.Replace(".", ""),
                    NewFileName = newFileName,
                    UploadDate = DateTime.Now,
                    UserId = user.Id
                };

                _service.Create(fileInfo);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
