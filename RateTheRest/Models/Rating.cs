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

        public float Value { get; set; }            //The calculated value of the restaurant's ratings NumOfVotes\SumOfVotes

        public int NumOfVotes { get; set; }         //The number of users who voted (Number of Users in the table below)

        public int SumOfVotes { get; set; }

        //Linked tables from db

        public virtual Restaurant Restaurant { get; set; }                                  //One(Restaurant)-to-One(Rating)
        
        public ICollection<ApplicationUser> User { get; set; }         //The Users who voted          //One(Rating)-to-Many(Users)
    }
}
