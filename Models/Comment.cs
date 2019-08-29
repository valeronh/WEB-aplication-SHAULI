using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }
        public int PostID { get; set; }
        public string Title { get; set; }
        public string CommentAuthor { get; set; }
        public string AuthorsWebsite { get; set; }
        public string Context { get; set; }
        public virtual Post CommentPost { get; set; }
        public DateTime CommentDate { get; set; }
    }
}