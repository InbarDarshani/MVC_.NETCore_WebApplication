﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RateTheRest.Models
{
    //A Review of a restaurant submittied by a user 
    public class Review
    {
        public int ReviewID { get; set; }

        [Display(Name = "Restaurant Score")]
        public int Score { get; set; }              //An Individual rating score by this user

        [Display(Name = "Short Description")]
        public string? Text { get; set; }           //Optional description of the review

        public DateTime DateCreated { get; set; }

        //Relations

        //One(Restaurant)-to-Many(Reviews)
        public Restaurant Restaurant { get; set; }

        //One(User)-to-Many(Reviews)
        public ApplicationUser User { get; set; }                          
    }
}
