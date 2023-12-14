using APITask.Entites;
using Microsoft.EntityFrameworkCore;

namespace APITask.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
                
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Tag> Tags { get; set; }
    }
}
