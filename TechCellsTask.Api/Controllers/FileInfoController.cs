using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechCellsTask.Core.Entities;
using TechCellsTask.Core.Services;

namespace TechCellsTask.Api.Controllers
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
        /// This action launchs first when application running
        /// </summary>
        /// <returns>Return OkResult with message</returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("The Application running");
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
            var query = _service.GetGroupByExtension();

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
                if (_service.CreateFileInfo(file, newFileName, user.Id))
                    return Ok();

                return BadRequest(new { Message = "Error on Creating FileInfo" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
