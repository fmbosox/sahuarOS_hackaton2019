using Core.Domain.Order;
using Core.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace Core.Model
{
    public interface SahuarOSContext
    {
         DbSet<Order> Orders { get;}
         DbSet<Product> Products { get; }
         DbSet<Customer> Customers { get;}

        int SaveChanges();
    }
}