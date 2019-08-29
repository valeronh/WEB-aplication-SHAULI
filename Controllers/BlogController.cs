using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class BlogController : Controller
    {
        private PostCommentsContext db = new PostCommentsContext();

        // GET: Blog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }


        /*************************************FILTER POSTS************************************************************/
        public ActionResult Index(String date, String author, String keyword)
        {
            var result = from p in db.Posts
                select p;

            if (!String.IsNullOrEmpty(date))
            {
                DateTime converted = DateTime.Parse(date);
                result = from p in result
                    where p.PublicationDate.Year == converted.Year
                          && p.PublicationDate.Month == converted.Month
                          && p.PublicationDate.Day == converted.Day
                    select p;
            }

            //filter by author
            if (!String.IsNullOrEmpty(author))
            {
                result = result.Where(p => p.AuthorsName.ToUpper().Contains(author.ToUpper()));
            }

/************************************JOIN QUERRY - SHOWING POSTS THAT CONTAIN THE KEYWORDS IN THE COMMNETS(headline and context)********************************** */
            if (!String.IsNullOrEmpty(keyword))
            {
                return(View("index", (from a in db.Posts
                    join b in db.Comments
                    on a.PostID equals b.PostID
                    where (b.Title.Contains(keyword)
                    || b.Context.Contains(keyword))
                    select a).ToList()));
            }

            //            ViewBag.Current = "postsManager";
            return View("index", result.ToList());
        }

        // GET: Blog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Blog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostID,Headline,AuthorsName,AuthorsWebsite,Context,IfImage,Image,IfVideo,Video")] Post post)
        {
            post.PublicationDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Blog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Blog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostID,Headline,AuthorsName,AuthorsWebsite,Context,IfImage,Image,IfVideo,Video")] Post post)
        {
            post.PublicationDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Blog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment([Bind(Include = "CommentID,PostID,Title,CommentAuthor,AuthorsWebsite,Context")] Comment comment)
        {
            comment.CommentDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PostID = new SelectList(db.Posts, "PostID", "Headline", comment.PostID);
            return View(comment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
