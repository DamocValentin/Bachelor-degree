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
            userModel.ActivitiesNumber = 0;
            userModel.BehaviourPoints = 0;
            userModel.SkillPoints = 0;
            userModel.UserName =  (await _unitOfWork.Users.GetUserByIdAsync(_userContext.Id)).UserName;

            var ratings = (await _unitOfWork.Ratings.GetAllAsync()).FindAll(x => x.UserId == _userContext.Id);

            foreach(var rate in ratings)
            {
                userModel.ActivitiesNumber += rate.GamesNumber;
                userModel.BehaviourPoints += rate.BehaviourScore;
                userModel.SkillPoints += rate.SkillScore;
            }

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
            userModel.ActivitiesNumber = 0;
            userModel.BehaviourPoints = 0;
            userModel.SkillPoints = 0;
            userModel.UserName = (await _unitOfWork.Users.GetUserByIdAsync(id)).UserName;

            var ratings = (await _unitOfWork.Ratings.GetAllAsync()).FindAll(x => x.UserId == id);

            foreach (var rate in ratings)
            {
                userModel.ActivitiesNumber += rate.GamesNumber;
                userModel.BehaviourPoints += rate.BehaviourScore;
                userModel.SkillPoints += rate.SkillScore;
            }

            return View(userModel);
        }

        public IActionResult CreateActivity()
        {
            return View();
        }
    }
}