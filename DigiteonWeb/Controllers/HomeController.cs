using DigiteonWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DigiteonWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View("~/Views/Home/Index.cshtml");
        }

        [Route("about-us")]
        public IActionResult About()
        {
            return View("~/Views/Home/About.cshtml");
        }

        public IActionResult Privacy()
        {
            return View("~/Views/Home/Privacy.cshtml");
        }

        [Route("contact-us")]
        public IActionResult Contact()
        {
            return View("~/Views/Home/Contact.cshtml");
        }

        [Route("services")]
        public IActionResult Services()
        {
            return View("~/Views/Home/Services.cshtml");
        }

        [Route("training-programs/scaled-agile")]
        public IActionResult ScaledAgile()
        {
            return View("~/Views/Home/Scaled-Agile.cshtml");
        }

        [Route("training-programs/scrum-alliance")]
        public IActionResult ScrumAlliance()
        {
            return View("~/Views/Home/Scrum-Alliance.cshtml");
        }

        [Route("training-programs/kanban")]
        public IActionResult Kanban()
        {
            return View("~/Views/Home/Kanban.cshtml");
        }

        [Route("services/it-consulting-and-solutions")]
        public IActionResult ITConsulting()
        {
            return View("~/Views/Home/ITConsulting.cshtml");
        }

        [Route("services/training-program")]
        public IActionResult Training()
        {
            return View("~/Views/Home/TrainingProgram.cshtml");
        }

        [Route("services/recruitment-and-payroll-services")]
        public IActionResult Recruitment()
        {
            return View("~/Views/Home/Recruitment.cshtml");
        }

        [Route("services/it-development-and-maintenance")]
        public IActionResult ITdevelopment()
        {
            return View("~/Views/Home/ITdevelopment.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
