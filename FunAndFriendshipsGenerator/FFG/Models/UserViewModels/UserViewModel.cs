using Data.Core.Domain;

namespace FFG.Models.UserViewModels
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public double SkillPoints { get; set; }
        public double BehaviourPoints { get; set; }
        public int ActivitiesNumber { get; set; }
    }
}
