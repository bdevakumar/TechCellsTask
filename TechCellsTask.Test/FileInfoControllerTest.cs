using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using TechCellsTask.Api.Controllers;
using TechCellsTask.Core.Dto;
using TechCellsTask.Core.Helpers;
using TechCellsTask.Core.Services;
using TechCellsTask.Infrastructure.Data;
using TechCellsTask.Infrastructure.Data.Repositories;
using Xunit;

namespace TechCellsTask.Test
{
    public class FileInfoControllerTest
    {
        private const string connectionStr =
            "Server=(localdb)\\mssqllocaldb;Database=TechCellsTask;Trusted_Connection=True;MultipleActiveResultSets=true";

        private ApplicationDbContext _context;
        public ApplicationDbContext Context
        {
            get
            {
                if (_context != null)
                    return _context;

                var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(connectionStr).Options;
                _context = new ApplicationDbContext(options);
                return _context;
            }
        }

        [Fact]
        public void GetFileInfos_ReturnsListOfFileInfos()
        {
            // Arrange
            var controller = CreateControllerInstance();

            // Act
            var result = controller.GetFileInfos();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<FileInfoGroup>>(okResult.Value);
            Assert.NotNull(model);
        }

        [Fact]
        public void GetFileRequirements_ReturnsExtensionAndSizeSetting()
        {
            // Arrange
            var controller = CreateControllerInstance();

            // Act
            var result = controller.GetFileRequirements();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<AppSettingOption>(okResult.Value);
            Assert.NotNull(model);
        }

        [Fact]
        public void Upload_ReturnsBadRequestResultWhenFilesIsNull()
        {
            // Arrange
            var controller = CreateControllerInstance();
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Form = new FormCollection(new Dictionary<string, StringValues>(), new FormFileCollection { });
            var cctx = new ControllerContext { HttpContext = httpContext };
            controller.ControllerContext = cctx;

            // Act
            var result = controller.Upload();

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(result);
            var statusCode = Assert.IsAssignableFrom<int>(badRequestResult.StatusCode);
            Assert.True(statusCode == 400);
        }

        [Fact]
        public void Upload_ReturnsBadRequestObjectResultWhenFileExtensionIsNotValid()
        {
            // Arrange
            var controller = CreateControllerInstance();
            controller.ControllerContext = RequestWithFile("test.txt");

            // Act
            var result = controller.Upload();

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var statusCode = Assert.IsAssignableFrom<int>(badRequestResult.StatusCode);
            Assert.True(statusCode == 400);
            Assert.NotNull(badRequestResult.Value);
        }

        [Fact]
        public void Upload_ReturnsOkResultWhenFileInfoSaved()
        {
            // Arrange
            var controller = CreateControllerInstance();
            controller.ControllerContext = RequestWithFile("test.png");

            // Act
            var result = controller.Upload();

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
            var statusCode = Assert.IsAssignableFrom<int>(okResult.StatusCode);
            Assert.True(statusCode == 200);
        }

        public FileInfoController CreateControllerInstance()
        {
            // initialize Repositories
            var userRepo = new UserRepository(Context);
            var fileInfoRepo = new FileInfoRepository(Context);

            // initialize AppSettingOption
            var options = Options.Create(GetOptions());

            // initialize Services
            var userService = new UserService(userRepo);
            var fileInfoService = new FileInfoService(fileInfoRepo, options);

            return new FileInfoController(fileInfoService, userService);
        }

        private ControllerContext RequestWithFile(string fileName)
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers.Add("Content-Type", "multipart/form-data");
            var file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a test file")), 0, 0, "Data", fileName);
            httpContext.Request.Form = new FormCollection(new Dictionary<string, StringValues>(), new FormFileCollection { file });
            var cctx = new ControllerContext();
            cctx.HttpContext = httpContext;
            return cctx;;
        }

        private AppSettingOption GetOptions()
        {
            return new AppSettingOption
            {
                UploadPath = "uploads",
                AllowedExtensions = new string[] { ".png", ".jpg" },
                AllowedFileSize = 1048576
            };
        }
    }
}
