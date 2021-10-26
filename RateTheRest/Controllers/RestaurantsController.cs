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
        private readonly ApplicationContext _dbcontext;                 //Working with db
        private readonly IWebHostEnvironment _environment;             //Working with local project's directories 

        public RestaurantsController(ApplicationContext context) { _dbcontext = context; _environment = context.HostingEnvironment; }

        //_____________________________________________Actions Functions___________________________________________________________________________________

        // GET: Restaurants
        public async Task<IActionResult> Index()
        {
            return View(await _dbcontext.Restaurants
                .Include(r => r.Location)
                .Include(r => r.OpeningHours)
                .Include(r => r.Tags)
                .Include(r => r.Logo)
                .Include(r => r.Photos)
                .Include(r => r.Rating)
                .Include(r => r.Reviews)
                .Include(r => r.Chefs)
                .ToListAsync());
        }

        // GET: Restaurants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var restaurant = await _dbcontext.Restaurants          //Include db tables
                .Include(r => r.Location)
                .Include(r => r.OpeningHours)
                .Include(r => r.Tags)
                .Include(r => r.Logo)
                .Include(r => r.Photos)
                .Include(r => r.Rating)
                .Include(r => r.Reviews).ThenInclude(r => r.User)
                .Include(r => r.Chefs)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.RestaurantID == id);

            if (restaurant == null) return NotFound();

            return View(restaurant);
        }
        //_________________________________________________________

        // GET: Restaurants/Create
        public IActionResult Create()
        {
            ViewData["Chefs"] = _dbcontext.Chefs.ToList();
            return View();
        }

        // POST: Restaurants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind(nameof(Restaurant.RestaurantID),
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
            List<string> tags,
            IFormFile logo,
            List<IFormFile> photos,
            List<int> chefs)

        {
            //Update opening hours (nullable)
            if (days.Count > 0 && from.Count > 0 && to.Count > 0)
                restaurant.OpeningHours = UpdateOpeningHours(days, from, to);

            //Update locations
            restaurant.Location = location;

            //Update tags (nullable)
            if (tags.Count > 0)
                restaurant.Tags = UpdateTags(tags);

            //Upload and update images (nullable)
            if (logo != null)
                restaurant.Logo = UpdateLogo(logo);
            if (photos != null)
                restaurant.Photos = UpdateImages(photos);

            //Update chefs list (nullable)
            if (chefs.Count > 0)
                restaurant.Chefs = UpdateChefs(chefs);

            //Check binding and valudation                         
            //if (!ModelState.IsValid) return View(restaurant);

            //Add to db
            _dbcontext.Add(restaurant);
            await _dbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        //_________________________________________________________

        // GET: Restaurants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var restaurant = await _dbcontext.Restaurants          //Include db tables
                .Include(r => r.Location)
                .Include(r => r.OpeningHours)
                .Include(r => r.Tags)
                .Include(r => r.Logo)
                .Include(r => r.Photos)
                .Include(r => r.Rating)
                .Include(r => r.Reviews)
                .Include(r => r.Chefs)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.RestaurantID == id);

            if (restaurant == null) return NotFound();

            ViewData["Chefs"] = _dbcontext.Chefs.ToList();
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind(nameof(Restaurant.RestaurantID),
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
            List<string> tags,
            IFormFile logo,
            List<IFormFile> photos,
            List<int> chefs)
        {
            if (id != restaurant.RestaurantID) return NotFound();

            restaurant = await _dbcontext.Restaurants
                .Include(r => r.Location)
                .Include(r => r.OpeningHours)
                .Include(r => r.Tags)
                .Include(r => r.Logo)
                .Include(r => r.Photos)
                .Include(r => r.Rating)
                .Include(r => r.Reviews)
                .Include(r => r.Chefs).ThenInclude(c => c.Portrait)
                .FirstOrDefaultAsync(m => m.RestaurantID == id);

            //Update opening hours (nullable)
            if (days.Count > 0 && from.Count > 0 && to.Count > 0)
                restaurant.OpeningHours = UpdateOpeningHours(days, from, to, restaurant.RestaurantID);

            //Update location
            restaurant.Location = location;

            //Update tags (nullable)
            if (tags != null && tags.Count > 0)
                restaurant.Tags = UpdateTags(tags);

            //Upload and update images (nullable)
            if (logo != null)
                restaurant.Logo = UpdateLogo(logo, restaurant.RestaurantID);
            if (photos != null && photos.Count > 0)
                restaurant.Photos = UpdateImages(photos, restaurant.RestaurantID);

            //Update chefs list (nullable)
            if (chefs.Count > 0)
                restaurant.Chefs = UpdateChefs(chefs, restaurant.RestaurantID);

            try
            {
                _dbcontext.Update(restaurant);
                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantExists(restaurant.RestaurantID))
                    return NotFound();
                else
                    throw;
            }

            //Check binding and valudation
            //if (!ModelState.IsValid) return View(restaurant);

            return RedirectToAction(nameof(Index));
        }
        //_________________________________________________________

        // GET: Restaurants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var restaurant = await _dbcontext.Restaurants          //Include db tables
                .Include(r => r.Location)
                .Include(r => r.OpeningHours)
                .Include(r => r.Tags)
                .Include(r => r.Logo)
                .Include(r => r.Photos)
                .Include(r => r.Rating)
                .Include(r => r.Reviews)
                .Include(r => r.Chefs)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.RestaurantID == id);

            if (restaurant == null) return NotFound();

            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var restaurant = await _dbcontext.Restaurants
                .Include(r => r.Location)
                .Include(r => r.OpeningHours)
                .Include(r => r.Tags)
                .Include(r => r.Logo)
                .Include(r => r.Photos)
                .Include(r => r.Rating)
                .Include(r => r.Reviews)
                .Include(r => r.Chefs)
                .FirstOrDefaultAsync(m => m.RestaurantID == id);

            //Delete Files from directory (nullable)
            DeleteFiles(restaurant);

            _dbcontext.Restaurants.Remove(restaurant);
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //_________________________________________________________________________________________________________________________________________________



        //_____________________________________________Additional Functions___________________________________________________________________________________

        private bool RestaurantExists(int id)
        {
            return _dbcontext.Restaurants.Any(e => e.RestaurantID == id);
        }

        public LogoFile UpdateLogo(IFormFile logo, int restaurantID = -1)
        {
            string relativePath = Path.Combine("images", "Restaurants");
            string filename;
            LogoFile uploadedLogo;

            if (logo == null)
                return new LogoFile();

            //Delete existing logo from db if restaurant exists
            if (RestaurantExists(restaurantID) && logo != null)
            {
                LogoFile remove = _dbcontext.Logos.Where(l => l.Restaurant.RestaurantID == restaurantID).FirstOrDefault();
                _dbcontext.Logos.Remove(remove);
            }

            //Upload
            filename = "_" + "logo" + "_" + DateTime.Now.ToString("d.M.yyyy_HH.mm.ss") + "_" + ".png";
            using (FileStream stream = new FileStream(Path.Combine(_environment.WebRootPath, relativePath, filename), FileMode.Create))
            {
                logo.CopyTo(stream);
                uploadedLogo = new LogoFile { Path = Path.Combine("~/", relativePath, filename), FileName = filename };
            }

            return uploadedLogo;
        }

        //TOOD: delete existing photos
        public List<ImageFile> UpdateImages(List<IFormFile> images, int restaurantID = -1)
        {
            string relativePath = Path.Combine("images", "Restaurants");
            string filename;
            List<ImageFile> uploadedImages = new List<ImageFile>();
            int imageNumber = 1;

            //Check if more photos are added to existing resaurant
            if (RestaurantExists(restaurantID))
                uploadedImages = _dbcontext.Images.Where(l => l.Restaurant.RestaurantID == restaurantID).ToList();

            //Upload
            foreach (IFormFile i in images)
            {
                filename = "_" + "photo" + "_" + DateTime.Now.ToString("d.M.yyyy_HH.mm.ss") + "_" + imageNumber.ToString() + ".png";
                using (FileStream stream = new FileStream(Path.Combine(_environment.WebRootPath, relativePath, filename), FileMode.Create))
                {
                    i.CopyTo(stream);
                    uploadedImages.Add(new ImageFile { Path = Path.Combine("~/", relativePath, filename), FileName = filename });
                }
                imageNumber++;
            }
            return uploadedImages;
        }

        public List<OpeningHours> UpdateOpeningHours(List<string> checkedDays, List<DateTime> from, List<DateTime> to, int restaurantID = -1)
        {
            //Create opening hours
            List<OpeningHours> openingHours = new List<OpeningHours>();

            //Delete existing OpeningHours from db if restaurant exists
            if (RestaurantExists(restaurantID))
            {
                List<OpeningHours> remove = _dbcontext.OpeningHours.Where(o => o.Restaurant.RestaurantID == restaurantID).ToList();
                _dbcontext.OpeningHours.RemoveRange(remove);
            }

            foreach (string day in GlobalWeek.WEEK)
            {
                if (checkedDays.Contains(day))
                {
                    int i = checkedDays.IndexOf(day);
                    openingHours.Add(new OpeningHours() { DayOfWeek = day, Open = true, From = from.ElementAt(i), To = to.ElementAt(i) });
                }
                else
                    openingHours.Add(new OpeningHours() { DayOfWeek = day, Open = false });
            }
            return openingHours;
        }

        public List<Tag> UpdateTags(List<string> tagsNames, int restaurantID = -1)
        {
            List<Tag> tags;

            //Chek if clear all required
            if (tagsNames.First() == "0")
                return null;

            tags = new List<Tag>();
            foreach (string t in tagsNames)
                tags.Add(new Tag() { TagName = t });
            return tags;
        }

        public ICollection<Chef> UpdateChefs(List<int> chefsIds, int restaurantID = -1)
        {
            var chefs = new List<Chef>();

            //Chek if clear all required
            if (chefsIds.First() == 0)
                return null;

            foreach (int cId in chefsIds)
            {
                var chef = _dbcontext.Chefs.Find(cId);
                chefs.Add(chef);        
            }

            return chefs;
        }

        public void DeleteFiles(Restaurant restaurant)
        {
            //Restaurant restaurant = _dbcontext.Restaurants.Find(restaurantID);
            string direcroty = Path.Combine(_environment.WebRootPath, "images", "Restaurants");

            if (restaurant.Logo != null)
            {
                string logoFile = Path.Combine(direcroty, restaurant.Logo.FileName);
                if (System.IO.File.Exists(logoFile))
                    System.IO.File.Delete(logoFile);
            }

            if (restaurant.Photos != null && restaurant.Photos.Count > 0)
            {
                foreach (ImageFile p in restaurant.Photos)
                {
                    string photoFile = Path.Combine(direcroty, p.FileName);
                    if (System.IO.File.Exists(photoFile))
                        System.IO.File.Delete(photoFile);
                }
            }
        }
        //_________________________________________________________________________________________________________________________________________________



    }
}
