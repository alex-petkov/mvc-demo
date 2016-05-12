using System;
using System.ComponentModel.DataAnnotations;

namespace EBanking.Data
{
    public class DataBaseUserModel
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime DateRegistered { get; set; }
    }
}