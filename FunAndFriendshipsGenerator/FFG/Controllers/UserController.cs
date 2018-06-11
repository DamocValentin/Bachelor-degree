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
            var userActivities = (await _unitOfWork.UserActivities.GetAllAsync()).FindAll(x => x.UserId == _userContext.Id);

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

        public IActionResult CreateActivity()
        {
            return View();
        }
    }
}