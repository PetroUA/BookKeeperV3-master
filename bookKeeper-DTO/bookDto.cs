using bookKeeper_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bookKeeper_DTO
{
    public class BookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Review { get; set; }
        public int BookId { get; set; }

        public BookDto()
        { }

        public BookDto(string title, string author, string review, int bookId )
        {
            Title = title;
            Author = author;
            Review = review;
            BookId = bookId;
        }

    }
}