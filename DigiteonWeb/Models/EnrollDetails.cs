using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PolarCastleWeb.Models
{
    public class EnrollDetails
    {
        public Guid unTrainingId { get; set; }

        [Required(ErrorMessage = "Please enter firstname.")]
        [NotMapped]
        public string stFirstName { get; set; }

        [Required(ErrorMessage = "Please enter lastname.")]
        [NotMapped]
        public string stLastName { get; set; }

        [Required(ErrorMessage = "Please enter email address.")]
        [NotMapped]
        public string stEmail { get; set; }

        [Required(ErrorMessage = "Please enter mobile.")]
        [NotMapped]
        public string stMobile { get; set; }
        [NotMapped]
        public string stAboutThis { get; set; }
        [NotMapped]
        public string stAboutPolarCastle { get; set; }

        public int inTrainingId { get; set; }
        public DateTime dtStartDate { get; set; }
        public DateTime dtEndDate { get; set; }
        public string stLocation { get; set; }
        public string stLanguage { get; set; }
        public string stCourseName { get; set; }
        public string stDescription { get; set; }
        public string stUnFileName { get; set; }
        public string stFileName { get; set; }
    }
}
