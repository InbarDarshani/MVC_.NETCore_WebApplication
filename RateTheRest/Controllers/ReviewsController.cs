using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RateTheRest.Data;
using RateTheRest.Models;
using RateTheRest.Additional;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace RateTheRest.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly ApplicationContext _dbcontext;                 //Working with db

        public ReviewsController(ApplicationContext context) { _dbcontext = context; }

        //_____________________________________________Actions Functions___________________________________________________________________________________

        //Partial View! - Show the Review in Restaurant Details page
        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var review = await _dbcontext.Reviews
                    .Include(r => r.Restaurant)
                    .Include(r => r.User)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.ReviewID == id);

            if (review == null) return NotFound();

            return PartialView("DetailsPartial");
        }
        //_________________________________________________________

        //Partial View! - Create a Review in Restaurant Details page
        // GET: Reviews/CreatePartial
        //[Authorize] - Manually
        public IActionResult CreatePartial()
        {
            if (!User.Identity.IsAuthenticated)
                return PartialView("_AuthenticationRequiredPartial");

            return PartialView("CreatePartial");
        }

        // POST: Reviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind(nameof(Review.ReviewID), nameof(Review.Score), nameof(Review.Text))] Review review, int restaurantId)
        {
            review.DateCreated = DateTime.Now;
            review.Restaurant = _dbcontext.Restaurants.Find(restaurantId);
            review.User = _dbcontext.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

            _dbcontext.Add(review);
            await _dbcontext.SaveChangesAsync();

            return RedirectToAction("Details", "Restaurants", new { id = restaurantId });
        }
        //_________________________________________________________

        //Partial View! - Create a Review in Restaurant Details page
        // GET: Reviews/Delete/5
        //[Authorize] - Manually
        public async Task<IActionResult> Delete(int? id)
        {
            if (!User.Identity.IsAuthenticated)
                return PartialView("_AuthenticationRequiredPartial");

            return PartialView("CreatePartial");
        }

        // POST: Reviews/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize] - Manually authorize current user
        public async Task<IActionResult> DeleteConfirmed(int id, int restaurantId)
        {
            if (User.Identity.Name != _dbcontext.Reviews.Find(id).User.UserName)
                return PartialView("_AccessDeniedPartial");

            var review = await _dbcontext.Reviews
                .Include(r => r.Restaurant)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ReviewID == id);

            return RedirectToAction("Details", "Restaurants", new { id = restaurantId });
        }









        //_________________________________NOT IN USE__________________________________________________________________
        // GET: Reviews
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _dbcontext.Reviews
        //            .Include(r => r.Restaurant)
        //            .Include(r => r.User)
        //            .ToListAsync());
        //}

        //// GET: Reviews/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null) return NotFound();

        //    var review = await _dbcontext.Reviews
        //            .Include(r => r.Restaurant)
        //            .Include(r => r.User)
        //            .AsNoTracking()
        //            .FirstOrDefaultAsync(m => m.ReviewID == id);

        //    if (review == null) return NotFound();

        //    return View(review);
        //}

        // GET: Reviews/Create
        //[Authorize]
        //public IActionResult Create()
        //{
        //    //ViewData["restaurantId"] = _restaurant.RestaurantID;
        //    return View();
        //}
        //// GET: Reviews/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    return View();
        //}

        //// POST: Reviews/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind(nameof(Review.ReviewID), nameof(Review.Score), nameof(Review.Text))] Review review)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        //_________________________________________________________

        // GET: Reviews/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    return View();
        //}

        //// POST: Reviews/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        //_________________________________________________________
    }
}
