using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FFG.Controllers
{
    public class RankingController : Controller
    {
        public IActionResult SkillRanking()
        {
            return View();
        }
    }
}