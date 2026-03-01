using Microsoft.EntityFrameworkCore;
using Quejas.Entities;

namespace Quejas.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Queja> Quejas { get; set; }

        public DbSet<HistorialQueja> HistorialQuejas { get; set; }

    }
}