using Data.Core.Interfaces;
using System;

namespace Data.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IActivityRepository Activities { get; }
        IActivityTypeRepository ActivityTypes { get; }
        IRatingRepository Ratings { get; }
        IReviewRepository Reviews { get; }
        IUserRepository Users { get; }
        IUserActivityRepository UserActivities { get; }

        int Complete();
    }
}