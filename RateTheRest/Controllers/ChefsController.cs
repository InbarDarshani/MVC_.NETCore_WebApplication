using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateTheRest.Additional;
using RateTheRest.Data;
using RateTheRest.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RateTheRest.Controllers
{
    public class ChefsController : Controller
    {
        private readonly ApplicationContext _dbcontext;       //Working with db
        private IWebHostEnvironment _environment;             //Working with local project's directories 

        public ChefsController(ApplicationContext context) { _dbcontext = context; _environment = context.HostingEnvironment; }

        //_____________________________________________Actions Functions___________________________________________________________________________________

        // GET: Chefs
        public async Task<IActionResult> Index(List<string> searchString, List<string> searchBy)
        {
            IEnumerable<Chef> chefs = _dbcontext.Chefs
                .Include(c => c.Portrait)
                .Include(c => c.Restaurants).ThenInclude(r => r.Rating).ThenInclude(r => r.Users)
                .Include(c => c.Restaurants).ThenInclude(r => r.Reviews)
                .ToList();

            //Perform search
            IEnumerable<(string searchBy, string searchString)> filters = searchBy.Zip(searchString, (s1, s2) => (s1, s2));
            foreach (var sb in filters)
            {
                if (!string.IsNullOrEmpty(sb.searchString))
                {
                    switch (sb.searchBy)
                    {
                        case "Name":
                            {
                                chefs = from c in chefs
                                        where c.FirstName.Contains(sb.searchString) || c.LastName.Contains(sb.searchString)
                                        select c;
                                break;
                            }
                        case "Restaurant":
                            {
                                chefs = from c in chefs
                                        where c.Restaurants.Any(r => r.Name.Contains(sb.searchString))
                                        select c;
                                break;
                            }
                        default:
                            break;
                    }
                }
            }

            if (searchString.Count > 0)
                ViewData["FiltersPlaceHolders"] = searchString.ToArray();
            else
                ViewData["FiltersPlaceHolders"] = new string[2];
            return View(chefs);
        }
        //_________________________________________________________

        // GET: Chefs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var chef = await _dbcontext.Chefs
                .Include(c => c.Portrait)
                .Include(c => c.Restaurants).ThenInclude(r => r.Rating).ThenInclude(r => r.Users)
                .Include(c => c.Restaurants).ThenInclude(r => r.Reviews)
                .FirstOrDefaultAsync(m => m.ChefID == id);

            if (chef == null) return NotFound();

            return View(chef);
        }
        //_________________________________________________________

        // GET: Chefs/Create
        [Authorize(Policy = "RequireAdmin")]
        public IActionResult Create()
        {
            ViewData["Restaurants"] = _dbcontext.Restaurants.ToList();
            return View();
        }

        // POST: Chefs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind(nameof(Chef.ChefID),
            nameof(Chef.FirstName),
            nameof(Chef.LastName),
            nameof(Chef.Description))]
            Chef chef,
            IFormFile portrait,
            List<int> restaurants)
        {

            //Upload and update portrait photo (nullable)
            if (portrait != null)
                chef.Portrait = UpdatePortrait(portrait);

            //Update restaurants list (nullable)
            if (restaurants.Count > 0)
                chef.Restaurants = UpdateRestaurants(restaurants, chef.ChefID);

            //Check binding and valudation
            //if (!ModelState.IsValid) return View(chef);

            _dbcontext.Add(chef);
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //_________________________________________________________

        // GET: Chefs/Edit/5
        [Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var chef = await _dbcontext.Chefs
                .Include(c => c.Portrait)
                .Include(c => c.Restaurants).ThenInclude(r => r.Rating).ThenInclude(r => r.Users)
                .Include(c => c.Restaurants).ThenInclude(r => r.Reviews)
                .FirstOrDefaultAsync(m => m.ChefID == id);

            if (chef == null) return NotFound();

            ViewData["Restaurants"] = _dbcontext.Restaurants.ToList();
            return View(chef);
        }

        // POST: Chefs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            string FirstName, string LastName, string Description,
            IFormFile portrait,
            List<int> restaurants)
        {
            if (!ChefExists(id)) return NotFound();

            Chef chef = await _dbcontext.Chefs
                .Include(c => c.Portrait)
                .Include(c => c.Restaurants)
                .FirstOrDefaultAsync(m => m.ChefID == id);

            //Update properties
            if (FirstName != null)
                chef.FirstName = FirstName;           
            if (LastName != null)
                chef.LastName = LastName;
            if (Description != null)
                chef.Description = Description;

            //Upload and update portrait photo (nullable)
            if (portrait != null)
                chef.Portrait = UpdatePortrait(portrait, chef.ChefID);

            //Update restaurants list (nullable)
            if (restaurants.Count > 0)
                chef.Restaurants = UpdateRestaurants(restaurants, chef.ChefID);

            try
            {
                _dbcontext.Update(chef);
                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChefExists(chef.ChefID))
                    return NotFound();
                else
                    throw;
            }

            //Check binding and valudation
            //if (!ModelState.IsValid) return View(chef);

            return RedirectToAction(nameof(Index));
        }
        //_________________________________________________________

        // GET: Chefs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var chef = await _dbcontext.Chefs
                .Include(c => c.Portrait)
                .Include(c => c.Restaurants).ThenInclude(r => r.Rating).ThenInclude(r => r.Users)
                .Include(c => c.Restaurants).ThenInclude(r => r.Reviews)
                .FirstOrDefaultAsync(m => m.ChefID == id);

            if (chef == null) { return NotFound(); }

            return View(chef);
        }

        // POST: Chefs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chef = await _dbcontext.Chefs
                .Include(c => c.Portrait)
                .Include(c => c.Restaurants)
                .FirstOrDefaultAsync(m => m.ChefID == id);

            //Delete Files from directory (nullable)
            DeleteFiles(chef);

            _dbcontext.Chefs.Remove(chef);
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //_________________________________________________________________________________________________________________________________________________


        //_____________________________________________Additional Functions___________________________________________________________________________________
        private bool ChefExists(int id)
        {
            return _dbcontext.Chefs.Any(e => e.ChefID == id);
        }

        public PortraitFile UpdatePortrait(IFormFile portrait, int chefID = -1)
        {
            string relativePath = Path.Combine("images", "Chefs");
            string filename;
            PortraitFile uploadedPortrait;

            //Delete existing or default logo from db if chef exists
            if (ChefExists(chefID))
            {
                PortraitFile remove = _dbcontext.Portraits.Where(p => p.Chef.ChefID == chefID).FirstOrDefault();
                _dbcontext.Portraits.Remove(remove);
            }

            //Upload
            filename = "_" + "Portrait" + "_" + DateTime.Now.ToString("d.M.yyyy_HH.mm.ss") + "_" + ".png";
            using (FileStream stream = new FileStream(Path.Combine(_environment.WebRootPath, relativePath, filename), FileMode.Create))
            {
                portrait.CopyTo(stream);
                uploadedPortrait = new PortraitFile { Path = Path.Combine("~/", relativePath, filename), FileName = filename };
            }
            return uploadedPortrait;
        }

        public ICollection<Restaurant> UpdateRestaurants(List<int> restaurantsIds, int chefID = -1)
        {
            var restaurants = new List<Restaurant>();

            //Check if clear all required
            if (restaurantsIds.First() == 0)
                return null;

            foreach (int rId in restaurantsIds)
            {
                var restaurant = _dbcontext.Restaurants.Find(rId);
                restaurants.Add(restaurant);
            }

            return restaurants;
        }

        public void DeleteFiles(Chef chef)
        {
            string direcroty = Path.Combine(_environment.WebRootPath, "images", "Chefs");

            if (chef.Portrait != null)
            {
                string portraitFile = Path.Combine(direcroty, chef.Portrait.FileName);
                if (System.IO.File.Exists(portraitFile))
                    System.IO.File.Delete(portraitFile);
            }
        }
        //_________________________________________________________________________________________________________________________________________________

    }
}
