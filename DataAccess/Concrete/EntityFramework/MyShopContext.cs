using DataAccess.Concrete.EntityFramework.EntityTypeConfiguration;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class MyShopContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MyShopDB;Trusted_Connection=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EfProductConfiguration());
            modelBuilder.ApplyConfiguration(new EfProductImageConfiguration());
            modelBuilder.ApplyConfiguration(new EfUserConfiguration());
            modelBuilder.ApplyConfiguration(new EfOrderConfiguration());
            modelBuilder.ApplyConfiguration(new EfOrderLineConfiguration());
            modelBuilder.ApplyConfiguration(new EfOrderAddressConfiguration());
            modelBuilder.ApplyConfiguration(new EfOrderPaymentConfiguration());
            modelBuilder.ApplyConfiguration(new EfBasketConfiguration());
            modelBuilder.ApplyConfiguration(new EfBasketLineConfiguration());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<OrderAddress> OrderAddresses { get; set; }
        public DbSet<OrderPayment> OrderPayments { get; set; }
        public DbSet<BasketLine> BasketLines { get; set; }
        public DbSet<Basket> Baskets { get; set; }


    }
}