using System;

namespace Data.Core.Domain
{
    public class UserActivity
    {
        public Guid Id { get; private set; }
        public bool ApprovalStatus { get; private set; }
        public bool Owner { get; private set; }

        public Guid UserId { get; private set; }
        public virtual User User { get; set; }

        public Guid ActivityId { get; private set; }
        public virtual Activity Activity { get; set; }

        public static UserActivity Create(Guid userId, Guid activityId)
        {
            var instance = new UserActivity { Id = Guid.NewGuid() };
            instance.Update(false, false, userId, activityId);
            return instance;
        }

        public void Update(bool approvalStatus, bool owner, Guid userId, Guid activityId)
        {
            ApprovalStatus = approvalStatus;
            Owner = owner;
            UserId = userId;
            ActivityId = activityId;
        }
    }
}
