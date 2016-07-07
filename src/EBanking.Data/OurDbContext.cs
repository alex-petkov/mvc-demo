using System.Data.Entity;
using EBanking.Data.Migrations;
using Npgsql;

namespace EBanking.Data
{
    public class OurDbContext: DbContext
    {
        public DbSet<DataBaseUserModel> User { get; set; }
        public DbSet<BankAccount> UserAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public OurDbContext()
            : base("TestConnection")
        {

            Database.SetInitializer<OurDbContext>(new CreateDatabaseIfNotExists<OurDbContext>());
            Database.SetInitializer<OurDbContext>(new MigrateDatabaseToLatestVersion<OurDbContext, Configuration>());

        }

        public void ApplyInterest(decimal rate)
        {
            Database.ExecuteSqlCommand("Select dbo.\"ApplyInterest\"(@rate,@type)", 
                new NpgsqlParameter("@rate", rate), 
                new NpgsqlParameter("@type", (int)TransactionType.Deposit));
        }
    }
}