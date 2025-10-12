using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolarCastleWeb.Models
{
    public class TrainingResults
    {
        public int inRecordCount { get; set; }
        public int inRownumber { get; set; }
        public int inTrainingId { get; set; }
        public Guid unTrainingId { get; set; }
        public DateTime dtStartDate { get; set; }
        public DateTime dtEndDate { get; set; }
        public string stLocation { get; set; }
        public string stLanguage { get; set; }
        public string stCourseName { get; set; }
    }
}
