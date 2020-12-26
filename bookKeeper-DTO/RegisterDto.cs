using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bookKeeper_DTO
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Не указано Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }

        public RegisterDto()
        { }

        public RegisterDto(string email, string password, string confirmPassword, string name)
        {
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
            Name = name;
        }
    }
}