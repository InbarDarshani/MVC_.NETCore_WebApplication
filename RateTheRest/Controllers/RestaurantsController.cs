using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RateTheRest.Data;
using RateTheRest.Models;
using RateTheRest.Additional;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace RateTheRest.Controllers
{


    public class RestaurantsController : Controller
    {
        private readonly ApplicationContext _dbcontext;       //Working with db
        private IWebHostEnvironment _environment;             //Working with local project's directories 

        public RestaurantsController(ApplicationContext context) { _dbcontext = context; _environment = context.env; }

        // GET: Restaurants
        public async Task<IActionResult> Index()
        {
            return View(await _dbcontext.Restaurants.ToListAsync());
        }

        // GET: Restaurants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var restaurant = await _dbcontext.Restaurants
                .Include(c => c.Chefs)
                .Include(r => r.Rating)
                .FirstOrDefaultAsync(m => m.RestaurantID == id);


            if (restaurant == null) return NotFound();

            return View(restaurant);
        }

        // GET: Restaurants/Create
        public IActionResult Create()
        {
            ViewData["Tags"] = _dbcontext.Tags.ToList();
            return View();
        }

        // POST: Restaurants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(
                nameof(Restaurant.RestaurantID),
                nameof(Restaurant.Name),
                nameof(Restaurant.Description))]
                Restaurant restaurant,
                [Bind(nameof(Location.LocationId),
                nameof(Location.City),
                nameof(Location.Street),
                nameof(Location.Number))]
                Location location,
                List<string> days,
                List<DateTime> from,
                List<DateTime> to,
                List<int> tags,
                IFormFile logo,
                List<IFormFile> photos)
        {

            //Update opening hours
            restaurant.OpeningHours = UpdateOpeningHours(days, from, to);

            //Update locations
            restaurant.Location = location;

            //Update tags
            restaurant.Tags = UpdateTags(tags);

            //Upload and update images
            restaurant.Logo = UploadImage(restaurant.RestaurantID, "logo", (List<IFormFile>)logo).FirstOrDefault();
            restaurant.Photos = UploadImage(restaurant.RestaurantID, "photo", photos);

            //Check binding and valudation
            //if (!ModelState.IsValid) return View(restaurant);

            //Add to db
            _dbcontext.Add(restaurant);
            await _dbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // GET: Restaurants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _dbcontext.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RestaurantID,Name,Logo,Description,Location,OpeningHours,Category,RatingID")] Restaurant restaurant)
        {
            if (id != restaurant.RestaurantID)
            {
                return NotFound();
            }


            try
            {
                _dbcontext.Update(restaurant);
                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantExists(restaurant.RestaurantID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            if (!ModelState.IsValid)
                return View(restaurant);

            return RedirectToAction(nameof(Index));

        }

        // GET: Restaurants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _dbcontext.Restaurants
                .FirstOrDefaultAsync(m => m.RestaurantID == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var restaurant = await _dbcontext.Restaurants.FindAsync(id);
            _dbcontext.Restaurants.Remove(restaurant);
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantExists(int id)
        {
            return _dbcontext.Restaurants.Any(e => e.RestaurantID == id);
        }



        //________________________________________________________________

        public List<ImageFile> UploadImage(int restaurantID, string imageType, List<IFormFile> images)
        {
            string path;
            string filename;

            if (images == null || images.Count == 0)
                return new List<ImageFile>();

            //Create folder for Restaurant
            path = Path.Combine(_environment.WebRootPath, "images", "Restaurants", restaurantID.ToString());
            Directory.CreateDirectory(path);

            //Upload
            List<ImageFile> uploadedImages = new List<ImageFile>();
            foreach (IFormFile i in images)
            {
                filename = "_" + imageType + "_" + DateTime.Now.ToString("d.M.yyyy_HH.mm.ss") + ".png";
                using (FileStream stream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    i.CopyTo(stream);
                    uploadedImages.Add(new ImageFile { ImageType = imageType, Path = path, FileName = filename });
                }
            }
            return uploadedImages;
        }

        public List<DayHours> UpdateOpeningHours(List<string> days, List<DateTime> from, List<DateTime> to, int restaurantID = -1)
        {
            List<DayHours> openingHours = new List<DayHours>();
            for (int i = 0; i < days.Count; i++)
                openingHours.Add(new DayHours() { DayOfWeek = days.ElementAt(i), Open = true, From = from.ElementAt(i), To = to.ElementAt(i) });
            return openingHours;
        }

        public List<Tag> UpdateTags(List<int> tagsIds, int restaurantID = -1)
        {
            List<Tag> tags = new List<Tag>();
            foreach (int i in tagsIds)
                tags.Add(_dbcontext.Tags.Find(i));
            return tags;
        }

       

    }
}
