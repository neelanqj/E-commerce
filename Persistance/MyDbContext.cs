using E_commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistance
{
    public class MyDbContext: DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
            
        }
    }
}