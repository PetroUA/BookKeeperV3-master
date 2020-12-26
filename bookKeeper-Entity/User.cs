using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace bookKeeper_Entity
{
    public class User : IWithId<int>
    {
        [Key, Required]
        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}