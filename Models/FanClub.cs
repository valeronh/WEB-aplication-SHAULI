using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class FanClub
    {
        [Key]
        public int FanID { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string Gender { get; set; }
        public int age { get; set; }
        public bool loveDicaprio { get; set; }
        public bool loveRedColor { get; set; }
        public bool loveActionMovies { get; set; }
        public bool loveBarMitzvah { get; set; }
        public bool loveStrawberries { get; set; }
        public bool loveCoffee { get; set; }
        public bool loveIrena { get; set; }

//        public bool doesUserLoveShauli { get; set; }

    }

    public class FansClubDbContext : DbContext
    {
        public DbSet<FanClub> Fans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<FansClubDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}