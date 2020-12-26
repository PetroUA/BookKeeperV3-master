using bookKeeper_DAL.Impl.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookKeeper.Tests
{
    class TestDataBaseContext : DataBaseContext
    {
        private static readonly DbContextOptions<DataBaseContext> Options;
        static TestDataBaseContext()
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=bookKeeperTestsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var optionsBuilder = new DbContextOptionsBuilder<DataBaseContext>().UseSqlServer(connectionString);
            Options = optionsBuilder.Options;
        }

        public TestDataBaseContext() : base(Options)
        { }
    }
}
