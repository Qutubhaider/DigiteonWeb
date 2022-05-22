using DigiteonWeb.Common;
using DigiteonWeb.Data;
using DigiteonWeb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DigiteonWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CareerController : Controller
    {
        private readonly DatabaseContext moDatabaseContext;
        private readonly IWebHostEnvironment _env;

        public CareerController(DatabaseContext foDatabaseContext, IWebHostEnvironment env)
        {
            moDatabaseContext = foDatabaseContext;
            _env = env;
        }
        public IActionResult Index()
        {
            return View("~/Areas/Admin/Views/Career/Index.cshtml");
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
            {   if(career!=null)
                {
                    #region Save File
                    if(career.CV!=null)
                    {
                        string lsUnFileName = Guid.NewGuid().ToString()+Path.GetExtension(career.CV.FileName);
                        string lsLocalPath = Path.Combine(_env.WebRootPath, "Files", "Career") ;
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
                    else if(fiSuccess==102)
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

