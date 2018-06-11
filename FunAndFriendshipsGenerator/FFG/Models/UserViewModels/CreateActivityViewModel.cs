using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FFG.Models.UserViewModels
{
    public class CreateActivityViewModel
    {
        [Required]
        [Display(Name = "Activity name")]
        public string ActivityName { get; set; }

        [Required]
        [Display(Name = "Type of activity")]
        public string ActivityTypeName { get; set; }

        [Required]
        [Display(Name = "Activity description")]
        public string ActivityDescription { get; set; }

        [Required]
        [Display(Name = "Number of participants")]
        public int NumberOfParticipants { get; set; }

        [Required]
        [Display(Name = "Location")]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Start date and time")]
        public DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "End date and time")]
        public DateTime EndTime { get; set; }

        [Required]
        [Display(Name = "Cost for participation in Lei")]
        public int Cost { get; set; }

        public List<string> activityTypes { get; set; }

    }
}
