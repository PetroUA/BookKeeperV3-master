using bookKeeper_DAL.Abstract.IInterfaces;
using bookKeeper_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookKeeper.Tests
{
    class MockBookRepository : MockRepository<int, Book>, IBookRepository
    {
        public object Books { get; internal set; }

        public IEnumerable<Book> GetUsersBooks(int userId)
        {
            return dic.Values.Where(book => book.UserId == userId);
        }
    }
}

