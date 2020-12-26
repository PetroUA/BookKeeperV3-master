using bookKeeper_BLL.Abstract;
using bookKeeper_BLL.Impl;
using bookKeeper_DAL.Abstract;
using bookKeeper_DAL.Abstract.IInterfaces;
using bookKeeper_DAL.Impl;
using bookKeeper_DAL.Impl.Contexts;
using bookKeeper_DTO;
using bookKeeper_Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace bookKeeper.Tests
{
    [TestClass]
    public class IntegrationTests
    {
        private int testUserId = 1;
        private const int wrongUserId = -9;

        public IntegrationTests()
        {
            IoC.Register<DataBaseContext, TestDataBaseContext>();
            IoC.Register<IUserRepository, UserRepository>();
            IoC.Register<IBookRepository, BookRepository>();
            IoC.Register<IAuthService, AuthService>();
            IoC.Register<IBookService, BookService>();
        }

        [TestInitialize, TestCleanup]
        public void TestReset()
        {
            using (var context = IoC.Resolve<DataBaseContext>())
            {
                context.Books.RemoveRange(context.Books);
                context.SaveChanges();
                context.Users.RemoveRange(context.Users);
                context.SaveChanges();
                var user = new User() { Email = "test@email.com", Id = 0, Name = "Username", Password = "Pa$$w0rd" };
                context.Users.Add(user);
                context.SaveChanges();
                testUserId = user.Id;
            }
        }

        [TestMethod]
        public void AddBook_NormalData_Success()
        {
            //Arrange
            var bookService = IoC.Resolve<IBookService>();
            var bookDto = new BookDto("Title", "Author", "Review", 0);

            //Act
            bookService.AddBook(bookDto, testUserId);

            //Assert
            var context = IoC.Resolve<DataBaseContext>();
            var book = context.Books.First();
            Assert.AreEqual(book.Title, bookDto.Title);
            Assert.AreEqual(book.Author, bookDto.Author);
            Assert.AreEqual(book.Description, bookDto.Review);
            Assert.AreEqual(book.UserId, testUserId);
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))] //Assert
        public void AddBook_WrongUser_Sql()
        {
            //Arrange
            var bookService = IoC.Resolve<IBookService>();
            var bookDto = new BookDto("Title", "Author", "Review", 0);

            //Act
            bookService.AddBook(bookDto, wrongUserId);
        }

        [TestMethod]
        public void AddUser_NormalData_Success()
        {
            //Arrange
            var userRepo = IoC.Resolve<IUserRepository>();
            var user = new User();
            user.Email = "email";
            user.Name = "Name";
            user.Password = "password";

            //Act
            userRepo.Create(user);
            userRepo.Save();

            //Assert
            var context = IoC.Resolve<DataBaseContext>();
            var getUser = context.Users.First(u => u.Name == user.Name);
            Assert.AreEqual(user.Name, getUser.Name);
            Assert.AreEqual(user.Password, getUser.Password);
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        public void AddUser_NullName_Sql()
        {
            //Arrange
            var userRepo = IoC.Resolve<IUserRepository>();

            var user1 = new User() { Email = "test@email.com", Id = 0, Name = null, Password = "Pa$$w0rd" };


            //Act
            userRepo.Create(user1);
            userRepo.Save();
        }


    }
}

