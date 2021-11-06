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

            if (context.Restaurants.Any() && context.Chefs.Any() && context.Users.Any())
                return;

            var restaurants = new Restaurant[]
            {
                //Eyal Shani
                new Restaurant { Name = "Malka",
                    Location = new Location { Country = "Israel", City = "Tel Aviv", Street = "Dafna", Number = 2 },
                    },

                new Restaurant { Name = "Miznon",
                    Location = new Location { Country = "Israel", City = "Tel Aviv", Street = "Shlomo Ibn Gabirol", Number = 23 },
                    },

                //Meir Adoni
                new Restaurant { Name = "Dunya",
                    Location = new Location { Country = "Israel", City = "Ashkelon", Street = "HaNamal ", Number = 9 },
                    },

                //Assaf Granit
                new Restaurant { Name = "Machneyuda",
                    Location = new Location { Country = "Israel", City = "Jerusalem", Street = "Beit Ya'akov", Number = 10 },
                    },

                new Restaurant { Name = "Yudale",
                    Location = new Location { Country = "Israel", City = "Jerusalem", Street = "Beit Ya'akov", Number = 11 },
                    },

                new Restaurant { Name = "Hasadna",
                    Location = new Location { Country = "Israel", City = "Jerusalem", Street = "Hebron road", Number = 28 },
                    },

                new Restaurant { Name = "The Palomar",
                    Location = new Location { Country = "UK", City = "London", Street = "Rupert", Number = 34 },
                    },

                new Restaurant { Name = "Shabour",
                    Location = new Location { Country = "France", City = "Paris", Street = "Rue Saint-Sauveur", Number = 19 },
                    }
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

            var users = new ApplicationUser[]
            {
                new ApplicationUser { FirstName = "Admin", LastName = "User", UserName = "Admin@walla.com", Email = "Admin@walla.com",
                                      NormalizedUserName="ADMIN@WALLA.COM", NormalizedEmail = "ADMIN@WALLA.COM",
                                      PasswordHash = "AQAAAAEAACcQAAAAEE33VlUuAC1IRJLdFJoOAkFpFeLMwd4WJQyk3OCkeNXK+RGZiur8gLuj17leZOLPGQ==",
                                      SecurityStamp = "C2DDJCG2JMPGW7Y4EZXEKAXTLYB7UR5H",
                                      ConcurrencyStamp = "3282148d-fa28-4e94-a8d8-708084d66030"},

                new ApplicationUser { FirstName = "Basic", LastName = "User", UserName = "Basic@walla.com", Email = "Basic@walla.com",
                                      NormalizedUserName="BASIC@WALLA.COM", NormalizedEmail = "BASIC@WALLA.COM",
                                      PasswordHash = "AQAAAAEAACcQAAAAEL0NHZ4TI17+dZMjnP9p1SMmUtoTC5dvKmN4eh1VuGClSm6Q5UO6AldjHeSemT2Wmg==",
                                      SecurityStamp = "KA5LLP5OYMZU43VIU6QJODEWL44SU6UA",
                                      ConcurrencyStamp = "262459cc-ec2b-4dab-8848-61754de853d2" }
            };
            foreach (ApplicationUser u in users) { context.Users.Add(u); }
            context.SaveChanges();


            foreach (Restaurant restaurant in restaurants)
            {
                var rand = new Random();
                for (int i = 0; i < rand.Next(20,50); i++)
                {
                    createReview(new Review(), rand.Next(0,10), restaurant.RestaurantID, "Basic@walla.com");
                }
            }
            context.SaveChanges();

            void createReview(Review review, int score, int restaurantId, string username)
            {
                review.DateCreated = DateTime.Now;
                review.Score = score;
                review.Restaurant = context.Restaurants          //Include db tables
                    .Include(r => r.Location)
                    .Include(r => r.OpeningHours)
                    .Include(r => r.Tags)
                    .Include(r => r.Logo)
                    .Include(r => r.Photos)
                    .Include(r => r.Rating).ThenInclude(r => r.Users)
                    .Include(r => r.Reviews).ThenInclude(r => r.User)
                    .Include(r => r.Chefs)
                    .FirstOrDefault(m => m.RestaurantID == restaurantId);
                review.User = context.Users
                    .Include(u => u.Rating).ThenInclude(r => r.Restaurant)
                    .FirstOrDefault(u => u.UserName == username);

                context.Add(review);
                context.SaveChanges();

                //Update Retaurant's Rating
                review.Restaurant.Rating.Users.Add(review.User);
                review.Restaurant.Rating.Score = review.Restaurant.Rating.calcScore();
                context.SaveChanges();
            }
        }
    }
}

