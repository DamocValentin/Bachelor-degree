using Constants;
using Data.Core;
using Data.Core.Domain;
using Data.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FFG.Seeder
{
    public class DbSeeder
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceProvider _serviceProvider;

        public DbSeeder(IUnitOfWork unitOfWork, IServiceProvider serviceProvider)
        {
            _unitOfWork = unitOfWork;
            _serviceProvider = serviceProvider;
        }

        public async Task SeedAsync()
        {
            using (var serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var service = serviceScope.ServiceProvider.GetService<DatabaseContext>();

                if (!await service.ActivityTypes.AnyAsync())
                {
                    await InsertActivityTypeData();
                }

            }
        }

        private async Task InsertActivityTypeData()
        {
            var activities = ActivityTypesConstants.GetActivitiesNames();

            foreach (var activity in activities)
            {
                await _unitOfWork.ActivityTypes.InsertAsync(ActivityType.Create(activity));
            }

            await _unitOfWork.CompleteAsync();
        }
    }
}
