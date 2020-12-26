using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using bookKeeper_BLL.Impl;
using bookKeeper_DTO;
using System.Linq;
using bookKeeper_Entity;

namespace bookKeeper.Tests
{
    [TestClass]
    public class BookServiceUnitTests
    {
        [TestMethod]
        public void AddBook_NormalData_Success()
        {
            //Arrange
            var bookRep = new MockBookRepository();
            var bookService = new BookService(bookRep);
            var bookDto = new BookDto("Title", "Author", "Review", 0);

            //Act
            bookService.AddBook(bookDto, 12345);
            var addedBook = bookRep.dic.First().Value;

            //Assert
            Assert.AreEqual(addedBook.Title, "Title");
            Assert.AreEqual(addedBook.Author, "Author");
            Assert.AreEqual(addedBook.Description, "Review");
            Assert.AreEqual(addedBook.UserId, 12345);
            Assert.AreEqual(addedBook.Id, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))] //Assert
        public void AddBook_Null_NullReference()
        {
            //Arrange
            var bookRep = new MockBookRepository();
            var bookService = new BookService(bookRep);

            //Act
            bookService.AddBook(null, 12345);
        }

        [TestMethod]
        public void EditBook_NormalData_Success()
        {
            //Arrange
            var bookRep = new MockBookRepository();
            var bookService = new BookService(bookRep);
            var userId = 12345;
            var oldBook = new Book() { Author = "Author", Description = "Desc", Title = "Title", Id = 15, UserId = userId };
            bookRep.dic[oldBook.Id] = oldBook;
            var bookDto = new BookDto("New Title", "New Author", "New Review", oldBook.Id);

            //Act
            bookService.EditBook(bookDto, userId);
            var editedBook = bookRep.dic.First().Value;

            //Assert
            Assert.AreEqual(editedBook.Title, bookDto.Title);
            Assert.AreEqual(editedBook.Author, bookDto.Author);
            Assert.AreEqual(editedBook.Description, bookDto.Review);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))] //Assert
        public void EditBook_WrongUser_InvalidOperation()
        {
            //Arrange
            var bookRep = new MockBookRepository();
            var bookService = new BookService(bookRep);
            var userId = 12345;
            var wrongUserId = 999;
            var oldBook = new Book() { Author = "Author", Description = "Desc", Title = "Title", Id = 15, UserId = userId };
            bookRep.dic[oldBook.Id] = oldBook;
            var bookDto = new BookDto("New Title", "New Author", "New Review", oldBook.Id);

            //Act
            bookService.EditBook(bookDto, wrongUserId);
        }

        [TestMethod]
        public void GetBooks_NormalData_Success()
        {
            //Arrange
            var bookRep = new MockBookRepository();
            var bookService = new BookService(bookRep);
            var userId = 12345;
            var anotherUserId = 999;
            var allBooks = new Dictionary<int, Book>()
            {
                { 1, new Book() {Id = 1, Title = "T1", Author = "A1", Description = "D1", UserId = userId} },
                { 2, new Book() {Id = 2, Title = "T2", Author = "A2", Description = "D2", UserId = anotherUserId} },
                { 3, new Book() {Id = 3, Title = "T3", Author = "A3", Description = "D3", UserId = userId} },
                { 4, new Book() {Id = 4, Title = "T4", Author = "A4", Description = "D4", UserId = anotherUserId} },
                { 5, new Book() {Id = 5, Title = "T5", Author = "A5", Description = "D5", UserId = anotherUserId} },
                { 6, new Book() {Id = 6, Title = "T6", Author = "A6", Description = "D6", UserId = userId} }
            };
            bookRep.dic = allBooks;

            //Act
            var userBooksDto = bookService.GetBooks(userId).ToList();

            //Assert
            var expectedUserBooks = allBooks.Values.Where(book => book.UserId == userId).ToList();

            Assert.AreEqual(userBooksDto.Count, expectedUserBooks.Count);
            for (int i = 0; i < expectedUserBooks.Count; i++)
            {
                var actual = expectedUserBooks[i];
                var receivedBook = userBooksDto[i];

                Assert.AreEqual(actual.Title, receivedBook.Title);
                Assert.AreEqual(actual.Author, receivedBook.Author);
                Assert.AreEqual(actual.Description, receivedBook.Review);
            }
        }

        [TestMethod]
        public void RemoveBook_NormalData_Success()
        {
            //Arrange
            var bookRep = new MockBookRepository();
            var bookService = new BookService(bookRep);
            var userId = 12345;
            var removingBookId = 25;
            var oldBook = new Book() { Author = "Author", Description = "Desc", Title = "Title", Id = 15, UserId = userId };
            var removingBook = new Book() { Author = "Author 2", Description = "Desc 2", Title = "Title 2", Id = removingBookId, UserId = userId };
            bookRep.dic[oldBook.Id] = oldBook;
            bookRep.dic[removingBook.Id] = removingBook;

            //Act
            bookService.RemoveBook(removingBookId, userId);

            //Assert
            Assert.IsFalse(bookRep.dic.ContainsKey(removingBookId));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))] //Assert
        public void RemoveBook_WrongUser_InvalidOperation()
        {
            //Arrange
            var bookRep = new MockBookRepository();
            var bookService = new BookService(bookRep);
            var userId = 12345;
            var wrongUser = 999;
            var removingBookId = 25;
            var removingBook = new Book() { Author = "Author 2", Description = "Desc 2", Title = "Title 2", Id = removingBookId, UserId = userId };
            bookRep.dic[removingBook.Id] = removingBook;

            //Act
            bookService.RemoveBook(removingBookId, wrongUser);
        }


    }
}
