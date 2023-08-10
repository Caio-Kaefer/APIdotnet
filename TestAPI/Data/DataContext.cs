using Microsoft.EntityFrameworkCore;

namespace TestAPI.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options) { }
        public DbSet<SuperHero> Superheroes { get; set; }

    }
}
