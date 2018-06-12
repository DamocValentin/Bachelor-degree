using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Core;
using Data.Core.Domain;
using FFG.Models.UserViewModels;
using FFG.Services;
using FFG.UserContext;
using Microsoft.AspNetCore.Mvc;

namespace FFG.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;
        private readonly IEmailSender _emailSender;

        public UserController(IUnitOfWork unitOfWork, IUserContext userContext, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
            _emailSender = emailSender;
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
            var userActivities = (await _unitOfWork.UserActivities.GetAllAsync()).FindAll(x => x.UserId == _userContext.Id && x.ApprovalStatus && !x.Rejected);

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
            userActivity.Update(true, true, false);

            await _unitOfWork.Activities.InsertAsync(activity);
            await _unitOfWork.UserActivities.InsertAsync(userActivity);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction("MyActivities", "User");
        }

        public async Task<IActionResult> ActivityDetails(Guid id)
        {
            ActivityDetailsViewModel model = new ActivityDetailsViewModel();
            model.Activity = await _unitOfWork.Activities.GetByIdAsync(id);
            model.Users = await _unitOfWork.UserActivities.GetAllUsersByActivityIdAsync(id);
            var owner = await _unitOfWork.UserActivities.GetOwnerByActivityIdAsync(id);
            model.Owner = false;
            if (owner.Id == _userContext.Id)
            {
                model.Owner = true;
            }
            model.UserRatings = new List<Rating>();
            model.UserActivities = new List<UserActivity>();
            foreach (var user in model.Users)
            {
                model.UserRatings.Add(await _unitOfWork.Ratings.GetRatingByUserIdAsync(user.Id));
                model.UserActivities.Add(await _unitOfWork.UserActivities.GetUserActivityByUserAndActivityAsync(user.Id, id));
            }
            model.Review = await _unitOfWork.Reviews.GetReviewByUserAndActivityAsync( _userContext.Id, id);

            return View(model);

        }

        public async Task<IActionResult> AcceptUserRequest(Guid userId, Guid activityId)
        {
            (await _unitOfWork.UserActivities.GetUserActivityByUserAndActivityAsync(userId, activityId)).Update(true, false, false);
            await _unitOfWork.CompleteAsync();

            (await _unitOfWork.Activities.GetByIdAsync(activityId)).UpdateParticipantsNumber();
            await _unitOfWork.CompleteAsync();

            await _unitOfWork.Reviews.InsertAsync(Review.Create(userId, activityId));
            await _unitOfWork.CompleteAsync();

            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            await _emailSender.SendRequestApprovedConfirmationAsync(user.Email);

            return RedirectToAction("MyActivities", "User");
        }

        public async Task<IActionResult> DeclineUserRequest(Guid userId, Guid activityId)
        {
            (await _unitOfWork.UserActivities.GetUserActivityByUserAndActivityAsync(userId, activityId)).Update(true, false, true);
            await _unitOfWork.CompleteAsync();

            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            await _emailSender.SendRequestDeclinedConfirmationAsync(user.Email);

            return RedirectToAction("MyActivities", "User");
        }

        [HttpGet]
        public async Task<IActionResult> CompleteReview(Guid activityId)
        {
            List<User> users = await _unitOfWork.UserActivities.GetAllUsersByActivityIdAsync(activityId);

            CompleteReviewViewModel model = new CompleteReviewViewModel();
            model.BehaviourProints = new List<int>();
            model.SkillPoints = new List<int>();
            model.Users = new List<User>();
            model.ActivityId = activityId;

            foreach(var user in users)
            {
                if(user.Id != _userContext.Id)
                {
                    model.Users.Add(user);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CompleteReview(CompleteReviewViewModel model)
        {
            for(int i = 0; i < model.Users.Count; i++)
            {
                var ranking = await _unitOfWork.Ratings.GetRatingByUserIdAsync(model.Users.ElementAt(i).Id);
                int games = ranking.GamesNumber + 1;
                ranking.Update(model.BehaviourProints.ElementAt(i), model.SkillPoints.ElementAt(i), games);
                await _unitOfWork.CompleteAsync();
            }

            (await _unitOfWork.Reviews.GetReviewByUserAndActivityAsync(_userContext.Id, model.ActivityId)).Update(true);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction("MyActivities", "User");
        }
    }
}