using System.ComponentModel.DataAnnotations;

namespace Bank.Models
{
    public class TransferModel
    {
        [Required(ErrorMessage = "Моля попълнете вашата банкова сметка тук.")]
        public string MyBankAccount { get; set; }

        [Required(ErrorMessage = "Моля напишете банковата сметка,към която желаете да правите превод")]
        public string OtherBankAccount { get; set; }
        [Required(ErrorMessage = "Моля въведете вашата сума")]
        public decimal Balance { get; set; }
    }
}