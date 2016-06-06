using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bank.Models
{
    public class AccountCheck
    {
        [Required(ErrorMessage = "Моля попълнете вашата банкова сметка тук.")]
        public string MyBankAccount { get; set; }
    }
}