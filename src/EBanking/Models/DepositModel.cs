using System;
using System.ComponentModel.DataAnnotations;

namespace Bank.Models
{
    public class DepositModel
    {
        [Required(ErrorMessage = "Моля попълнете вашата банкова сметка тук.")]
        public string MyBankAccount { get; set; }
        [Required(ErrorMessage = "Моля въведете избраната от вас сума.")]
        public decimal Balance { get; set; }
    }
}