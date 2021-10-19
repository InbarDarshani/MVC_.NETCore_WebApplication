using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RateTheRest.Additional
{
    public class Location
    {
        public int LocationId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Number")]
        public int Number { get; set; }

        public string? Description { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
