using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RateTheRest.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int AppUserId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; } 

        //Linked tables from db

        [ValidateNever]
        public ICollection<Review>? Reviews { get; set; }                  //One(User)-to-Many(Review)

        [ValidateNever]
        public Rating? Rating { get; set; }                              //One(Rating)-to-Many(Users)
    }



}
