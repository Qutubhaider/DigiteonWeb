using DigiteonWeb.Common;
using DigiteonWeb.Data;
using DigiteonWeb.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static DigiteonWeb.Common.CommonFunctions;

namespace DigiteonWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext moDatabaseContext;
        private readonly IWebHostEnvironment _env;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, DatabaseContext foDatabaseContext, IWebHostEnvironment env)
        {
            _logger = logger;
            moDatabaseContext = foDatabaseContext;
            _env = env;
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

        public IActionResult GetTraining(int? sort_column, string sort_order, int? pg, int? size)
        {
            try
            {
                string lsSearch = string.Empty;
                int liTotalRecords = 0, liStartIndex = 0, liEndIndex = 0;
                if (sort_column == 0 || sort_column == null)
                    sort_column = 1;
                if (string.IsNullOrEmpty(sort_order) || sort_order == "desc")
                {
                    sort_order = "desc";
                    ViewData["sortorder"] = "asc";
                }
                else
                {
                    ViewData["sortorder"] = "desc";
                }
                if (pg == null || pg <= 0)
                    pg = 1;
                if (size == null || size.Value <= 0)
                    size = 10;

                List<TrainingResults> loTrainingResults = new List<TrainingResults>();
                loTrainingResults = moDatabaseContext.Set<TrainingResults>().FromSqlInterpolated($"EXEC getTrainingList  @inSortColumn={sort_column},@stSortOrder={sort_order}, @inPageNo={pg.Value},@inPageSize={size.Value}").ToList();
                dynamic loModel = new ExpandoObject();
                loModel.GetTrainingList = loTrainingResults;
                if (loTrainingResults.Count > 0)
                {
                    liTotalRecords = loTrainingResults[0].inRecordCount;
                    liStartIndex = loTrainingResults[0].inRownumber;
                    liEndIndex = loTrainingResults[loTrainingResults.Count - 1].inRownumber;
                }
                loModel.Pagination = PaginationService.getPagination(liTotalRecords, pg.Value, size.Value, liStartIndex, liEndIndex);
                return PartialView("~/Views/Home/_TrainingCalendarList.cshtml", loModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error");
            }

        }

        [Route("training-calendar/{cname}/{id}")]
        public IActionResult Enroll(Guid id,string cname)
        {
            try
            {
                EnrollDetails loEnrollDetail = new EnrollDetails();
                loEnrollDetail.unTrainingId = id;
                if (id != Guid.Empty)
                {
                    loEnrollDetail = moDatabaseContext.Set<EnrollDetails>().FromSqlInterpolated($"EXEC getTrainingDetail @unTrainingId={id}").AsEnumerable().FirstOrDefault();
                }
                return View("~/Views/Home/Enroll.cshtml", loEnrollDetail);
            }
            catch (Exception ex)
            {
                return null;
            }
           
        }

        [Route("thank-you")]
        public IActionResult ThankYou()
        {
            return View("~/Views/Home/ThankYou.cshtml");
        }
        public IActionResult SaveEnroll(EnrollDetails foEnrollDetails)
        {
            try
            {
                SqlParameter loSuccess = new SqlParameter("@inSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
                moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC saveEnroll @unTrainingId={foEnrollDetails.unTrainingId}, @stFirstName={foEnrollDetails.stFirstName}, @stLastName={foEnrollDetails.stLastName}, @stEmail={foEnrollDetails.stEmail}, @stMobile={foEnrollDetails.stMobile}, @stAboutDigiteon={foEnrollDetails.stAboutDigiteon}, @stAboutThis={foEnrollDetails.stAboutThis}, @inSuccess={loSuccess} OUT");
                int fiSuccess = Convert.ToInt32(loSuccess.Value);
                if (fiSuccess == 101)
                {
                    return RedirectToAction("ThankYou");
                }
                else
                    return RedirectToAction("Error");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public IActionResult GetCareerListData(string jobTitle, int? sort_column, string sort_order, int? pg, int? size)
        {
            try
            {
                string lsSearch = string.Empty;
                int liTotalRecords = 0, liStartIndex = 0, liEndIndex = 0;
                if (sort_column == 0 || sort_column == null)
                    sort_column = 1;
                if (string.IsNullOrEmpty(sort_order) || sort_order == "desc")
                {
                    sort_order = "desc";
                    ViewData["sortorder"] = "asc";
                }
                else
                {
                    ViewData["sortorder"] = "desc";
                }
                if (pg == null || pg <= 0)
                    pg = 1;
                if (size == null || size.Value <= 0)
                    size = 10;

                List<CareerListResult> loCareerListResult = new List<CareerListResult>();
                loCareerListResult = moDatabaseContext.Set<CareerListResult>().FromSqlInterpolated($"EXEC getCareerList @stJobTitle={jobTitle},@inSortColumn={sort_column},@stSortOrder={sort_order},@inPageNo={pg},@inPageSize={size}").ToList();
                dynamic loModel = new ExpandoObject();
                loModel.GetCareerList = loCareerListResult;
                if (loCareerListResult.Count > 0)
                {
                    liTotalRecords = loCareerListResult[0].inRecordCount;
                    liStartIndex = loCareerListResult[0].inRownumber;
                    liEndIndex = loCareerListResult[loCareerListResult.Count - 1].inRownumber;
                }
                loModel.Pagination = PaginationService.getPagination(liTotalRecords, pg.Value, size.Value, liStartIndex, liEndIndex);
                return PartialView("~/Views/Home/_CareerList.cshtml", loModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error");
            }

        }

        [Route("careers/apply-now/{jname}/{id}")]
        public IActionResult ApplyNow(Guid id,string jname)
        {
            try
            {
                CareerApplication application = new CareerApplication();
                application.unCareerId = id;
                application.stJobName= jname;
                return View("~/Views/Home/ApplicationDetail.cshtml", application);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error");
            }
        }
        public async Task<IActionResult> SaveCareerApplication(CareerApplication application)
        {
            try
            {
                if (application != null)
                {
                    #region Save File
                    if (application.CV != null)
                    {
                        string lsUnFileName = Guid.NewGuid().ToString() + Path.GetExtension(application.CV.FileName);
                        string lsLocalPath = Path.Combine(_env.WebRootPath, "Files", "Career");
                        if (!Directory.Exists(lsLocalPath))
                            Directory.CreateDirectory(lsLocalPath);
                        using (var stream = new FileStream(lsLocalPath + "/" + lsUnFileName, FileMode.Create))
                        {
                            await application.CV.CopyToAsync(stream);
                        }
                        application.stFileName = application.CV.FileName;
                        application.stUnFileName = lsUnFileName;
                    }
                    #endregion

                    SqlParameter loSuccess = new SqlParameter("@inSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC saveCareerApplication @unCareerId={application.unCareerId} ,@inCareerApplicationId={application.inCareerApplicationId},@stName={application.stName} ,@stEmail={application.stEmail} ,@stMessage={application.stMessage} ,@stFileName={application.stFileName} ,@stUnFileName={application.stUnFileName},@inSuccess={loSuccess} OUT");
                    int fiSuccess = Convert.ToInt32(loSuccess.Value);
                    if (fiSuccess == 101)
                    {
                        TempData["ResultCode"] = CommonFunctions.ActionResponse.Add;
                        TempData["Message"] = string.Format(AlertMessage.SaveData);
                        return RedirectToAction("Index");
                    }
                    else if (fiSuccess == 102)
                    {
                        TempData["ResultCode"] = CommonFunctions.ActionResponse.Update;
                        TempData["Message"] = string.Format(AlertMessage.SaveData);
                        return RedirectToAction("Index");
                    }
                    else
                        return RedirectToAction("Error");
                }
                return RedirectToAction("Error");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }

        }
    }
}
