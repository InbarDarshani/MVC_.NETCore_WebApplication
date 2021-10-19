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

        [ValidateNever]
        public Location Location { get; set; }

        [Display(Name = "Opening Hours")]
        [ValidateNever]
        public ICollection<DayHours>? OpeningHours { get; set; }

        [ValidateNever]
        public List<Tag>? Tags { get; set; }                 //Vegan, Kosher, etc

        [ValidateNever]
        public ImageFile? Logo { get; set; }               //Restaurant's logo image paths

        [ValidateNever]
        public List<ImageFile>? Photos { get; set; }         //Restaurant's images paths

        //Linked fields from db
        [Display(Name = "Rating Score")]
        public int RatingID { get; set; }       
        public Rating Rating { get; set; }

        //Linked tables from db
        public ICollection<Chef> Chefs { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}