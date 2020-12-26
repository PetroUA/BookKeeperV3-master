using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using bookKeeper_BLL.Abstract;
using bookKeeper_DTO;

namespace bookKeeper.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService bookService;

        public HomeController()
        {
            bookService = IoC.Resolve<IBookService>();
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Index()
        {
            var objId = Session["id"];
            if (objId == null) return RedirectToAction("Login", "Account");
            int userId = (int)objId;
            var books = bookService.GetBooks(userId);
            return View(books);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult EditBook(BookDto book, int bookId)
        {
            int userId = (int) Session["id"];
            bookService.EditBook(book, userId);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult AddBook(BookDto book)
        {
            int userId = (int)Session["id"];
            bookService.AddBook(book, userId);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult RemoveBook(int bookId)
        {
            int userId = (int)Session["id"];
            bookService.RemoveBook(bookId, userId);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) bookService.Dispose();
            base.Dispose(disposing);
        }
    }
}
