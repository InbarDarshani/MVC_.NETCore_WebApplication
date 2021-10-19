using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateTheRest.Models
{
    //A Review of a restaurante created by user 
    public class Review
    {
        public int ReviewID { get; set; }

        public float Score { get; set; }            //An Individual rating score by this user

        public string? text { get; set; }           //Optional description of the review

        public DateTime DateCreated { get; set; }

        //Linked fields from db
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
