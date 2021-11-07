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

            //Create reviews with random score
            foreach (Restaurant restaurant in restaurants)
            {
                var rand = new Random();
                for (int i = 0; i < rand.Next(20, 50); i++)
                {
                    createReview(new Review(), rand.Next(0, 10), restaurant.RestaurantID, "Basic@walla.com");
                }
            }

            //Update chefs for each restaurant
            restaurants[0].Chefs.Add(chefs[1]);
            restaurants[1].Chefs.Add(chefs[1]);
            restaurants[2].Chefs.Add(chefs[2]);
            restaurants[3].Chefs.Add(chefs[3]);
            restaurants[4].Chefs.Add(chefs[3]);
            restaurants[5].Chefs.Add(chefs[3]);
            restaurants[6].Chefs.Add(chefs[3]);
            restaurants[7].Chefs.Add(chefs[3]);

            //set restaurants logos
            restaurants[0].Logo = new LogoFile { FileName = "Logo", Path = "~/dbInitialize/Malka/logo.jpg" };
            restaurants[1].Logo = new LogoFile { FileName = "Logo", Path = "~/dbInitialize/Miznon/logo.jpg" };
            restaurants[2].Logo = new LogoFile { FileName = "Logo", Path = "~/dbInitialize/Dunya/logo.jpg" };
            restaurants[3].Logo = new LogoFile { FileName = "Logo", Path = "~/dbInitialize/Machneyuda/logo.jpg" };
            restaurants[4].Logo = new LogoFile { FileName = "Logo", Path = "~/dbInitialize/Yudale/logo.jpg" };
            restaurants[5].Logo = new LogoFile { FileName = "Logo", Path = "~/dbInitialize/Hasadna/logo.jpg" };
            restaurants[6].Logo = new LogoFile { FileName = "Logo", Path = "~/dbInitialize/The Palomar/logo.jpg" };
            restaurants[7].Logo = new LogoFile { FileName = "Logo", Path = "~/dbInitialize/Shabour/logo.jpg" };

            //set restaurant photos
            restaurants[0].Photos.Add(new PhotoFile { FileName = "Photo", Path = "~/dbInitialize/Malka/1.jpg" });
            restaurants[0].Photos.Add(new PhotoFile { FileName = "Photo", Path = "~/dbInitialize/Malka/2.jpg" });
            restaurants[0].Photos.Add(new PhotoFile { FileName = "Photo", Path = "~/dbInitialize/Malka/3.jpg" });
            restaurants[1].Photos.Add(new PhotoFile { FileName = "Photo", Path = "~/dbInitialize/Miznon/1.jpg" });
            restaurants[1].Photos.Add(new PhotoFile { FileName = "Photo", Path = "~/dbInitialize/Miznon/2.jpg" });
            restaurants[1].Photos.Add(new PhotoFile { FileName = "Photo", Path = "~/dbInitialize/Miznon/3.jpg" });
            restaurants[1].Photos.Add(new PhotoFile { FileName = "Photo", Path = "~/dbInitialize/Miznon/4.jpg" });
            restaurants[2].Photos.Add(new PhotoFile { FileName = "Photo", Path = "~/dbInitialize/Dunya/1.jpg" });
            restaurants[2].Photos.Add(new PhotoFile { FileName = "Photo", Path = "~/dbInitialize/Dunya/2.jpg" });
            restaurants[2].Photos.Add(new PhotoFile { FileName = "Photo", Path = "~/dbInitialize/Dunya/3.jpg" });
            restaurants[2].Photos.Add(new PhotoFile { FileName = "Photo", Path = "~/dbInitialize/Dunya/4.jpg" });
            restaurants[3].Photos.Add(new PhotoFile { FileName = "Photo", Path = "~/dbInitialize/Machneyuda/1.jpg" });
            restaurants[3].Photos.Add(new PhotoFile { FileName = "Photo", Path = "~/dbInitialize/Machneyuda/2.jpg" });
            restaurants[3].Photos.Add(new PhotoFile { FileName = "Photo", Path = "~/dbInitialize/Machneyuda/3.jpg" });
            restaurants[4].Photos.Add(new PhotoFile { FileName = "Photo", Path = "~/dbInitialize/Yudale/1.jpg" });
            restaurants[4].Photos.Add(new PhotoFile { FileName = "Photo", Path = "~/dbInitialize/Yudale/2.jpg" });
            restaurants[5].Photos.Add(new PhotoFile { FileName = "Photo", Path = "~/dbInitialize/Hasadna/1.jpg" });
            restaurants[5].Photos.Add(new PhotoFile { FileName = "Photo", Path = "~/dbInitialize/Hasadna/2.jpg" });
            restaurants[5].Photos.Add(new PhotoFile { FileName = "Photo", Path = "~/dbInitialize/Hasadna/3.jpg" });
            restaurants[5].Photos.Add(new PhotoFile { FileName = "Photo", Path = "~/dbInitialize/Hasadna/4.jpg" });
            restaurants[6].Photos.Add(new PhotoFile { FileName = "Photo", Path = "~/dbInitialize/The Palomar/1.jpg" });
            restaurants[6].Photos.Add(new PhotoFile { FileName = "Photo", Path = "~/dbInitialize/The Palomar/2.jpg" });
            restaurants[6].Photos.Add(new PhotoFile { FileName = "Photo", Path = "~/dbInitialize/The Palomar/3.jpg" });
            restaurants[6].Photos.Add(new PhotoFile { FileName = "Photo", Path = "~/dbInitialize/The Palomar/4.jpg" });
            restaurants[7].Photos.Add(new PhotoFile { FileName = "Photo", Path = "~/dbInitialize/Shabour/1.jpg" });
            restaurants[7].Photos.Add(new PhotoFile { FileName = "Photo", Path = "~/dbInitialize/Shabour/2.jpg" });
            restaurants[7].Photos.Add(new PhotoFile { FileName = "Photo", Path = "~/dbInitialize/Shabour/3.jpg" });
            restaurants[7].Photos.Add(new PhotoFile { FileName = "Photo", Path = "~/dbInitialize/Shabour/4.jpg" });

            //set chefs portraits
            chefs[0].Portrait = new PortraitFile { FileName = "Portrait", Path = "~/dbInitialize/Chefs/IsraelAharoni.jpg" };
            chefs[1].Portrait = new PortraitFile { FileName = "Portrait", Path = "~/dbInitialize/Chefs/EyalShani.jpg" };
            chefs[2].Portrait = new PortraitFile { FileName = "Portrait", Path = "~/dbInitialize/Chefs/MeirAdoni.jpg" };
            chefs[3].Portrait = new PortraitFile { FileName = "Portrait", Path = "~/dbInitialize/Chefs/AssafGranit.jpg" };
            chefs[4].Portrait = new PortraitFile { FileName = "Portrait", Path = "~/dbInitialize/Chefs/MoshikRoth.jpg" };
                                                                                                        
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

