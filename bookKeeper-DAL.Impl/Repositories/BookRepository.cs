using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Web;
using bookKeeper_Entity;
using bookKeeper_DAL.Abstract.IInterfaces;
using bookKeeper_DAL.Impl.Contexts;
using bookKeeper_DAL.Impl.Repositories;

namespace bookKeeper_DAL.Impl
{
    public class BookRepository : EFCoreRepository<int, Book>, IBookRepository
    {
        public BookRepository(DataBaseContext context) : base(context)
        { }

        IEnumerable<Book> IBookRepository.GetUsersBooks(int userId)
        {
            return Db.Books.Where(book => book.UserId == userId);
        }
    }
}
