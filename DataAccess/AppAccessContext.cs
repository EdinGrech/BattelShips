using DataAccess.Structures.DBStructures;
using DataAccess.Structures.DBStructures.Relations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public class AppAccessContext
    {

        public class AppDBContext : DbContext
        {
            public DbSet<Game> Games { get; set; }
            public DbSet<Player> Players { get; set; }
            public DbSet<Attack> Attacks { get; set; }
            public DbSet<Board> Boards { get; set; }
            public DbSet<Board2Attack> Boards2Attacks { get; set; }
            public DbSet<Board2Ship> Boards2Ships { get; set; }
            public DbSet<GameShipConfiguration> GameShipConfigurations { get; set; }
            public DbSet<ShipTypes> ShipTypes { get; set; }
            public DbSet<Ship> Ships { get; set; }

            public string connectionString()
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json")
                .Build();

                string connectionString = configuration.GetConnectionString("DefaultConnection")!;
                return connectionString;
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                //optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=battelShipDB;Trusted_Connection=True;MultipleActiveResultSets=true;");
                optionsBuilder.UseSqlServer(connectionString());
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Player>().HasData(
                        new Player { id = 1, username = "test", password = "test123456789" },
                        new Player { id = 2, username = "test2", password = "test12345678" }
                    );

                modelBuilder.Entity<ShipTypes>().HasData(
                        new ShipTypes { id = 1, size = 2, title = "size 2" },
                        new ShipTypes { id = 2, size = 3, title = "size 3" },
                        new ShipTypes { id = 3, size = 3, title = "size 3" },
                        new ShipTypes { id = 4, size = 4, title = "size 4" },
                        new ShipTypes { id = 5, size = 5, title = "size 5" }
                    );
            }
        }
    }
}
