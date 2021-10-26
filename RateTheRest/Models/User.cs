using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RateTheRest.Models
{
    public static class GlobalUserRoles
    {
        public static readonly string[] ROLES = { "Admin", "Basic" };
    }

    public class User
    {
        public int UserId { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; } 
        
        public string Role { get; set; }

        //Linked tables from db

        [ValidateNever]
        public ICollection<Review>? Reviews { get; set; }                  //One(User)-to-Many(Review)

        [ValidateNever]
        public Rating? Rating { get; set; }                              //One(Rating)-to-Many(Users)
    }
}
