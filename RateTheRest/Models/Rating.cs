using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RateTheRest.Models
{
    //The Rating score of a restaurant calculated from all the restaurant's reviewes
    public class Rating
    {
        [ForeignKey("Restaurant")]
        public int RatingID { get; set; }

        public float Value                     //The calculated value of the restaurant's ratings SumOfVotes/NumOfVotes
        {
            get
            {
                if (Restaurant.Reviews == null || Users == null) return 0;
                if (Restaurant.Reviews.Count == 0 || Users.Count == 0) return 0;
                return Restaurant.Reviews.Sum(r => r.Score) / Users.Count;
            }
        }

        //Relations

        public virtual Restaurant Restaurant { get; set; }                                              //One(Restaurant)-to-One(Rating)

        public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();        //The Users who voted          //One(Rating)-to-Many(Users)
    }
}
