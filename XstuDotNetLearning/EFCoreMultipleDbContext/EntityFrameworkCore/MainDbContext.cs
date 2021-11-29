using EFCoreMultipleDbContext.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreMultipleDbContext.EntityFrameworkCore
{
    public class MainDbContext : DbContext
    {
        public DbSet<ApplicationInfo> ApplicationInfo { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {

        }
    }
}
