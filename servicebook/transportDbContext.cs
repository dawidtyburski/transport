using Azure;
using Microsoft.EntityFrameworkCore;
using transport.Models;

namespace transport
{
    public class transportDbContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Principal> Principals { get; set; }
        public DbSet<PrincipalAdress> PrincipalsAdresses { get; set;}
        public DbSet<InitialAdress> InitialAdresses { get; set; }
        public DbSet<DestinationAdress> DestinationAdresses { get; set; }

        private string _connectionString = 
            "Server=(localdb)\\mssqllocaldb;Database=TransportDb;Trusted_Connection=True;";

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //--Country
            modelBuilder.Entity<Country>()
                .HasMany(c => c.InitialAdresses)
                .WithOne(e => e.Country)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Country>()
                .HasMany(c => c.DestinationAdresses)
                .WithOne(e => e.Country)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Country>()
                .HasMany(c => c.PrincipalAdresses)
                .WithOne(e => e.Country)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Country>()
                .Property(c => c.CountryName)
                .IsRequired()
                .HasMaxLength(20);
            modelBuilder.Entity<Country>()
                .HasData(
                    new Country { Id = 1, CountryName = "Austria" },
                    new Country { Id = 2, CountryName = "Belgia" },
                    new Country { Id = 3, CountryName = "Bułgaria" },
                    new Country { Id = 4, CountryName = "Chorwacja" },
                    new Country { Id = 5, CountryName = "Cypr" },
                    new Country { Id = 6, CountryName = "Czechy" },
                    new Country { Id = 7, CountryName = "Dania" },
                    new Country { Id = 8, CountryName = "Estonia" },
                    new Country { Id = 9, CountryName = "Finlandia" },
                    new Country { Id = 10, CountryName = "Francja" },
                    new Country { Id = 11, CountryName = "Grecja" },
                    new Country { Id = 12, CountryName = "Hiszpania" },
                    new Country { Id = 13, CountryName = "Irlandia" },
                    new Country { Id = 14, CountryName = "Litwa" },
                    new Country { Id = 15, CountryName = "Luksemburg" },
                    new Country { Id = 16, CountryName = "Łotwa" },
                    new Country { Id = 17, CountryName = "Malta" },
                    new Country { Id = 18, CountryName = "Holandia" },
                    new Country { Id = 19, CountryName = "Niemcy" },
                    new Country { Id = 20, CountryName = "Polska" },
                    new Country { Id = 21, CountryName = "Portugalia" },
                    new Country { Id = 22, CountryName = "Rumunia" },
                    new Country { Id = 23, CountryName = "Słowacja" },
                    new Country { Id = 24, CountryName = "Słowenia" },
                    new Country { Id = 25, CountryName = "Szwecja" },
                    new Country { Id = 26, CountryName = "Węgry" },
                    new Country { Id = 27, CountryName = "Włochy" }
                );
            //--Order
            modelBuilder.Entity<Order>()
                .Property(o => o.Title)
                .IsRequired()
                .HasMaxLength(20);
            modelBuilder.Entity<Order>()
                .Property(o => o.Weight)
                .IsRequired()
                .HasMaxLength(10);
            modelBuilder.Entity<Order>()
                .Property(o => o.PalletPlace)
                .IsRequired()
                .HasMaxLength(2);
            modelBuilder.Entity<Order>()
                .Property(o => o.ContactEmail)
                .IsRequired();
            modelBuilder.Entity<Order>()
                .Property(o => o.ContactPhone)
                .IsRequired();
            modelBuilder.Entity<Order>()
                .Property(o => o.Price)
                .IsRequired()
                .HasMaxLength(5);
            modelBuilder.Entity<Order>()
                .Property(o => o.Description)
                .IsRequired(false);

            //--Principal
            modelBuilder.Entity<Principal>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(20);
            modelBuilder.Entity<Principal>()
                .Property(p => p.ContactEmail)
                .IsRequired();

            //--PrincipalAdress
            modelBuilder.Entity<PrincipalAdress>()
                .HasMany(pr => pr.Principals)
                .WithOne(e => e.PrincipalAdress)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<PrincipalAdress>()
                .Property(pr => pr.City)
                .IsRequired()
                .HasMaxLength(20);
            modelBuilder.Entity<PrincipalAdress>()
               .Property(pr => pr.PostCode)
               .IsRequired()
               .HasMaxLength(10);

            //--InitialAdress
            modelBuilder.Entity<InitialAdress>()
                .HasMany(i => i.Orders)
                .WithOne(e => e.InitialAdress)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<InitialAdress>()
                .Property(i => i.City)
                .IsRequired()
                .HasMaxLength(20);
            modelBuilder.Entity<InitialAdress>()
               .Property(i => i.PostCode)
               .IsRequired()
               .HasMaxLength(10);

            //--DestinationAdress
            modelBuilder.Entity<DestinationAdress>()
                .HasMany(d => d.Orders)
                .WithOne(e => e.DestinationAdress)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<DestinationAdress>()
                .Property(d => d.City)
                .IsRequired()
                .HasMaxLength(20);
            modelBuilder.Entity<DestinationAdress>()
               .Property(d => d.PostCode)
               .IsRequired()
               .HasMaxLength(10);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
