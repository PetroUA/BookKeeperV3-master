using bookKeeper_DTO;
using System;
using System.Collections.Generic;

namespace bookKeeper_BLL.Abstract
{
    public interface IBookService : IDisposable
    {
        void AddBook(BookDto book, int userId);
        void RemoveBook(int bookId, int userId);
        void EditBook(BookDto book, int userId);
        IEnumerable<BookDto> GetBooks(int userId);
    }
}