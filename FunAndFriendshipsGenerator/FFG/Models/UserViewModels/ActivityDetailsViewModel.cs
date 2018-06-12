using Data.Core.Domain;
using System.Collections.Generic;

namespace FFG.Models.UserViewModels
{
    public class ActivityDetailsViewModel
    {
        public Activity Activity { get; set; }
        public List<User> Users { get; set; }
        public List<Rating> UserRatings { get; set; }
        public List<UserActivity> UserActivities { get; set; }
        public Review Review { get; set; }
        public bool Owner { get; set; }
    }
}
