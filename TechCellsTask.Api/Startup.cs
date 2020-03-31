using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using TechCellsTask.Core.Helpers;
using TechCellsTask.Core.Interfaces;
using TechCellsTask.Core.Services;
using TechCellsTask.Infrastructure.Data;
using TechCellsTask.Infrastructure.Data.Repositories;

namespace TechCellsTask.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register NewtonsoftJson and set serialization settings
            services.AddControllers()
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                        options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                        options.SerializerSettings.FloatFormatHandling = Newtonsoft.Json.FloatFormatHandling.String;
                        options.SerializerSettings.Culture = System.Globalization.CultureInfo.InvariantCulture;
                        options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
                    });

            // Set database connection string
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TechCellsTaskContext")));

            // Get appsetting.json settings
            services.Configure<AppSettingOption>(Configuration);

            services.AddCors();

            // Register Services and Repositories
            services.AddScoped(typeof(IEntityService<>), typeof(EntityService<>));
            services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>));

            services.AddScoped<UserRepository>();
            services.AddScoped<FileInfoRepository>();

            services.AddScoped<UserService>();
            services.AddScoped<FileInfoService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(b =>
            {
                b.AllowAnyOrigin();
                b.AllowAnyMethod();
                b.AllowAnyHeader();
            });

            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads")),
                RequestPath = "/uploads"
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
