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

            if (context.Restaurants.Any())
            {
                return;
            }

            //Initial tags table
            var tags = new Tag[]
            {
                new Tag{TagID = 1, TagName = "Kosher"},
                new Tag{TagID = 2, TagName = "Vegan"},
                new Tag{TagID = 3, TagName = "Meat"},
                new Tag{TagID = 4, TagName = "Dairy"}
            };
            foreach (Tag t in tags) { context.Tags.Add(t); }
            context.SaveChanges();




        }
    }
}
