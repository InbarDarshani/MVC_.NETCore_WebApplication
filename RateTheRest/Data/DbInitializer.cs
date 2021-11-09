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

            //Restaurants
            var restaurants = new Restaurant[]
            {
                //Eyal Shani
                new Restaurant { Name = "Malka",
                    Location = new Location { Country = "Israel", City = "Tel Aviv", Street = "Dafna", Number = 2 }},
                new Restaurant { Name = "Miznon",
                    Location = new Location { Country = "Israel", City = "Tel Aviv", Street = "Shlomo Ibn Gabirol", Number = 23 }},
                //Meir Adoni
                new Restaurant { Name = "Dunya",
                    Location = new Location { Country = "Israel", City = "Ashkelon", Street = "HaNamal ", Number = 9 }},
                //Assaf Granit
                new Restaurant { Name = "Machneyuda",
                    Location = new Location { Country = "Israel", City = "Jerusalem", Street = "Beit Ya'akov", Number = 10 }},
                new Restaurant { Name = "Yudale",
                    Location = new Location { Country = "Israel", City = "Jerusalem", Street = "Beit Ya'akov", Number = 11 }},
                new Restaurant { Name = "Hasadna",
                    Location = new Location { Country = "Israel", City = "Jerusalem", Street = "Hebron road", Number = 28 }},
                new Restaurant { Name = "The Palomar",
                    Location = new Location { Country = "UK", City = "London", Street = "Rupert", Number = 34 }},
                new Restaurant { Name = "Shabour",
                    Location = new Location { Country = "France", City = "Paris", Street = "Rue Saint-Sauveur", Number = 19 }}
            };
            foreach (Restaurant r in restaurants) { context.Restaurants.Add(r); }
            context.SaveChanges();

            //Chefs
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

            //Users
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
                                      ConcurrencyStamp = "262459cc-ec2b-4dab-8848-61754de853d2" },
                new ApplicationUser { FirstName = "Alona", LastName = "Tal", UserName = "Alona@gmail.com", Email = "Alona@gmail.com",
                                      NormalizedUserName="ALONA@GMAIL.COM", NormalizedEmail = "ALONA@GMAIL.COM",
                                      PasswordHash = "AQAAAAEAACcQAAAAEI0OT6ySr0Pbd5RQjf4B4FRy/5xlUXjK/mVZ8artpPHJqDwQ908ARf7UIASgbv0SYw==",
                                      SecurityStamp = "W7BRV46WRYMUGULJ5C4BWU5IM7N722DW",
                                      ConcurrencyStamp = "36476d49-263e-41f3-adbf-20e57693ca94" },
                new ApplicationUser { FirstName = "Kobi", LastName = "Farag", UserName = "Kobi@gmail.com", Email = "Kobi@gmail.com",
                                      NormalizedUserName="KOBI@GMAIL.COM", NormalizedEmail = "KOBI@GMAIL.COM",
                                      PasswordHash = "AQAAAAEAACcQAAAAEADUk9JtPqYdJIC43+0SfYwRgCdSLHkIZdaYn2ZtR58Zo6IPmVHyNMueBd5nELtTRg==",
                                      SecurityStamp = "IVTWFAIJCSAQZXHV65I2P6D5VUBW5CJO",
                                      ConcurrencyStamp = "8425a5dd-8034-41e2-9732-4e593af671e1" },
                new ApplicationUser { FirstName = "Ilan", LastName = "Rozenfeld", UserName = "Ilan@gmail.com", Email = "Ilan@gmail.com",
                                      NormalizedUserName="ILAN@GMAIL.COM", NormalizedEmail = "ILAN@GMAIL.COM",
                                      PasswordHash = "AQAAAAEAACcQAAAAEH8VBSo9eZdHi6KPCA/QaiW40n/R42rh0GvcNPlCHzfnv7tiNxNIhizUYtGPp57psQ==",
                                      SecurityStamp = "FN6VJZRRILZVDAPPJPCUCS42SES6GPYI",
                                      ConcurrencyStamp = "5926e998-8b75-448e-920a-684c3c149d82" },
                new ApplicationUser { FirstName = "Oded", LastName = "Paz", UserName = "Oded@gmail.com", Email = "Oded@gmail.com",
                                      NormalizedUserName="ODED@GMAIL.COM", NormalizedEmail = "ODED@GMAIL.COM",
                                      PasswordHash = "AQAAAAEAACcQAAAAEPscTRY6qLrbYZ3DCh0gaDXdkzdFUngjLsmAB52adnUM0/DKlbVjDgb4dtqHcPgnkQ==",
                                      SecurityStamp = "KNXZL4D5DYGTF3FEWQO22LITDBYYCMDO",
                                      ConcurrencyStamp = "42a43e46-5a1d-4751-8c9a-c2a739a208df" }
            };
            foreach (ApplicationUser u in users) { context.Users.Add(u); }
            context.SaveChanges();

            //Random values for restaurants
            foreach (Restaurant restaurant in restaurants)
            {
                var rand = new Random();

                //Create reviews with random score and random user
                for (int i = 0; i < rand.Next(20, 50); i++)
                {
                    int randomUser = rand.Next(0, users.Length);
                    createReview(new Review(), rand.Next(0, 11), restaurant.RestaurantID, users[randomUser].Email);
                }

                //Set opening hours on random days
                int randomDay = rand.Next(0, 7);
                restaurant.OpeningHours.ElementAt(randomDay).Open = true;
                restaurant.OpeningHours.ElementAt(randomDay).From = new DateTime(2021, 01, 01, 9, 0, 0);
                restaurant.OpeningHours.ElementAt(randomDay).To = new DateTime(2021, 01, 01, 22, 0, 0);
                randomDay = rand.Next(0, 7);
                restaurant.OpeningHours.ElementAt(randomDay).Open = true;
                restaurant.OpeningHours.ElementAt(randomDay).From = new DateTime(2021, 01, 01, 11, 0, 0);
                restaurant.OpeningHours.ElementAt(randomDay).To = new DateTime(2021, 01, 01, 23, 0, 0);
                randomDay = rand.Next(0, 7);
                restaurant.OpeningHours.ElementAt(randomDay).Open = true;
                restaurant.OpeningHours.ElementAt(randomDay).From = new DateTime(2021, 01, 01, 10, 0, 0);
                restaurant.OpeningHours.ElementAt(randomDay).To = new DateTime(2021, 01, 01, 22, 0, 0);
                randomDay = rand.Next(0, 7);
                restaurant.OpeningHours.ElementAt(randomDay).Open = true;
                restaurant.OpeningHours.ElementAt(randomDay).From = new DateTime(2021, 01, 01, 9, 0, 0);
                restaurant.OpeningHours.ElementAt(randomDay).To = new DateTime(2021, 01, 01, 23, 59, 0);

                //Add random tags
                ICollection<Tag> tags = new List<Tag>();
                int randomTag = rand.Next(0, 2);
                restaurant.Tags.Add(new Tag { TagName = GlobalTags.TAGS[randomTag] });
                randomTag = rand.Next(2, GlobalTags.TAGS.Length);
                restaurant.Tags.Add(new Tag { TagName = GlobalTags.TAGS[randomTag] });
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

