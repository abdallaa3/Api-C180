using Microsoft.EntityFrameworkCore;

namespace Day4tackC180.models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Product> Products { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }

}
