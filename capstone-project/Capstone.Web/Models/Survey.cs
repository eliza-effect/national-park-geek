using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;


namespace Capstone.Web.Models
{
    public class Survey
    {
        public int SurveyId { get; set; }

        [Required]
        public string ParkCode { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string ActivityLevel { get; set; }

        //this is to create the dropdown list of parks in the survey view
        public List<SelectListItem> Parks { get; set; }
    }
}