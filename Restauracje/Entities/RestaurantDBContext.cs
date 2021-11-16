using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kurs2.Entities
{
    public class RestaurantDBContext : DbContext
    {
        private string _connectionString = "Server=DESKTOP-9CQ2O9E\\SQLEXPRESS;Database=RestaurantDb;Trusted_Connection=True";
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Dish> Dishes { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<Restaurant>().Property(r => r.Name).IsRequired();
            model.Entity<Address>().Property(r => r.Street).IsRequired().HasMaxLength(50);
            model.Entity<Address>().Property(r => r.City).IsRequired().HasMaxLength(50);
            model.Entity<Role>().Property(r => r.Name).IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
