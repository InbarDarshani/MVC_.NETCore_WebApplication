using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using RateTheRest.Models;

namespace RateTheRest.Additional
{
    public static class GlobalTags
    {
        public static readonly string[] TAGS = { "Kosher", "Meat", "Dairy", "Vegan" };
    }

    public class Tag
    {
        public int TagID { get; set; }
        public string TagName { get; set; }

        //Relations

        //One(Restaurant)-to-Many(Tags)
        public Restaurant Restaurant { get; set; }              
    }
}
