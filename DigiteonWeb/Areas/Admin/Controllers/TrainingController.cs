using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigiteonWeb.Areas.Admin.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Area("Admin")]
    public class TrainingController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Areas/Admin/Views/Training/Training.cshtml");
        }

        public IActionResult Detail()
        {
            return View("~/Areas/Admin/Views/Training/TrainingDeatils.cshtml");
        }
    }
}
