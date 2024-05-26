using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Data.Entities;

namespace Data
{
    public class DefaultContext : DbContext
    {
        public DbSet<Route> Routes { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<City> Cities { get; set; }
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Connection>()
                .HasOne(c => c.Departure)
                .WithMany()
                .HasForeignKey(c => c.DepartureCityId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Connection>()
                .HasOne(c => c.Destination)
                .WithMany()
                .HasForeignKey(c => c.DestinationCityId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Route>()
                .HasOne(r => r.Departure)
                .WithMany()
                .HasForeignKey(r => r.DepartureCityId)
                .OnDelete(DeleteBehavior.NoAction);  // NoAction to prevent cascade paths

            modelBuilder.Entity<Route>()
                .HasOne(r => r.Destination)
                .WithMany()
                .HasForeignKey(r => r.DestinationCityId)
                .OnDelete(DeleteBehavior.NoAction);  // NoAction to prevent cascade paths


            base.OnModelCreating(modelBuilder);
        }
    }
}
