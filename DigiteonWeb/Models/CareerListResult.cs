using System;

namespace DigiteonWeb.Models
{
    public class CareerListResult
    {
        public int inRecordCount { get; set; }
        public int inRownumber { get; set; }
        public int inCareerId { get; set; }
        public Guid unCareerId { get; set; }
        public string stJobTitle { get; set; }
        public string stJobCategory { get; set; }
        public string stJobCode { get; set; }
        public string stJobType { get; set; }
        public string stSalary { get; set; }
        public DateTime dtStartDate { get; set; }
    }
}
