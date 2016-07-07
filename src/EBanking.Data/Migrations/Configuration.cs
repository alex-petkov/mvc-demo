using System.IO;
using System.Reflection;
using Npgsql;

namespace EBanking.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EBanking.Data.OurDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(OurDbContext context)
        {
            var assembly = Assembly.GetExecutingAssembly();

            try
            {
                using (var stream = assembly.GetManifestResourceStream("EBanking.Data.Seed-1.txt"))
                using (var reader = new StreamReader(stream))
                {
                    context.Database.ExecuteSqlCommand(reader.ReadToEnd());
                }
            }
            catch (NpgsqlException ex)
            {
                if (!ex.Message.Contains("extension \"uuid-ossp\" already exists"))
                    throw ex;
            }

            using (var stream = assembly.GetManifestResourceStream("EBanking.Data.Seed-2.txt"))
            using (var reader = new StreamReader(stream))
            {
                context.Database.ExecuteSqlCommand(reader.ReadToEnd());
            }
        }
    }
}
