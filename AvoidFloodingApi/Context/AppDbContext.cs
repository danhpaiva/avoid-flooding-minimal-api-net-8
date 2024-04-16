using AvoidFloodingApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AvoidFloodingApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Localizacao>? Localizacoes { get; set; }
    }
}
