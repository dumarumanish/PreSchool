using PreSchool.Infrastructure;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Filters;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace PreSchool
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                 .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                 .Build();
        public async static Task Main(string[] args)
        {
            var dbConnectionString = Configuration.GetConnectionString("DatabaseName");

            var columns = new Serilog.Sinks.MSSqlServer.ColumnOptions
            {
                AdditionalColumns = new Collection<SqlColumn>
                           {
                               new SqlColumn("AppUserId",System.Data.SqlDbType.Int,true),
                               new SqlColumn("ActivityTypeId",System.Data.SqlDbType.Int,false),
                               new SqlColumn("Parameters",System.Data.SqlDbType.NVarChar,true),
                           },

            };
            columns.Store.Remove(StandardColumn.Properties);
            columns.Store.Remove(StandardColumn.MessageTemplate);
            columns.Store.Remove(StandardColumn.LogEvent);


            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
               .Enrich.FromLogContext()
               .WriteTo.Async(a => a.File("Logs/Log.txt", LogEventLevel.Information, rollingInterval: RollingInterval.Day, fileSizeLimitBytes: null, buffered: true)) // Write all the logs
               .WriteTo.Logger(l => l
                    .Filter.ByIncludingOnly(Matching.WithProperty<bool>("IsActivity", p => p))
                    .WriteTo.Async(a => a.MSSqlServer(dbConnectionString,
                       sinkOptions: new Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options.SinkOptions
                       {
                           TableName = "Log",
                           SchemaName = "Log",
                           AutoCreateSqlTable = false,
                       }, null, null,
                       LogEventLevel.Information,
                       null,
                       columnOptions: columns,
                       null,
                       null
                    ))
               )
               .CreateLogger();

            Serilog.Debugging.SelfLog.Enable(msg =>
            {
                Debug.Print(msg);
                Debugger.Break();
            });

            try
            {
                Log.Information("Web Application Starts...");
                var host = CreateWebHostBuilder(args).Build();
                using (var scope = host.Services.CreateScope())
                {
                    try
                    {
                        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                        context.Database.Migrate();


                        await DbInitializer.Initialize(context);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "An error occurred while migrating or initializing the database.");
                    }
                }

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
         .CaptureStartupErrors(true)
         .UseSetting("detailedErrors", "true")
         .UseStartup<Startup>()
         .UseSerilog()
         ;
    }
}
