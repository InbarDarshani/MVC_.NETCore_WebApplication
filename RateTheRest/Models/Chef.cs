using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using RateTheRest.Additional;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RateTheRest.Models
{
    public class Chef
    {
        public Chef() { this.Restaurants = new HashSet<Restaurant>(); }     //For the Many to Many Relation

        public int ChefID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string? Description { get; set; }

        //The chef's restaurants average rating score:  Sum Rating Values / Num Of Restaurants
        [Display(Name = "Average Rating Of Restaurants")]
        public float? AvgRate
        {
            get
            {
                if (Restaurants == null) return 0;
                if (Restaurants.ToList().Count == 0) return 0;
                return Restaurants.Sum(r => r.Rating.Score) / Restaurants.ToList().Count;
            }
        }

        //Relations

        //One(Chef)-to-One(Portrait)
        [ValidateNever]
        public PortraitFile? Portrait { get; set; } = new PortraitFile { FileName = "Chef_Empty_Logo.png", Path = "~/images/Chef_Empty_Logo.png" };

        //Many(Restaurants)-to-Many(Chefs)
        [ValidateNever]
        public virtual ICollection<Restaurant>? Restaurants { get; set; }
    }
}
