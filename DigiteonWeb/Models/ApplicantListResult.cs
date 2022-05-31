using System;

namespace DigiteonWeb.Models
{
    public class ApplicantListResult
    {
        public int inRecordCount { get; set; }
        public int inRownumber { get; set; }
        public int inCareerApplicationId { get; set; }
        public Guid unCareerApplicationId { get; set; }
        public int inCareerId { get; set; }
        public string stName { get; set; }
        public string stEmail { get; set; }
        public string stMessage { get; set; }
        public string stFileName { get; set; }
        public string stUnFileName { get; set; }
        public DateTime dtCreateDate { get; set; }


                               
    }
}
