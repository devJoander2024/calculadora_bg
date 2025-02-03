using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApplication1.Entities;

namespace WebApplication1.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Operacion> Operaciones { get; set; }
    }
}
