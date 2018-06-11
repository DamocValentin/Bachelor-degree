using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Constants;
using Data.Core;
using Data.Core.Domain;
using FFG.Models.RankingViewModels;
using FFG.UserContext;
using Microsoft.AspNetCore.Mvc;

namespace FFG.Controllers
{
    public class RankingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public RankingController(IUnitOfWork unitOfWork, IUserContext userContext)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<IActionResult> SkillRanking(int pageNumber)
        {
            RankingViewModel viewModel = new RankingViewModel();
           
            double number = (await _unitOfWork.Ratings.GetAllAsync()).Count;
            number = number / ((double)RatingConstants.NumberOfItemsOnPage);

            viewModel.NumberOfPages = (int)Math.Ceiling(number);
            viewModel.PageNumber = pageNumber;

            var ratings = await _unitOfWork.Ratings.GetUsersRankingByPageAsync(pageNumber, RatingConstants.NumberOfItemsOnPage, RatingConstants.SkillRating);

            viewModel.Users = new List<User>();
            viewModel.Points = new List<double>();
            viewModel.WhiteSpaces = new List<string>();

            foreach (var rate in ratings)
            {
                var user = await _unitOfWork.Users.GetByIdAsync(rate.UserId);
                var nrSpaces = 50 - user.UserName.Length - rate.SkillScore.ToString().Length;
                string whiteSpaces = "";
                for (int i = 0; i < nrSpaces; i++)
                {
                    whiteSpaces += " ";
                }

                viewModel.Users.Add(user);
                viewModel.Points.Add(rate.SkillScore);
                viewModel.WhiteSpaces.Add(whiteSpaces);
            }
            return View(viewModel);
        }

        public async Task<IActionResult> BehaviourRanking(int pageNumber)
        {
            RankingViewModel viewModel = new RankingViewModel();

            var ratings = await _unitOfWork.Ratings.GetUsersRankingByPageAsync(pageNumber, RatingConstants.NumberOfItemsOnPage, RatingConstants.BehaviourRating);
            viewModel.Users = new List<User>();
            viewModel.Points = new List<double>();

            foreach (var rate in ratings)
            {
                viewModel.Users.Add(await _unitOfWork.Users.GetByIdAsync(rate.UserId));
                viewModel.Points.Add(rate.BehaviourScore);
            }


            return View(viewModel);
        }
    }
}