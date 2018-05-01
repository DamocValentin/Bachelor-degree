using System;
using System.Collections.Generic;

namespace Data.Core.Domain
{
    public class Activity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Location { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public int Cost { get; private set; }
        public int ParticipantsNumber { get; private set; }

        public Guid ActivityTypeId { get; private set; }
        public virtual ActivityType ActivityType { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<UserActivity> UserActivities { get; set; }

        public static Activity Create(string name, string description, string location, DateTime startTime, DateTime endTime, int cost, int participantsNumber, Guid activityTypeId)
        {
            var instance = new Activity { Id = Guid.NewGuid() };
            instance.Update(name, description, location, startTime, endTime, cost, participantsNumber, activityTypeId);
            return instance;
        }

        public void Update(string name, string description, string location, DateTime startTime, DateTime endTime, int cost, int participantsNumber, Guid activityTypeId)
        {
            Name = name;
            Description = description;
            Location = location;
            StartTime = startTime;
            EndTime = endTime;
            Cost = cost;
            ParticipantsNumber = participantsNumber;
            ActivityTypeId = activityTypeId;

        }
    }
}
