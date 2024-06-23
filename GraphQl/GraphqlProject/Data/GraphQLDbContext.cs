using GraphqlProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphqlProject.Data
{
    public class GraphQLDbContext : DbContext
    {
        public GraphQLDbContext(DbContextOptions<GraphQLDbContext> options) : base(options) { }

        public DbSet<Menu> Menus { get; set; }

        //one-one relatin with menu
        public DbSet<Category> Categories { get; set; }
        //one to many relation with menu
        public DbSet<Reservation> Reservations { get; set; }

        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("Categories");  // Ensure this matches your database table name
        }
        */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Menu>()
                .HasOne(m => m.Category)
                .WithMany(c => c.Menus)
                .HasForeignKey(m => m.CategoryId);

            modelBuilder.Entity<Menu>()
                .HasMany(m => m.Reservations)
                .WithOne(r => r.Menu)
                .HasForeignKey(r => r.MenuId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
