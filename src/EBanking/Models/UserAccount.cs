using System.ComponentModel.DataAnnotations;

namespace Bank.Models
{
    public class UserAccount
    {
        [Required(ErrorMessage = "Моля въведете потребителско име.")]
        [MinLength(4, ErrorMessage = "Потребителското име трябва да е поне 4 символа."),
         StringLength(16, ErrorMessage = "Потребителското име не трябва да е повече 16 символа.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Моля въведете парола.")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Паролата трябва да е поне 8 символа.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Моля въведете повторно вашата парола.")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Паролата трябва да е поне 8 символа.")]

        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Моля въведете името си.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Моля въведете имейл адреса си.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Имейл адреса Ви е невалиден.")]

        [EmailAddress(ErrorMessage = "Insert valid Email.")]
        public string Email { get; set; }
    }
}