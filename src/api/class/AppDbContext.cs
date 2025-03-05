using Microsoft.EntityFrameworkCore;

namespace ERPBridge.Class
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}