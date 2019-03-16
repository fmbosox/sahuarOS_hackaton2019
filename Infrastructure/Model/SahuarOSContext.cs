﻿using Core.Domain.Order;
using Core.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Model
{
    public class SahuarOSContext : DbContext
    {
        public SahuarOSContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(order =>
            {
                order.HasOne(o => o.Customer);
                order.HasMany(o => o.Products);
            });
            
            
            modelBuilder.Entity<OrderProduct>(orderProduct =>
            {
                orderProduct.HasOne(op => op.Product);
            });
        }
    }
}