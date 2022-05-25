using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Infastructures
{
    public interface IHostingEnvironmentService
    {
        string WebRootPath { get; }
    }
}
