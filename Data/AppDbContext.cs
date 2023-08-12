using Microsoft.EntityFrameworkCore;
using RabbitMQ.Models;

namespace RabbitMQ.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        // Add other DbSet properties for your entities
    }
}
