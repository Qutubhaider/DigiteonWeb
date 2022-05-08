using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DigiteonWeb.Models
{
    public class EnrollDetails
    {
        public Guid unTrainingId { get; set; }
        public string stCourseName { get; set; }

        [Required(ErrorMessage = "Please enter firstname.")]
        public string stFirstName { get; set; }

        [Required(ErrorMessage = "Please enter lastname.")]
        public string stLastName { get; set; }

        [Required(ErrorMessage = "Please enter email address.")]
        public string stEmail { get; set; }

        [Required(ErrorMessage = "Please enter mobile.")]
        public string stMobile { get; set; }
        public string stAboutThis { get; set; }
        public string stAboutDigiteon { get; set; }
	}
}
