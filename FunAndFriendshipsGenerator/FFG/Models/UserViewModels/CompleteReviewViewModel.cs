using Data.Core.Domain;
using System;
using System.Collections.Generic;

namespace FFG.Models.UserViewModels
{
    public class CompleteReviewViewModel
    {
        public List<User> Users { get; set; }
        public List<Guid> UsersId { get; set; }
        public List<int> BehaviourProints { get; set; }
        public List<int> SkillPoints { get; set; }
        public Guid ActivityId { get; set; }
    }
}
