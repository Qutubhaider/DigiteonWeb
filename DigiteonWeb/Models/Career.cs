using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolarCastleWeb.Models
{
    public class Career
    {
        public Guid   unCareerId { get; set; }
        public int    inCareerId { get; set; }
        [Required(ErrorMessage="Job title is required.")]
        public string stJobTitle { get; set; }
        [Required(ErrorMessage = "Job Category is required.")]
        public string stJobCategory { get; set; }
        //[Required(ErrorMessage = "Job Code is required.")]
        public string stJobCode { get; set; }
        //[Required(ErrorMessage = "Job Type is required.")]
        public string stJobType { get; set; }
        [Required(ErrorMessage = "Job Description is required.")]
        public string stJobDescription { get; set; }
        //[Required(ErrorMessage = "Job Responsiblities is required.")]
        public string stJobResponsiblities { get; set; }
        //[Required(ErrorMessage = "Job skills is required.")]
        public string stJobSkills { get; set; }
        //[Required(ErrorMessage = "Offer Description is required.")]
        public string stOfferDescription { get; set; }
        //[Required(ErrorMessage = "Job Advantages is required.")]
        public string stJobAdvantages { get; set; }
        [NotMapped]
        //[Required(ErrorMessage = "Please upload your CV.")]
        public IFormFile CV { get; set; }
        public string stUniqueFileName { get; set; }
        public string stFileName { get; set; }
        public string stSalary { get; set; }
        public string stSalaryType { get; set; }
        public string stJobDuration { get; set; }

        [Required(ErrorMessage = "Please enter start date.")]
        public DateTime? dtStartDate { get; set; }
       

    }
}
