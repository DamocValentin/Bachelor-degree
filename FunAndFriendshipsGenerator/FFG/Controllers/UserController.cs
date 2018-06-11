using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Core;
using Data.Core.Domain;
using FFG.Models.UserViewModels;
using FFG.UserContext;
using Microsoft.AspNetCore.Mvc;

namespace FFG.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public UserController(IUnitOfWork unitOfWork, IUserContext userContext)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<IActionResult> MyProfile()
        {
            UserViewModel userModel = new UserViewModel();

            userModel.UserName =  (await _unitOfWork.Users.GetByIdAsync(_userContext.Id)).UserName;

            var rating = (await _unitOfWork.Ratings.GetRatingByUserIdAsync(_userContext.Id));
            userModel.ActivitiesNumber = rating.GamesNumber;
            userModel.BehaviourPoints = rating.BehaviourScore;
            userModel.SkillPoints = rating.SkillScore;

            return View(userModel);
        }

        public async Task<IActionResult> MyActivities()
        {
            UserActivitiesViewModel myActivitiesModel = new UserActivitiesViewModel();
            var userActivities = (await _unitOfWork.UserActivities.GetAllAsync()).FindAll(x => x.UserId == _userContext.Id && x.ApprovalStatus);

            List<Activity> activities = new List<Activity>();

            foreach (var userActivity in userActivities)
            {
                activities.Add(await _unitOfWork.Activities.GetByIdAsync(userActivity.ActivityId));
            }

            myActivitiesModel.activitiesList = activities;

            return View(myActivitiesModel);
        }

        public async Task<IActionResult> UserProfile(Guid id)
        {
            UserViewModel userModel = new UserViewModel();

            userModel.UserName = (await _unitOfWork.Users.GetByIdAsync(id)).UserName;

            var rating = (await _unitOfWork.Ratings.GetRatingByUserIdAsync(id));
            userModel.ActivitiesNumber = rating.GamesNumber;
            userModel.BehaviourPoints = rating.BehaviourScore;
            userModel.SkillPoints = rating.SkillScore;

            return View(userModel);
        }

        [HttpGet]
        public async Task<IActionResult> CreateActivity()
        {
            CreateActivityViewModel viewModel = new CreateActivityViewModel();
            viewModel.activityTypes = new List<string>();
            var activities = await _unitOfWork.ActivityTypes.GetAllAsync();

            foreach(var activity in activities)
            {
                viewModel.activityTypes.Add(activity.ActivityTypeName);
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity(CreateActivityViewModel model)
        {
            var id = await _unitOfWork.ActivityTypes.GetActivityIdByNameAsync(model.ActivityTypeName);
            var activityType = await _unitOfWork.ActivityTypes.GetByIdAsync(id);
            Activity activity = Activity.Create(model.ActivityName, model.ActivityDescription, model.Location, model.StartTime, model.EndTime, model.Cost, model.NumberOfParticipants, activityType.Id);
            UserActivity userActivity = UserActivity.Create(_userContext.Id, activity.Id);

            await _unitOfWork.Activities.InsertAsync(activity);
            await _unitOfWork.UserActivities.InsertAsync(userActivity);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction("MyActivities", "User");
        }
    }
}