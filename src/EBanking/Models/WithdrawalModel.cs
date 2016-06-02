using System.ComponentModel.DataAnnotations;

namespace Bank.Models
{
    public class WithdrawalModel
    {
        [Required(ErrorMessage = "Моля попълнете вашата банкова сметка тук.")]
        public string MyAccount { get; set; }

        [Required(ErrorMessage = "Моля въведете вашата сума")]
        public decimal Balance { get; set; }
    }
}