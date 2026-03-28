using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystemWebAPI.Models.Entities;

namespace TaskManagementSystemWebAPI.Data
{
    public class AppDbContext : DbContext
    {
     public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
