using DigiteonWeb.Common;
using DigiteonWeb.Data;
using DigiteonWeb.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static DigiteonWeb.Common.CommonFunctions;

namespace DigiteonWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext moDatabaseContext;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, DatabaseContext foDatabaseContext)
        {
            _logger = logger;
            moDatabaseContext = foDatabaseContext;
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

        [Route("careers")]
        public IActionResult Careers()
        {
            return View("~/Views/Home/Careers.cshtml");
        }

        [Route("privacy-policy")]
        public IActionResult Privacypolicy()
        {
            return View("~/Views/Home/Privacypolicy.cshtml");
        }

        [Route("terms-of-use")]
        public IActionResult Termsofuse()
        {
            return View("~/Views/Home/Termsofuse.cshtml");
        }

        [Route("training-calendar")]
        public IActionResult TrainingCalendar()
        {
            return View("~/Views/Home/TrainingCalendar.cshtml");
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

        [Route("services/training")]
        public IActionResult Training()
        {
            return View("~/Views/Home/TrainingProgram.cshtml");
        }

        [Route("services/recruitment-and-payroll-services")]
        public IActionResult Recruitment()
        {
            return View("~/Views/Home/Recruitment.cshtml");
        }

        [Route("services/web-development-and-maintenance")]
        public IActionResult ITdevelopment()
        {
            return View("~/Views/Home/ITdevelopment.cshtml");
        }

        [Route("login")]
        public IActionResult Login()
        {
            LoginVM loLoginVM = new LoginVM();
            return View("~/Views/Home/Login.cshtml", loLoginVM);
        }
        public IActionResult AuthenticateUser(LoginVM foLoginVM)
        {
            try
            {
                LoginResult LoginResult = moDatabaseContext.Set<LoginResult>().FromSqlInterpolated($"EXEC getUserByEmail @stUserEmail={foLoginVM.stEmail}").AsEnumerable().FirstOrDefault();
                if (LoginResult != null)
                {
                    if (foLoginVM.stPassword == LoginResult.stPassword)
                    {
                        if (LoginResult.inStatus == (int)CommonFunctions.UserStatus.InActive)
                        {
                            TempData["ResultCode"] = CommonFunctions.ActionResponse.Error;
                            TempData["Message"] = string.Format(AlertMessage.UserInactive);
                            return RedirectToAction("Login");
                        }
                        else
                        {
                            var claims = new List<Claim>();
                            claims.Add(new Claim(SessionConstant.stEmail, LoginResult.stEmail));
                            claims.Add(new Claim(SessionConstant.Id, LoginResult.inUserId.ToString()));
                            claims.Add(new Claim(SessionConstant.stUserName, LoginResult.stUsername));
                            claims.Add(new Claim(SessionConstant.unUserId, LoginResult.unUserId.ToString()));
                            claims.Add(new Claim(SessionConstant.RoleId, LoginResult.inRole.ToString()));
                            ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "Login");
                            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTimeOffset.UtcNow.AddHours(24) });

                            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });

                        }

                    }
                    else
                    {
                        TempData["ResultCode"] = CommonFunctions.ActionResponse.Error;
                        TempData["Message"] = string.Format(AlertMessage.CredentialMisMatch);
                        return RedirectToAction("Login");
                    }
                }
                else
                {
                    TempData["ResultCode"] = CommonFunctions.ActionResponse.Error;
                    TempData["Message"] = string.Format(AlertMessage.UserNotFound);
                    return RedirectToAction("Login");
                }

            }
            catch (Exception ex)
            {
                TempData["ResultCode"] = CommonFunctions.ActionResponse.Error;
                TempData["Message"] = string.Format(AlertMessage.OperationalError, "login");
                return RedirectToAction("Login");
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch (Exception e)
            {

                return RedirectToAction("Index", "Error");
            }
            return RedirectToAction("Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
