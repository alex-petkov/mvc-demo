using System;
using System.ComponentModel.DataAnnotations;

namespace EBanking.Data
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        public int UserAccountId { get; set; }
        public Guid Key { get; set; }

        public TransactionType Type { get; set; }
        public DateTime EvenDate { get; set; }
        public decimal Amount { get; set; }
    }

    public enum TransactionType
    {
        Withdrawal = 1,
        Deposit,
        Transfer
    }
}