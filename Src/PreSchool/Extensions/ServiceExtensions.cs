using PreSchool.Application.Models;
using PreSchool.Application.Models.PaymentPartners;
using PreSchool.Application.Services.AppUsers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PreSchool.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Enabling Cross-Origin Requests (CORS) for angular application
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }
        public static void ConfigureAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            // configure strongly typed settings objects
            var appSettingsSection = configuration.GetSection("AppSettings");

            services.Configure<AppSettings>(appSettingsSection);
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.Configure<FacebookAuthSettings>(configuration.GetSection("FacebookAuthSettings"));
            services.Configure<ReCaptchaSettings>(configuration.GetSection("ReCaptchaSettings"));
            services.Configure<esewaSettings>(configuration.GetSection("esewaSettings"));
            services.Configure<KhaltiSettings>(configuration.GetSection("KhaltiSettings"));
            services.Configure<IMEPaySettings>(configuration.GetSection("IMEPaySettings"));
            services.Configure<CardPaymentSettings>(configuration.GetSection("CyberSourceCardSetting"));

        }
        public static void AddJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.AuthenticationKey);
            // var key = Encoding.ASCII.GetBytes("Get this key form appsettings");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                            //var userService = context.HttpContext.RequestServices.GetRequiredService<IAppUserService>();
                            //var userId = int.Parse(context.Principal.Identity.Name);
                            //var user = userService.GetAppUserById(userId).GetAwaiter().GetResult();
                            //if (user == null)
                            //{
                            //    // return unauthorized if user no longer exists
                            //    context.Fail("Unauthorized");
                            //}
                            return Task.CompletedTask;
                    },
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/notifications")))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
       
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "PreSchool ",
                    Version = "v1",
                    Description = "PreSchool"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                }
                });
                //Locate the XML file being generated by ASP.NET...
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                //... and tell Swagger to use those XML comments.
                c.IncludeXmlComments(xmlPath);



            });
        }
    }
}
