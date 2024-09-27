using DevaLokaVillaAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DevaLokaVillaAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

       
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<LocalUser> LocalUsers { get; set; }
        public DbSet<Villa> Villas { get; set; }
        public DbSet<VillaNumber> VillaNumbers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Villa>().HasData(
            new Villa
            {
                Id=1,
                Name="Royal Villa",
                Details= "Royal villa with private spa",
                ImageUrl= "https://placehold.co/",
                Occupancy=1,
                Rate=111,
                Sqft=1111,
                Amenity="",
                CreatedDate= DateTime.Now

            },
            new Villa
            {
                Id = 2,
                Name = "Premium Villa",
                Details = "Premium villa with private pool",
                ImageUrl = "https://placehold.co/",
                Occupancy = 2,
                Rate = 222,
                Sqft = 2222,
                Amenity = "",
                CreatedDate = DateTime.Now
            },
            new Villa
            {
                Id = 3,
                Name = "Luxury Villa",
                Details = "Luxury villa with private and Spa pool",
                ImageUrl = "https://placehold.co/",
                Occupancy = 3,
                Rate = 333,
                Sqft = 33333,
                Amenity = "",
                CreatedDate = DateTime.Now
            },
            new Villa
            {
                 Id = 4,
                 Name = "Presidential Villa",
                 Details = "Presidential villa with private and spa pool",
                 ImageUrl = "https://placehold.co/",
                 Occupancy = 4,
                 Rate = 444,
                 Sqft = 4444,
                 Amenity = "",
                CreatedDate = DateTime.Now
            });

        }
    }
}
