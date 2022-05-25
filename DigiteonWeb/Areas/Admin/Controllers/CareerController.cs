using DigiteonWeb.Common;
using DigiteonWeb.Data;
using DigiteonWeb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiteonWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CareerController : Controller
    {
        private readonly DatabaseContext moDatabaseContext;
        private readonly IWebHostEnvironment _env;
        private readonly static int miPageSize = 10;

        public CareerController(DatabaseContext foDatabaseContext, IWebHostEnvironment env)
        {
            moDatabaseContext = foDatabaseContext;
            _env = env;
        }
        public IActionResult Index()
        {
            return View("~/Areas/Admin/Views/Career/Index.cshtml");
        }
        public IActionResult GetCareerListData(string jobTitle, int? sort_column, string sort_order, int? pg, int? size)
        {
            StringBuilder lolog = new StringBuilder();
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
                    size = miPageSize;

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
                return PartialView("~/Areas/Admin/Views/Career/_CareerList.cshtml", loModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error");
            }

        }

        public IActionResult Detail(Guid? id)
        {
            try
            {
                Career loCareer = new Career();
                if (id != null)
                {
                    loCareer = moDatabaseContext.Set<Career>().FromSqlInterpolated($"EXEC getCareerDetail @unCareerId={id}").AsEnumerable().FirstOrDefault();
                }
                return View("~/Areas/Admin/Views/Career/Detail.cshtml", loCareer);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveCareer(Career career)
        {
            try
            {
                if (career != null)
                {
                    #region Save File
                    if (career.CV != null)
                    {
                        string lsUnFileName = Guid.NewGuid().ToString() + Path.GetExtension(career.CV.FileName);
                        string lsLocalPath = Path.Combine(_env.WebRootPath, "Files", "Career");
                        if (!Directory.Exists(lsLocalPath))
                            Directory.CreateDirectory(lsLocalPath);
                        using (var stream = new FileStream(lsLocalPath + "/" + lsUnFileName, FileMode.Create))
                        {
                            await career.CV.CopyToAsync(stream);
                        }
                        career.stFileName = career.CV.FileName;
                        career.stUniqueFileName = lsUnFileName;
                    }
                    #endregion

                    SqlParameter loSuccess = new SqlParameter("@inSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC saveCareer @unCareerId={career.unCareerId},@inCareerId={career.inCareerId},@stJobTitle={career.stJobTitle},@stJobResponsiblities={career.stJobResponsiblities},@stJobSkills={career.stJobSkills},@stJobCategory={career.stJobCategory},@stJobCode={career.stJobCode},@stJobType={career.stJobType},@stJobDescription={career.stOfferDescription},@stOfferDescription={career.stOfferDescription},@stJobAdvantages={career.stJobAdvantages},@stUniqueFileName={career.stUniqueFileName},@stFileName={career.stFileName},@stSalary={career.stSalary},@stSalaryType={career.stSalaryType},@stJobDuration={career.stJobDuration},@dtStartDate={career.dtStartDate},@inSuccess={loSuccess} OUT");
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

        public IActionResult ApplyNow(Guid id)
        {
          
            try
            {
                CareerApplication application = new CareerApplication();
                application.unCareerId = id;
                return View("~/Areas/Admin/Views/Career/ApplicationDetail.cshtml", application);
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

