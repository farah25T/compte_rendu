using Microsoft.EntityFrameworkCore;

namespace compte_rendu.Models
{
    public class ApplicationdbContext : DbContext
    {
        public ApplicationdbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Movie> Movies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Membershiptypes> Memberships { get; set; }
        public DbSet<Genre> Genre { get; set; }
    }

}
