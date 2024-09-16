using Microsoft.EntityFrameworkCore;
using StudentApi.Models.Entity;

namespace StudentApi.Infrastructure.Data
{
    public class StudentDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
