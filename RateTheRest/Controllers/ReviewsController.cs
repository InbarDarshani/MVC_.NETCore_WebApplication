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

namespace RateTheRest.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly ApplicationContext _dbcontext;                 //Working with db

        public ReviewsController(ApplicationContext context) { _dbcontext = context; }

        //_____________________________________________Actions Functions___________________________________________________________________________________

        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            return View(await _dbcontext.Reviews
                    .Include(r => r.Restaurant)
                    .Include(r => r.User)
                    .ToListAsync());
        }

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

            return View(review);
        }
        //_________________________________________________________

        //Partial View! - Create a Review in Restaurant Details page
        // GET: Reviews/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: Reviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(nameof(Review.ReviewID),nameof(Review.Score),nameof(Review.Text))] Review review, int restaurantId)
        {
            review.DateCreated = DateTime.Now;
            review.Restaurant = _dbcontext.Restaurants.Find(restaurantId);
            review.User = _dbcontext.Users.Find(1);

            _dbcontext.Add(review);
            await _dbcontext.SaveChangesAsync();

            return RedirectToAction("Details", "Restaurants", new { id = restaurantId });
        }
        //_________________________________________________________

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            return View();
        }

        // POST: Reviews/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind(nameof(Review.ReviewID), nameof(Review.Score), nameof(Review.Text))] Review review)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        //_________________________________________________________

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            return View();
        }

        // POST: Reviews/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        //_________________________________________________________


    }
}
