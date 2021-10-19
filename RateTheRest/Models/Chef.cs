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
        public int ChefID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        public string Description { get; set; }

        public float AvgRate { get; set; }              //The chef's restaurants average rating score

        [Required]
        [ValidateNever]
        public ImageFile? Photo { get; set; }               //Chef's image location

        //Linked tables from db
        public ICollection<Restaurant> Restaurants { get; set; }
    }
}
