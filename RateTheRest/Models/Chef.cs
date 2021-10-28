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

        [Display(Name = "Average Rating Of Restaurants")]
        public float? AvgRate { get; set; }                                //The chef's restaurants average rating score          

        //Linked tables from db

        [ValidateNever]
        public PortraitFile? Portrait { get; set; }                                     //One(Chef)-to-One(Portrait)

        [ValidateNever]
        public virtual ICollection<Restaurant>? Restaurants { get; set; }               //Many(Restaurants)-to-Many(Chefs)
    }
}
