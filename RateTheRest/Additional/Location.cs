using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using RateTheRest.Models;

namespace RateTheRest.Additional
{
    public class Location
    {
        [ForeignKey("Restaurant")]
        public int LocationId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Number")]
        public int Number { get; set; }

        //Relations

        //One(Restaurant)-to-One(Location)
        public virtual Restaurant Restaurant { get; set; }              
    }
}
