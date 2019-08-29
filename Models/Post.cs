using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Post
    {
        [Key]
        public int PostID { get; set; }
        public string Headline { get; set; }
        public string AuthorsName { get; set; }
        public string AuthorsWebsite { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Context { get; set; }
        public bool IfImage { get; set; }
        public string Image { get; set; }
        public bool IfVideo { get; set; }
        public string Video { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}