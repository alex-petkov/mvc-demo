using System.Data.Entity;

namespace EBanking.Data
{
    public class OurDbContext: DbContext
    {
        public DbSet<DataBaseUserModel> User { get; set; }

        public OurDbContext()
            : base("TestConnection")
        {

            Database.SetInitializer<OurDbContext>(new CreateDatabaseIfNotExists<OurDbContext>());
        }

    }
}