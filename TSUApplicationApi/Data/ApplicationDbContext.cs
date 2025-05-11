using Microsoft.EntityFrameworkCore;
using TSUApplicationApi.Entities;

namespace TSUApplicationApi.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
