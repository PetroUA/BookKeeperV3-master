using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace bookKeeper_DAL.Impl.Contexts
{
    public class MsSqlDataBaseContext : DataBaseContext
    {
        private static readonly DbContextOptions<DataBaseContext> Options;
        static MsSqlDataBaseContext()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            var optionsBuilder = new DbContextOptionsBuilder<DataBaseContext>().UseSqlServer(connectionString);
            Options = optionsBuilder.Options;
        }

        public MsSqlDataBaseContext() : base(Options)
        { }
    }
}