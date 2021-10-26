using Microsoft.AspNetCore.Mvc.ModelBinding;
using RateTheRest.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RateTheRest.Additional
{
	public static class GlobalWeek
	{
		public static readonly string[] WEEK = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
	}

	public class OpeningHours //Per a day of the week
	{
		public int OpeningHoursID { get; set; }
		public string DayOfWeek { get; set; }
        public bool Open { get; set; }
        public DateTime? From { get; set; }
		public DateTime? To { get; set; }

		//Linked tables from db

		public Restaurant Restaurant { get; set; }              //One(Restaurant)-to-Many(OpeningHours)
	}
}
