using Instiute01.Models;
using Microsoft.EntityFrameworkCore;

namespace Instiute01.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base (options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define the many-to-many relationship between Teacher and Course
            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.Courses)
                .WithMany(c => c.Teachers);

            // Define the many-to-many relationship between Student and Batch
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Batches)
                .WithMany(b => b.Students);

            base.OnModelCreating(modelBuilder);
        }
    }
}