using Microsoft.EntityFrameworkCore;
using WEBTEST_API_PROYECT.Models;

namespace WEBTEST_API_PROYECT.Data
{
    public class ApplicationDbContext: DbContext
    {
   

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
        {

        }

        public DbSet<TestWeb> TestWebs { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestWeb>().HasData(
                    
                new TestWeb()
                {
                    Id = 1,
                    Name = "Villa real",
                    Detail = "Detalle de la villa",
                    ImageUrl = "",
                    Pages = 5,
                    SquareMeters = 200,
                    Fee = 500,
                    Amenity = "",
                    DateCreation = DateTime.Now,
                    DateUpdating = DateTime.Now

                },

                new TestWeb()
                {
                    Id = 2,
                    Name = "Sunny Shores",
                    Detail = "A sunny vacation spot by the beach",
                    ImageUrl = "https://example.com/sunny-shores.jpg",
                    Pages = 10,
                    SquareMeters = 400,
                    Fee = 800,
                    Amenity = "Swimming pool, beach access",
                    DateCreation = DateTime.UtcNow,
                    DateUpdating = DateTime.UtcNow
                }

            );
        }
    }
}
