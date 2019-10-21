using ExampleWebApi.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleWebApi.Core.Domain.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
