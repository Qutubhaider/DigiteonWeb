using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiteonWeb.Models
{
    public class CareerApplication
    {
        public int inCareerApplicationId {get;set;}
        public Guid unCareerApplicationId {get;set;}
        public int inCareerId            {get;set;}
        [Required(ErrorMessage="Name is required")]
        public string stName                {get;set;}
        [Required(ErrorMessage = "Name is required")]
        public string stEmail               {get;set;}
        [NotMapped]
        public IFormFile CV { get; set; }
        public string stFileName            {get;set;}
        public string stUnFileName { get; set; }
    }
}
