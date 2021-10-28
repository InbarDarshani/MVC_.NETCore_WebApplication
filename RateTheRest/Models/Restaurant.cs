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
		public Restaurant() { this.Chefs = new HashSet<Chef>(); }     //For the Many to Many Relation

		public int RestaurantID { get; set; }

		[Required]
		public string Name { get; set; }

		public string? Description { get; set; }

		//Linked tables from db

		[ValidateNever]
		public virtual Location Location { get; set; }                                                      //One(Restaurant)-to-One(Location)

		[ValidateNever]
		[Display(Name = "Opening Hours")]
		public ICollection<OpeningHours>? OpeningHours { get; set; }                                        //One(Restaurant)-to-Many(OpeningHours)

		[ValidateNever]
		public ICollection<Tag>? Tags { get; set; }                 //Vegan, Kosher, etc					//One(Restaurant)-to-Many(Tags)

		[ValidateNever]
		[Display(Name = "Rating Score")]   
		public virtual Rating? Rating { get; set; }                                                         //One(Restaurant)-to-One(Rating)

		[ValidateNever]
		public ICollection<Review>? Reviews { get; set; }                                                   //One(Restaurant)-to-Many(Reviews)

		[ValidateNever]
		public LogoFile? Logo { get; set; }								//Restaurant's logo image paths     //One(Restaurant)-to-One(Logo)

		[ValidateNever]
		public ICollection<ImageFile>? Photos { get; set; }             //Restaurant's images paths         //One(Restaurant)-to-Many(Images)

		[ValidateNever]
		public virtual ICollection<Chef>? Chefs { get; set; }                                                //Many(Restaurants)-to-Many(Chefs)
	}
}