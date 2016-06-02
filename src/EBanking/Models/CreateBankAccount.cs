using System.ComponentModel.DataAnnotations;

namespace Bank.Models
{
    public class CreateBankAccount
    {
        [Required(ErrorMessage = "Моля въведе име на вашата банкова сметка.")]
        public string FriendlyName { get; set; }
    }
}