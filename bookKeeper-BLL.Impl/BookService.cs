using bookKeeper_BLL.Abstract;
using bookKeeper_DAL.Abstract.IInterfaces;
using bookKeeper_DTO;
using bookKeeper_Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace bookKeeper_BLL.Impl
{
    public class BookService : IBookService
    {
        private readonly IBookRepository BookRepo;

        public BookService(IBookRepository bookRepo)
        {
            BookRepo = bookRepo;
        }

        public void AddBook(BookDto bookDto, int userId)
        {
            Book book = new Book();
            book.Title = bookDto.Title;
            book.Author = bookDto.Author;
            book.Description = bookDto.Review;
            book.UserId = userId;
            BookRepo.Create(book);
            BookRepo.Save();
        }

        public void EditBook(BookDto bookDto, int userId)
        {
            Book book = BookRepo.GetSingle(bookDto.BookId);
            if (book.UserId != userId)
                throw new InvalidOperationException("You are trying to edit another user's Book.");
            book.Title = bookDto.Title;
            book.Author = bookDto.Author;
            book.Description = bookDto.Review;
            BookRepo.Update(book);
            BookRepo.Save();
        }

        public IEnumerable<BookDto> GetBooks(int userId)
        {
            return BookRepo.GetUsersBooks(userId).Select(book => new BookDto(book.Title, book.Author, book.Description, book.Id));
        }

        public void RemoveBook(int bookId, int userId)
        {
            Book book = BookRepo.GetSingle(bookId);
            if (book.UserId != userId)
                throw new InvalidOperationException("You are trying to delete another user's Book.");
            BookRepo.Delete(bookId);
            BookRepo.Save();
        }


        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    BookRepo.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}