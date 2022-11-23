using Microsoft.EntityFrameworkCore;

namespace UserMvc.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> users { get; set; }
      
    }
}
