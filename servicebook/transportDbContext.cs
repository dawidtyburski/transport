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
