using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DigiteonWeb.Models
{
    public class TrainingDetail
    {
        public int inTrainingId { get; set; }
        public Guid unTrainingId { get; set; }

        [Required(ErrorMessage = "Please enter start date.")]
        public DateTime dtStartDate { get; set; }

        [Required(ErrorMessage = "Please enter end date.")]
        public DateTime dtEndDate { get; set; }

        [Required(ErrorMessage = "Please enter location.")]
        public string stLocation { get; set; }

        [Required(ErrorMessage = "Please enter language.")]
        public string stLanguage { get; set; }

        [Required(ErrorMessage = "Please enter course name.")]
        public string stCourseName { get; set; }
    }
}
