using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        //The calculated value of the restaurant's ratings SumOfVotes/NumOfVotes
        [DefaultValue("calcScore()")]
        public float Score { get; set; }

        //Relations

        //One(Restaurant)-to-One(Rating)
        public virtual Restaurant Restaurant { get; set; }

        //The Users who voted
        //One(Rating)-to-Many(Users)
        public ICollection<ApplicationUser> Users { get; set; }

        public float calcScore()
        {
            if (Restaurant.Reviews == null) return 0;
            if (Restaurant.Reviews.Count == 0) return 0;
            return Restaurant.Reviews.Sum(r => r.Score) / Restaurant.Reviews.Count;
        }
    }
}
