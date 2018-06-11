using System;
using System.Collections.Generic;

namespace Data.Core.Domain
{
    public class ActivityType
    {
        public Guid Id { get; private set; }
        public string ActivityTypeName { get; private set; }
        public ICollection<Activity> Activities { get; set; }

        public static ActivityType Create(string activityTypeName)
        {
            var instance = new ActivityType { Id = Guid.NewGuid() };
            instance.Update(activityTypeName);
            return instance;
        }

        public void Update(string activityTypeName)
        {
            ActivityTypeName = activityTypeName;
        }
    }
}
