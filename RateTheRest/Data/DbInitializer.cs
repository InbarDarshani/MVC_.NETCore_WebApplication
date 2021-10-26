using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RateTheRest.Models;
using RateTheRest.Additional;

namespace RateTheRest.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationContext context)
        {
            context.Database.EnsureCreated();

            if (context.Restaurants.Any() && context.Chefs.Any() && context.Users.Any())
                return;

            var restaurants = new Restaurant[]
            {
                new Restaurant { Name = "Hakosem", Location = new Location { Country = "Israel", City = "Tel Aviv", Street = "Shlomo HaMelekh", Number = 1 } },
                                 
                new Restaurant { Name = "Malka", Location = new Location { Country = "Israel", City = "Tel Aviv", Street = "Dafna", Number = 2 } },
                new Restaurant { Name = "Miznon", Location = new Location { Country = "Israel", City = "Tel Aviv", Street = "Shlomo Ibn Gabirol", Number = 23 } },
                                 
                new Restaurant { Name = "Dunya", Location = new Location { Country = "Israel", City = "Ashkelon", Street = "HaNamal ", Number = 9 } },
                new Restaurant { Name = "YAM", Location = new Location { Country = "Israel", City = "Jerusalem", Street = "HaAliya ", Number = 1 } },
                                 
                new Restaurant { Name = "Machneyuda", Location = new Location { Country = "Israel", City = "Jerusalem", Street = "Beit Ya'akov", Number = 10 } },
                new Restaurant { Name = "Yudale", Location = new Location { Country = "Israel", City = "Jerusalem", Street = "Beit Ya'akov", Number = 11 } },
                new Restaurant { Name = "Hasadna", Location = new Location { Country = "Israel", City = "Jerusalem", Street = "Hebron road", Number = 28 } },
                new Restaurant { Name = "The Palomar", Location = new Location { Country = "UK", City = "London", Street = "Rupert", Number = 34 } },
                new Restaurant { Name = "Shabour", Location = new Location { Country = "France", City = "Paris", Street = "Rue Saint-Sauveur", Number = 19 } }
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

            var users = new User[]
            {
                new User { Email = "admin88888dlfnxlv@gmail.com", Password = "adminadmin", FirstName = "Admin", LastName = "User", Role = "Admin"},
                new User { Email = "bobi5klbclvb@gmail.com", Password = "12345pass", FirstName = "Bobi", LastName = "Boten", Role = "Basic"}
            };
            foreach (User u in users) { context.Users.Add(u); }
            context.SaveChanges();

        }
    }
}
