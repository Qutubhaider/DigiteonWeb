using DigiteonWeb.Common;
using DigiteonWeb.Data;
using DigiteonWeb.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;

namespace DigiteonWeb.Areas.Admin.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Area("Admin")]
    public class TrainingController : Controller
    {
        private readonly DatabaseContext moDatabaseContext;

        public TrainingController(DatabaseContext foDatabaseContext)
        {
            moDatabaseContext = foDatabaseContext;
        }
        public IActionResult Index()
        {
            return View("~/Areas/Admin/Views/Training/Training.cshtml");
        }

        public IActionResult Detail()
        {
            return View("~/Areas/Admin/Views/Training/TrainingDeatils.cshtml");
        }

        public IActionResult EnquiryList()
        {
            return View("~/Areas/Admin/Views/Training/EnquiryList.cshtml");
        }

        [HttpPost]
        public IActionResult SaveTraining(TrainingDetail foTrainingDetail)
        {
            try
            {
                SqlParameter loSuccess = new SqlParameter("@inSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
                moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC saveTraining @inTrainingId={foTrainingDetail.inTrainingId}, @dtStartDate={foTrainingDetail.dtStartDate}, @dtEndDate={foTrainingDetail.dtEndDate}, @stLocation={foTrainingDetail.stLocation}, @stLanguage={foTrainingDetail.stLanguage}, @stCourseName={foTrainingDetail.stCourseName},@inCreatedBy=1, @inSuccess={loSuccess} OUT");
                int fiSuccess = Convert.ToInt32(loSuccess.Value);
                if (fiSuccess == 101)
                {
                    TempData["ResultCode"] = CommonFunctions.ActionResponse.Add;
                    TempData["Message"] = string.Format(AlertMessage.SaveData);
                    return RedirectToAction("Index");
                }
                else
                    return RedirectToAction("Error");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
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
                return PartialView("~/Areas/Admin/Views/Training/_TrainingList.cshtml", loModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error");
            }

        }
    }
}
