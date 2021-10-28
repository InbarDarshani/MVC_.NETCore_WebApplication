using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using RateTheRest.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RateTheRest.Additional
{
	public class LogoFile
	{
		[ForeignKey("Restaurant")]
		public int LogoFileID { get; set; }
		public string Path { get; set; }            //examples: ~\images\Restaurants\_logo_24.10.2021_13.24.46.png            
		public string FileName { get; set; }        //examples: _logo_20.10.2021_02.12.12.png 

		//Linked tables from db
		
		public virtual Restaurant Restaurant { get; set; }             //One(Restaurant)-to-One(Logo)
	}

	public class ImageFile
	{
		public int ImageFileID { get; set; }
		public string Path { get; set; }            //examples: ~\images\Restaurants\_photo_20.10.2021_02.12.13_2.png            
		public string FileName { get; set; }        //examples:  _photo_20.10.2021_02.12.13_2.png

		//Linked tables from db

		public Restaurant Restaurant { get; set; }				//One(Restaurant)-to-Many(Images)
	}

	public class PortraitFile
	{
		[ForeignKey("Chef")]
		public int PortraitFileID { get; set; }
		public string Path { get; set; }            //examples: ~\images\Restaurants\_photo_20.10.2021_02.12.13_2.png            
		public string FileName { get; set; }        //examples:  _photo_20.10.2021_02.12.13_2.png

		//Linked tables from db

		public virtual Chef Chef { get; set; }                     //One(Chef)-to-One(Portrait)
	}

}
