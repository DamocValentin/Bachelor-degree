using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FFG.Models;
using Data.Core;
using FFG.Models.HomeViewModels;
using Data.Core.Domain;
using FFG.UserContext;

namespace FFG.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Guid _userId;

        public HomeController(IUnitOfWork unitOfWork, IUserContext userContext)
        {
            _unitOfWork = unitOfWork;
            var userId = userContext.Id;
            if (userId == null)
            {
                throw new ApplicationException("userId is null");
            }
            _userId = (Guid)userId;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Activities()
        {
            ActivitiesViewModel activitiesViewModel = new ActivitiesViewModel();
            var activitiesTypes =  await _unitOfWork.ActivityTypes.GetAllAsync();
            activitiesViewModel.activities = activitiesTypes;
            return View(activitiesViewModel);
        }

        public async Task<IActionResult> ActivitiesSelected(Guid id)
        {
            ActivitiesSelectedViewModel activitiesSelectedViewModel = new ActivitiesSelectedViewModel();
            var activitiesSelected = await _unitOfWork.Activities.GetAllAvailableActivitiesByTypeIdAsync(id);
            activitiesSelectedViewModel.activities = activitiesSelected;

            string activityType = (await _unitOfWork.ActivityTypes.GetByIdAsync(id)).ActivityTypeName;
            activitiesSelectedViewModel.activityTypeName = activityType;
            return View(activitiesSelectedViewModel);
        }

        public async Task<IActionResult> ParticipationRequest(Guid id)
        {
            var userActivity = UserActivity.Create(_userId, id);
            await _unitOfWork.UserActivities.InsertAsync(userActivity);
            await _unitOfWork.CompleteAsync();
            return RedirectToAction("MyProfile", "User");
        }
    }
}
