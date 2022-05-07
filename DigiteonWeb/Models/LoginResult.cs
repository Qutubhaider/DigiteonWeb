using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiteonWeb.Models
{
    public class LoginResult
    {
        public int inUserId { get; set; }
        public Guid unUserId { get; set; }
        public string stUsername { get; set; }
        public string stPassword { get; set; }
        public int inRole { get; set; }
        public string stEmail { get; set; }
        public string stMobile { get; set; }
        public int inStatus { get; set; }
    }
}
