using Microsoft.EntityFrameworkCore;

namespace Minimal_ASP.NET_With_JWT.DbContexts
{
    public class DbContextData : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = config.GetSection("ConnectionStrings:connection").Value;

            optionsBuilder.UseSqlServer(connectionString);
        }


    }
}
