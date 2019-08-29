using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Place
    {
        [Key]
        public int Id { get; set; }
        public String title { get; set; }
        public String lat { get; set; }
        public String lng { get; set; }

    }
}