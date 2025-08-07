using Microsoft.EntityFrameworkCore;
using ANKETUYGULAMASI.Models;

namespace ANKETUYGULAMASI
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ManagerUser> ManagerUsers { get; set; }
        public DbSet<ManagerUserLog> ManagerUserLogs { get; set; }
    }
}
