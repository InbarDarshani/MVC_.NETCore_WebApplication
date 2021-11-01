using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RateTheRest.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using RateTheRest.Additional;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace RateTheRest.Data
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public readonly IWebHostEnvironment HostingEnvironment;
        public IConfigurationRoot Configuration { get; set; }

        //public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        //{
        //}

        public ApplicationContext(IWebHostEnvironment environment) : base()
        {
            this.HostingEnvironment = environment;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Setup and Build a custom DbContexOptions in order to specify local hosting environment
            var builder = new ConfigurationBuilder()
                .SetBasePath(HostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Chef> Chefs { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<LogoFile> Logos { get; set; }
        public DbSet<PhotoFile> Photos { get; set; }
        public DbSet<PortraitFile> Portraits { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<OpeningHours> OpeningHours { get; set; }
    }
}
