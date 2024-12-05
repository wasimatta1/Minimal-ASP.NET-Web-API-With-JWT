using Microsoft.EntityFrameworkCore;
using Minimal_ASP.NET_With_JWT.Entities;

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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "Wasim", LastName = "Atta", UserName = "wasimatta", Password = "123" },
                new User { Id = 2, FirstName = "Ahmad", LastName = "Ali", UserName = "ahmadali", Password = "123" }
            );
        }

    }
}
