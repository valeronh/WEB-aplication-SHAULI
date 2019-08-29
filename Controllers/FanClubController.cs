using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using Accord.Controls;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Math.Optimization.Losses;
using Accord.Statistics;
using Accord.Statistics.Kernels;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning;

namespace WebApplication1.Controllers
{
    public class FanClubController : Controller
    {
        private FansClubDbContext db = new FansClubDbContext();

        // GET: FanClub
        public ActionResult Index()
        {
            return View(db.Fans.ToList());
        }

        // GET: FanClub/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FanClub fanClub = db.Fans.Find(id);
            if (fanClub == null)
            {
                return HttpNotFound();
            }
            return View(fanClub);
        }

        // GET: FanClub/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FanClub/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FanID,FirstName,FamilyName,Gender,age,loveDicaprio,loveRedColor,loveActionMovies,loveBarMitzvah,loveStrawberries,loveCoffee,loveIrena")] FanClub fanClub)
        {
            if (ModelState.IsValid)
            {
                db.Fans.Add(fanClub);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fanClub);
        }

        // GET: FanClub/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FanClub fanClub = db.Fans.Find(id);
            if (fanClub == null)
            {
                return HttpNotFound();
            }
            return View(fanClub);
        }

        // POST: FanClub/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FanID,FirstName,FamilyName,Gender,age,loveDicaprio,loveRedColor,loveActionMovies,loveBarMitzvah,loveStrawberries,loveCoffee,loveIrena")] FanClub fanClub)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fanClub).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fanClub);
        }

        // GET: FanClub/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FanClub fanClub = db.Fans.Find(id);
            if (fanClub == null)
            {
                return HttpNotFound();
            }
            return View(fanClub);
        }

        /*****************************************GROUP BY QURRIES - SHOWS STATISCTICS OVER FANS*****************************************************/
        // GET: FanClub/Delete/5
        public ActionResult Stats()
        {

            Dictionary<String,int> outputs = new Dictionary<String, int>();
            var result = (from c in db.Fans
                          where c.loveDicaprio.ToString().Equals(Boolean.TrueString)
                group c by c.loveDicaprio
                into g
                select new {
                    key = g.Key,
                    count = g.Count()
            }).ToList();

            
            foreach (var m in result)
            {
                outputs["loveDicaprio"] = m.count;
            }

            var result2 = (from c in db.Fans
                where c.loveRedColor.ToString().Equals(Boolean.TrueString)
                group c by c.loveRedColor
                into g
                select new
                {
                    key = g.Key,
                    count = g.Count()
                }).ToList();


            foreach (var m in result2)
            {
                outputs["loveRedColor"] = m.count;
            }

            var result3 = (from c in db.Fans
                where c.loveActionMovies.ToString().Equals(Boolean.TrueString)
                group c by c.loveActionMovies
                into g
                select new
                {
                    key = g.Key,
                    count = g.Count()
                }).ToList();


            foreach (var m in result3)
            {
                outputs["loveActionMovies"] = m.count;
            }

            var result4 = (from c in db.Fans
                where c.loveBarMitzvah.ToString().Equals(Boolean.TrueString)
                group c by c.loveBarMitzvah
                into g
                select new
                {
                    key = g.Key,
                    count = g.Count()
                }).ToList();


            foreach (var m in result4)
            {
                outputs["loveBarMitzvah"] = m.count;
            }

            var result5 = (from c in db.Fans
                where c.loveStrawberries.ToString().Equals(Boolean.TrueString)
                group c by c.loveStrawberries
                into g
                select new
                {
                    key = g.Key,
                    count = g.Count()
                }).ToList();


            foreach (var m in result5)
            {
                outputs["loveStrawberries"] = m.count;
            }

            var result6 = (from c in db.Fans
                where c.loveCoffee.ToString().Equals(Boolean.TrueString)
                group c by c.loveCoffee
                into g
                select new
                {
                    key = g.Key,
                    count = g.Count()
                }).ToList();


            foreach (var m in result6)
            {
                outputs["loveCoffee"] = m.count;
            }

            var result7 = (from c in db.Fans
                where c.loveIrena.ToString().Equals(Boolean.TrueString)
                group c by c.loveIrena
                into g
                select new
                {
                    key = g.Key,
                    count = g.Count()
                }).ToList();


            foreach (var m in result7)
            {
                outputs["loveIrena"] = m.count;
            }



            return View(outputs);
        }

        // POST: FanClub/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FanClub fanClub = db.Fans.Find(id);
            db.Fans.Remove(fanClub);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        /******************************LOVE CALCULATOR***********************************************/
        public ActionResult CalculateLove(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FanClub fanClub = db.Fans.Find(id);
            if (fanClub == null)
            {
                return HttpNotFound();
            }
            return View(CalculateSVM(fanClub));
        }

        /*****************************MACHINE LEARNING ALGORYTHM********************************************/
        public bool CalculateSVM(FanClub f)
        {

            //age,loveDicaprio,loveRedColor,loveActionMovies,loveBarMitzvah,loveStrawberries,loveCoffee,loveIrena
            double[][] vectors = {
                new double[] { 26, 1, 1, 1, 0, 0, 1, 1}, // Tal
                new double[] { 32, 0, 1, 1, 0, 1, 1, 0}, // Or
                new double[] { 21, 0, 0, 1, 0, 1, 0, 0}, // Hila
                new double[] { 24, 0, 1, 0, 0, 1, 0, 0}, // Efrat
                new double[] { 23, 1, 0, 1, 0, 0, 0, 0}, // Farjun
                new double[] { 27, 0, 0, 1, 1, 0, 0, 0}, // Guy
                new double[] { 28, 1, 0, 1, 0, 1, 1, 1}, // Valery
                
            };

            int[] true_false_map = {
                1,
                0,
                0,
                0,
                0,
                1,
                0
            };

            // Learning Algorithm (Hard Margin SVM)
            var svm = new SequentialMinimalOptimization<Gaussian>(){ Complexity = 100 };

            // Learning the Tree
            var result = svm.Learn(vectors, true_false_map);

            double[] input = {
                f.age,
                System.Convert.ToDouble(f.loveDicaprio),
                System.Convert.ToDouble(f.loveRedColor),
                System.Convert.ToDouble(f.loveActionMovies),
                System.Convert.ToDouble(f.loveBarMitzvah),
                System.Convert.ToDouble(f.loveStrawberries),
                System.Convert.ToDouble(f.loveCoffee),
                System.Convert.ToDouble(f.loveIrena)
            };

            bool[] prediction = { result.Decide(input) };

            return(prediction[0]);
        }
    }
}
