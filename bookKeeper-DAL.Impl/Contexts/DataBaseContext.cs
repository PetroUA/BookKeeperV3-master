using bookKeeper_Entity;
using Microsoft.EntityFrameworkCore;

namespace bookKeeper_DAL.Impl.Contexts
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}