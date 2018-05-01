using System;

namespace Data.Core.Domain
{
    public class Review
    {
        public Guid Id { get; private set; }
        public bool ReviewSubmitted { get; private set; }

        public Guid UserId { get; private set; }
        public virtual User User { get; set; }

        public Guid ActivityId { get; private set; }
        public virtual Activity Activity { get; set; }

        public static Review Create(Guid userId, Guid activityId)
        {
            var instance = new Review { Id = Guid.NewGuid() };
            instance.Update(false, userId, activityId);
            return instance;
        }

        public void Update(bool reviewSubmitted, Guid userId, Guid activityId)
        {
            ReviewSubmitted = reviewSubmitted;
            UserId = userId;
            ActivityId = activityId;
        }
    }
}
