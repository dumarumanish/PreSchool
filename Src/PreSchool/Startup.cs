using PreSchool.Application;
using PreSchool.Application.Infastructures;
using PreSchool.Application.Models;
using PreSchool.Application.Services;
using PreSchool.Application.Services.BackgroundTasks;
using PreSchool.Application.Services.Notifications;
using PreSchool.EmailTemplates;
using PreSchool.Extensions;
using PreSchool.Filters;
using PreSchool.Infrastructure;
using PreSchool.Middlewares;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;

namespace PreSchool
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            // Configure appsettings
            services.ConfigureAppSettings(this.Configuration);



            // Add Cors
            services.ConfigureCors();
            services.AddCors(options =>
            {
                var appSettingsSection = Configuration.GetSection("AppSettings");
                var appSettings = appSettingsSection.Get<AppSettings>();
                options.AddPolicy("CorsPolicy",
                    builder =>
                        builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowAnyOrigin()
                        .AllowCredentials()
                        .WithOrigins(appSettings.AllowedOrigins)
                        );
            });
            // Add httpContext
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            // Add Application Dependencies
            services.AddApplication();
            services.AddHttpClient();
            services.AddSignalRNotification();
            // Add Infastructure Dependencies
            services.AddInfrastructure(this.Configuration);
           
            



            services.AddJWTAuthentication(this.Configuration);
            services.AddMvc(
                options =>
                {
                    // Model validate filter
                    options.Filters.Add(typeof(ModelValidationAttribute));

                }
                )
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddControllersAsServices()
                ;


            // Swagger
            services.AddSwagger();
 
            // profiling
            services.AddMiniProfiler(options => {
                options.RouteBasePath = "/profiler";
                }
            ).AddEntityFramework();
           
            // Customise default API behavour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });


            services.AddTransient<IRazorViewToStringRenderer, RazorViewToStringRenderer>();

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            //    services.AddTransient<Microsoft.Extensions.Hosting.IHostedService, ColdBootApi>();  //For run at startup and die.

            //hangfire reseduling bids.
            services.AddHangfire(config =>
                         config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                         .UseSimpleAssemblyNameTypeSerializer()
                         .UseSerializerSettings(new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })
                         .UseMemoryStorage());
            services.AddHangfireServer();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, 
            IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // profiling, url to see last profile check: http://localhost:xxxxx/profiler/results
                app.UseMiniProfiler();

            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("CorsPolicy");
            app.UseOptions();
            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("swagger/v1/swagger.json", "PreSchool API V1");

                c.RoutePrefix = string.Empty;

                // this custom html has miniprofiler integration
                c.IndexStream = () => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("PreSchool.SwaggerIndex.html");

                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                c.DocumentTitle = "PreSchool API";

            });

            app.UseStaticFiles();// For the wwwroot folder

            var imageDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Files");
            if (!Directory.Exists(imageDirectoryPath))
                Directory.CreateDirectory(imageDirectoryPath);

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(imageDirectoryPath)
                            ,
                RequestPath = "/Files"
            });


            ////Enable directory browsing
            //app.UseDirectoryBrowser(new DirectoryBrowserOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            //                Path.Combine(Directory.GetCurrentDirectory(), "Uplodaed_Documents")),
            //    RequestPath = "/Uplodaed_Documents"
            //});


            app.UseMiddleware<CustomExceptionMiddleware>();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseRequestLogging();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotificationHub>("/notifications");

            });
            // Hangfire
            // app.UseHangfireDashboard();
            app.UseHangfireDashboard(options: new DashboardOptions()
            {
                Authorization = new IDashboardAuthorizationFilter[]
            {
                new MyAuthorizationFilter()
                {
                    Username = "superadmin",
                    Password = "zxasqw12!@"
                }
            }
            });
            //    app.UseHangfireDashboard("/hangfire", new DashboardOptions
            //    {
            //        //AppPath = "" //The path for the Back To Site link. Set to null in order to hide the Back To  Site link.
            //        DashboardTitle = "My Website",
            //        Authorization = new IDashboardAuthorizationFilter[]
            //{
            //        new HangfireCustomBasicAuthenticationFilter{
            //            User = "superadmin",
            //            Pass = "zxasqw12!@"
            //        }
            //    }
            //    });
            var backgroundService = serviceProvider.GetService<IRecurringTaskService>();
            backgroundService.InitializeRecurringJob();
           






    }
}
    // TODO : Move this to other location
    class MyAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }

}
