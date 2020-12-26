using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace bookKeeper_Entity
{
    public class Book : IWithId<int>
    {
        [Key, Required]
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
    }
}