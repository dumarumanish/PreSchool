using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PreSchool.EmailTemplates;
using PreSchool.EmailTemplates.ViewModels;
using PreSchool.EmailTemplatesView.Models;

namespace PreSchool.EmailTemplatesView.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/";
            ViewData["BaseUrl"] = baseUrl;

            return View(Templates.HtmlEmail, new EmailViewModel());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
