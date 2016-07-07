using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bank.Models
{
    public class TransactionCheck
    {
        [Required(ErrorMessage = "Моля попълнете вашата банкова сметка тук.")]
        public string Comment { get; set; }

        public DateTime Date { get; set; }





        
    }
}