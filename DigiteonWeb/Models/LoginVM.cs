using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DigiteonWeb.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email address is required.")]
        public string stEmail { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string stPassword { get; set; }
    }
}
