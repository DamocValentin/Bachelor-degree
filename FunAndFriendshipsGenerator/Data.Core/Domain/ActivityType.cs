using System;
using System.Collections.Generic;

namespace Data.Core.Domain
{
    public class ActivityType
    {
        public Guid Id { get; private set; }
        public string Type { get; private set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<Activity> Activities { get; set; }

        public static ActivityType Create(string type)
        {
            var instance = new ActivityType { Id = Guid.NewGuid() };
            instance.Update(type);
            return instance;
        }

        public void Update(string type)
        {
            Type = type;
        }
    }
}
