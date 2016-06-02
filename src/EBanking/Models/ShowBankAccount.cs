using System;

namespace Bank.Models
{
    public class ShowBankAccount
    {
        public Guid Key { get; set; }
        public string FriendlyName { get; set; }
        public decimal Balance { get; set; }
    }
}