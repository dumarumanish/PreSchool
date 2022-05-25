using PreSchool.Application.Infastructures;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace CleanArchitecture.Infrastructure.Services
{
    public class HostingEnvironmentService : IHostingEnvironmentService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HostingEnvironmentService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public string WebRootPath => _webHostEnvironment.ContentRootPath;
    }
}
