using Microsoft.EntityFrameworkCore;

namespace Mission6Assignment.Models
{
    public class MovieInputContext : DbContext
    {
        public MovieInputContext(DbContextOptions<MovieInputContext> options) : base (options) { }
        
        public DbSet<MovieApplication> MovieList { get; set; } // name of the table in the databse
    }
}
