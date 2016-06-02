using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBanking.Data
{
    public class BankAccount
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public Guid Key { get; set; }
        public string FriendlyName { get; set; }
        public decimal Balance { get; set; }

        [ForeignKey("UserId")]
        public virtual DataBaseUserModel User { get; set; }
    }
}
