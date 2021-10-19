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
	public class DayHours 
	{
		public int DayHoursID { get; set; }
		public string DayOfWeek { get; set; }
        public bool Open { get; set; }
        public DateTime From { get; set; }
		public DateTime To { get; set; }
	}
}
