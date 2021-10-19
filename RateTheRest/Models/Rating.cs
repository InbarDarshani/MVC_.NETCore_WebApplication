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
        public int RatingID { get; set; }

        public float Value { get; set; }            //The calculated value of the restaurant's ratings 

        public int NumOfVotes { get; set; }         //The number of users who voted (Number of Users in the table below)

        //Linked fields from db
        [ForeignKey("Restaurant")]                  //The Rating is the dependant entity in the One-To-One relationship
        public int RestaurantID { get; set; }
        public Restaurant Restaurant { get; set; }

        //Linked tables from db
        public ICollection<User> User { get; set; }     //The Users who voted
    }
}
