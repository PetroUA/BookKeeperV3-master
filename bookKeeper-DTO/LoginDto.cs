using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bookKeeper_DTO
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public LoginDto()
        { }

        public LoginDto(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}