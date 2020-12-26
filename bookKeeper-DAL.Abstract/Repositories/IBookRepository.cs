using bookKeeper_DAL.Abstract.Repository;
using bookKeeper_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bookKeeper_DAL.Abstract.IInterfaces
{
    public interface IBookRepository : IRepository<int, Book> 
    {
        IEnumerable<Book> GetUsersBooks(int userId);
    }
}