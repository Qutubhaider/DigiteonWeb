using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiteonWeb.Models
{
    public class EnrollResults
    {
        public int inRecordCount { get; set; }
        public int inRownumber { get; set; }
        public int inEnrollId { get; set; }
        public string stFirstName { get; set; }
        public string stLastName { get; set; }
        public string stEmail { get; set; }
        public string stMobile { get; set; }
        public string stAboutThis { get; set; }
        public DateTime dtCreateDate { get; set; }
    }
}
