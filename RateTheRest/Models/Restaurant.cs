using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using RateTheRest.Additional;

namespace RateTheRest.Models
{
    public class Restaurant
    {
        public int RestaurantID { get; set; }

        public string Name { get; set; }

        public string? Logo { get; set; }               //Optional restaurant's logo image location

        public string Description { get; set; }

        public string Location { get; set; }

        [Display(Name = "Opening Hours")]
        public string OpeningHours { get; set; }

        public string Category { get; set; }            //Fast Food, Italian, etc

        public List<FilePath> Tags { get; set; }          //Vegan, Kosher, etc

        public List<FilePath> Photos { get; set; }        //Restaurant's images locations

        //Linked fields from db
        public int RatingID { get; set; }
        public Rating Rating { get; set; }

        //Linked tables from db
        public ICollection<Chef> Chefs { get; set; }
        public ICollection<Review> Reviews { get; set; }

    }
}
