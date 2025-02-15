using Microsoft.EntityFrameworkCore;

namespace Mission06_Stephenson.Models
{
    public class MoviesContext : DbContext
    {
        public MoviesContext(DbContextOptions<MoviesContext> options) : base(options)
        {
            
        }
        
        public DbSet<Movie> Movies { get; set; }
    }
}

