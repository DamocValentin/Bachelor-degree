using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FFG.Models.UserViewModels
{
    public class CreateActivityViewModel
    {
        [Required]
        public string ActivityName { get; set; }

        [Required]
        public string ActivityTypeName { get; set; }

        [Required]
        public string ActivityDescription { get; set; }

        [Required]
        public int NumberOfParticipants { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public int Cost { get; set; }

    }
}
