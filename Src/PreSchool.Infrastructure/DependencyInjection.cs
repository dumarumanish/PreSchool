using CleanArchitecture.Infrastructure.Services;
using PreSchool.Application.Infastructures;
using PreSchool.Application.Services.Notifications;
using PreSchool.Infrastructure.Files;
using PreSchool.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PreSchool.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {


            // Add DbContext to DI
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DatabaseName")));

            services.AddTransient<ApplicationDbContext>();

            

            services.AddTransient<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
          

            services.AddSingleton<IDateTime, DateTimeService>();
            services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();
            services.AddSingleton<IDateConverter, DateConverter>();
            services.AddSingleton<IHostingEnvironmentService, HostingEnvironmentService>();
            services.AddSingleton<IExcelService, ExcelService>();
 
            return services;
        }

    }
}
