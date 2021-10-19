using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using RateTheRest.Models;

namespace RateTheRest.Additional
{
    public class Tag
    {
        public int TagID { get; set; }
        public string TagName { get; set; }

        //Linked tables from db
        public ICollection<Restaurant> Restaurants { get; set; }
    }
}
