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
        public static readonly string[] TAGS = { "Kosher", "Vegan", "Meat", "Dairy" };
    }

    public class Tag
    {
        public int TagID { get; set; }
        public string TagName { get; set; }

        //Linked tables from db

        public Restaurant Restaurant { get; set; }              //One(Restaurant)-to-Many(Tags)
    }
}
