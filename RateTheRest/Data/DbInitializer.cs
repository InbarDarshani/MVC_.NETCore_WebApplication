using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RateTheRest.Models;
using RateTheRest.Additional;
using Microsoft.AspNetCore.Identity;

namespace RateTheRest.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationContext context)
        {
            context.Database.EnsureCreated();

            if (context.Restaurants.Any() && context.Chefs.Any())
                return;

            var restaurants = new Restaurant[]
            {
                new Restaurant { Name = "Hakosem", Location = new Location { Country = "Israel", City = "Tel Aviv", Street = "Shlomo HaMelekh", Number = 1 }, OpeningHours = InitializeOpeningHours() },

                new Restaurant { Name = "Malka", Location = new Location { Country = "Israel", City = "Tel Aviv", Street = "Dafna", Number = 2 }, OpeningHours = InitializeOpeningHours() },
                new Restaurant { Name = "Miznon", Location = new Location { Country = "Israel", City = "Tel Aviv", Street = "Shlomo Ibn Gabirol", Number = 23 }, OpeningHours = InitializeOpeningHours() },

                new Restaurant { Name = "Dunya", Location = new Location { Country = "Israel", City = "Ashkelon", Street = "HaNamal ", Number = 9 }, OpeningHours = InitializeOpeningHours() },
                new Restaurant { Name = "YAM", Location = new Location { Country = "Israel", City = "Jerusalem", Street = "HaAliya ", Number = 1 }, OpeningHours = InitializeOpeningHours() },

                new Restaurant { Name = "Machneyuda", Location = new Location { Country = "Israel", City = "Jerusalem", Street = "Beit Ya'akov", Number = 10 }, OpeningHours = InitializeOpeningHours() },
                new Restaurant { Name = "Yudale", Location = new Location { Country = "Israel", City = "Jerusalem", Street = "Beit Ya'akov", Number = 11 }, OpeningHours = InitializeOpeningHours() },
                new Restaurant { Name = "Hasadna", Location = new Location { Country = "Israel", City = "Jerusalem", Street = "Hebron road", Number = 28 }, OpeningHours = InitializeOpeningHours() },
                new Restaurant { Name = "The Palomar", Location = new Location { Country = "UK", City = "London", Street = "Rupert", Number = 34 }, OpeningHours = InitializeOpeningHours() },
                new Restaurant { Name = "Shabour", Location = new Location { Country = "France", City = "Paris", Street = "Rue Saint-Sauveur", Number = 19 }, OpeningHours = InitializeOpeningHours() }
            };
            foreach (Restaurant r in restaurants) { context.Restaurants.Add(r); }
            context.SaveChanges();

            var chefs = new Chef[]
            {
                new Chef { FirstName = "Israel", LastName = "Aharoni" },
                new Chef { FirstName = "Eyal", LastName = "Shani" },
                new Chef { FirstName = "Meir", LastName = "Adoni" },
                new Chef { FirstName = "Assaf", LastName = "Granit" },
                new Chef { FirstName = "Moshik", LastName = "Roth" }
            };
            foreach (Chef c in chefs) { context.Chefs.Add(c); }
            context.SaveChanges();

            var roles = new IdentityRole[]
            {
                new IdentityRole("Admin"),
                new IdentityRole("Basic")
            };
            foreach (IdentityRole i in roles) { context.Roles.Add(i); }
            context.SaveChanges();
        }

        public static List<OpeningHours> InitializeOpeningHours()
        {
            var openingHours = new OpeningHours[]
            {
                new OpeningHours { DayOfWeek = "Sunday", Open = false },
                new OpeningHours { DayOfWeek = "Monday", Open = false },
                new OpeningHours { DayOfWeek = "Tuesday", Open = false },
                new OpeningHours { DayOfWeek = "Wednesday", Open = false },
                new OpeningHours { DayOfWeek = "Thursday", Open = false },
                new OpeningHours { DayOfWeek = "Friday", Open = false },
                new OpeningHours { DayOfWeek = "Saturday", Open = false },
            };

            return openingHours.ToList<OpeningHours>();
        }
    }
}
