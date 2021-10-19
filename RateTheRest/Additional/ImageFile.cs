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
    //Restaurant's Logo and Images table
    public class ImageFile
    {
        public int ImageFileID { get; set; }
        public string ImageType { get; set; }   //Logo, Photo or Chef's
        public string Path { get; set; }                                   
        public string FileName{ get; set; }
    }
}
