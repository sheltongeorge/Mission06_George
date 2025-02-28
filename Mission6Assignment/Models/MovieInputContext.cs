using Microsoft.EntityFrameworkCore;
using Mission06_George.Models;

namespace Mission6Assignment.Models
{
    public class MovieInputContext : DbContext
    {
        public MovieInputContext(DbContextOptions<MovieInputContext> options) : base (options) { } // constructor, called one time
        
        public DbSet<MovieApplication> Movies { get; set; } // name of the table in the databse

        public DbSet<Categories> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) // This may have been obsolete below because the table already had values
        {
            modelBuilder.Entity<Categories>().HasData(
                new Categories { CategoryId = 1, CategoryName = "Miscellaneous" },
                new Categories { CategoryId = 2, CategoryName = "Drama" },
                new Categories { CategoryId = 3, CategoryName = "Television" },
                new Categories { CategoryId = 4, CategoryName = "Horror/Suspense" },
                new Categories { CategoryId = 5, CategoryName = "Comedy" },
                new Categories { CategoryId = 6, CategoryName = "Family" },
                new Categories { CategoryId = 7, CategoryName = "Action/Adventure" },
                new Categories { CategoryId = 8, CategoryName = "VHS" }
                );
        }
    }
}
