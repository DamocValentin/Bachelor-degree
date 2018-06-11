using Data.Core.Domain;
using System.Collections.Generic;

namespace FFG.Models.HomeViewModels
{
    public class ActivitiesSelectedViewModel
    {
        public List<Activity> activities { get; set; }
        public string activityTypeName { get; set; }
    }
}
