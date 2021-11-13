using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using RateTheRest.Additional;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace RateTheRest.Models
{
	public class Restaurant
	{
		public int RestaurantID { get; set; }

		[Required]
		public string Name { get; set; }

		public string? Description { get; set; }

		//Relations

		//One(Restaurant)-to-One(Location)
		[ValidateNever]
		public virtual Location Location { get; set; }

        //One(Restaurant)-to-Many(OpeningHours)
        [ValidateNever]
        [Display(Name = "Opening Hours")]
        public ICollection<OpeningHours> OpeningHours { get; set; } = new List<OpeningHours>();

		//One(Restaurant)-to-Many(Tags)
		[ValidateNever]
		public ICollection<Tag> Tags { get; set; } = new List<Tag>();

        //One(Restaurant)-to-One(Rating)
        [ValidateNever]
        public virtual Rating Rating { get; set; } = new Rating() { Score = 0, Users = new List<ApplicationUser>() };

        //One(Restaurant)-to-Many(Reviews)
        [ValidateNever]
		public ICollection<Review> Reviews { get; set; } = new List<Review>();

		//One(Restaurant)-to-One(Logo)
		[ValidateNever]
		public LogoFile? Logo { get; set; } = new LogoFile { FileName = "Restaurant_Empty_Logo.png", Path = "~/images/Restaurant_Empty_Logo.png" };

		//One(Restaurant)-to-Many(Images)
		[ValidateNever]
		public ICollection<PhotoFile>? Photos { get; set; } = new List<PhotoFile>();

		//Many(Restaurants)-to-Many(Chefs)
		[ValidateNever]
		public virtual ICollection<Chef>? Chefs { get; set; } = new HashSet<Chef>();
    }
}