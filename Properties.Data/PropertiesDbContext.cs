using Microsoft.EntityFrameworkCore;
using Properties.Data.Entities;

namespace Properties.Data
{
    public class PropertiesDbContext : DbContext
    {
        public PropertiesDbContext(DbContextOptions<PropertiesDbContext> options) : base(options)
        {

        }


        public DbSet<User> Users { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<PropertyTrace> PropertyTraces { get; set; }
    }
}
