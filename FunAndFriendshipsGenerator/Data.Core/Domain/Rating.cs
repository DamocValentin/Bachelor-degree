using System;

namespace Data.Core.Domain
{
    public class Rating
    {
        public Guid Id { get; private set; }
        public double BehaviourScore { get; private set; }
        public double SkillScore { get; private set; }
        public int GamesNumber { get; private set; }

        public Guid UserId { get; private set; }
        public virtual User User { get; set; }

        public static Rating Create(Guid userId)
        {
            var instance = new Rating { Id = Guid.NewGuid() };
            instance.Update(0, 0, 0, userId);
            return instance;
        }

        public void Update(double behaviourScore, double skillScore, int gamesNumber, Guid userId)
        {
            BehaviourScore = behaviourScore;
            SkillScore = skillScore;
            GamesNumber = gamesNumber;
            UserId = userId;
        }
    }
}
