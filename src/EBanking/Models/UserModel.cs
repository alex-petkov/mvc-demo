using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EBanking.Models
{
    public class RegisterUserModel
    {
        [Required(ErrorMessage = "Моля въведете потребителско име")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Моля въведете парола")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage="Моля въведете повторно паролата си")]
        [DataType(DataType.Password)]
        [Display(Name="Потвърдете паролата")]
        [Compare("Password", ErrorMessage="Двете пароли не съвпадат")]
        public string ConfirmPassword { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
    }
}