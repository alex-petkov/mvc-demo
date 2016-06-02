using System.Data.Entity;
using EBanking.Data.Migrations;

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

    }
}