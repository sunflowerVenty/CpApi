using Microsoft.EntityFrameworkCore;
using CpApi.Model;

namespace CpApi.DataBaseContext
{
    public class ContextDb : DbContext
    {
        public ContextDb(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Logins> Logins { get; set; }
        public DbSet<Films> Films { get; set; }
        public DbSet<Genres> Genres { get; set; }
    }
}
