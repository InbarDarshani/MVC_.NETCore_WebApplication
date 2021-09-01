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

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Photo { get; set; }               //Chef's image location

        public string Description { get; set; }

        public float AvgRate { get; set; }              //The chef's restaurants average rating score

        //Linked tables from db
        public ICollection<Restaurant> Restaurants { get; set; }
    }
}
