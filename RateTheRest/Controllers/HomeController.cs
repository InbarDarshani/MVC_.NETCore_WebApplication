using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using RateTheRest.Data;
using RateTheRest.Models;
using RateTheRest.Additional;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RateTheRest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _dbcontext;       //Working with db

        public HomeController(ILogger<HomeController> logger, ApplicationContext context) { _logger = logger; _dbcontext = context; }

        public IActionResult Index()
        {
            ViewData["Top5RatedRestaurants"] = _dbcontext.Restaurants
                 .Include(r => r.Location)
                .Include(r => r.OpeningHours)
                .Include(r => r.Tags)
                .Include(r => r.Logo)
                .Include(r => r.Photos)
                .Include(r => r.Rating).ThenInclude(r => r.Users)
                .Include(r => r.Reviews).ThenInclude(r => r.User)
                .Include(r => r.Chefs)
                .OrderByDescending(r => r.Rating.Score).Take(5).ToArray();

            ViewData["RestaurantsAndReviews"] = _dbcontext.Restaurants
                .Include(r => r.Reviews).ThenInclude(r => r.User).ToArray();

            ViewData["UsersByNumOfReviews"] = (from r in _dbcontext.Reviews.ToList()
                                               join u in _dbcontext.Users.ToList()
                                               on r.User.Id equals u.Id 
                                               group r by r.User.Id into reviewsPerUser
                                               orderby reviewsPerUser.Count() descending
                                               select new Tuple<string, int>(reviewsPerUser.Select(r => r.User.FullName).First(), reviewsPerUser.Count())).ToList();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
