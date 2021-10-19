using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RateTheRest.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using RateTheRest.Additional;

namespace RateTheRest.Data
{
    public class ApplicationContext : DbContext
    {
        public readonly IWebHostEnvironment env;
        public IConfigurationRoot Configuration { get; set; }

        public ApplicationContext(IWebHostEnvironment env) : base()
        {
            this.env = env;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Chef> Chefs { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ImageFile> Images { get; set; }
        public DbSet<Location> Locations { get; set; }
    }
}
