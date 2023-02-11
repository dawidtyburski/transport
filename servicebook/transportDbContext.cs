using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using transport.Models;

namespace transport
{
    public class transportDbContext : IdentityDbContext<CustomUser>
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<PickupAdress> PickupAdresses { get; set; }
        public DbSet<DestinationAdress> DestinationAdresses { get; set; }


        private string _connectionString = 
            "Server=(localdb)\\mssqllocaldb;Database=TransportDb;Trusted_Connection=True;";

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //--Order
            modelBuilder.Entity<Order>()
                .Property(o => o.Title)
                .IsRequired();
            modelBuilder.Entity<Order>()
                .Property(o => o.Weight)
                .IsRequired();
            modelBuilder.Entity<Order>()
                .Property(o => o.PalletPlace)
                .IsRequired();
            modelBuilder.Entity<Order>()
                .Property(o => o.Price)
                .IsRequired();
            modelBuilder.Entity<Order>()
                .Property(o => o.Description)
                .IsRequired(false);

            //--PickupAdress
            modelBuilder.Entity<PickupAdress>()
                .HasMany(i => i.Orders)
                .WithOne(e => e.PickupAdress)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<PickupAdress>()
                .Property(i => i.City)
                .IsRequired();
            modelBuilder.Entity<PickupAdress>()
               .Property(i => i.PostCode)
               .IsRequired();
            modelBuilder.Entity<PickupAdress>()
               .Property(i => i.Country)
               .IsRequired();

            //--DestinationAdress
            modelBuilder.Entity<DestinationAdress>()
                .HasMany(d => d.Orders)
                .WithOne(e => e.DestinationAdress)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<DestinationAdress>()
                .Property(d => d.City)
                .IsRequired();
            modelBuilder.Entity<DestinationAdress>()
               .Property(d => d.PostCode)
               .IsRequired();
            modelBuilder.Entity<DestinationAdress>()
               .Property(d => d.Country)
               .IsRequired();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
