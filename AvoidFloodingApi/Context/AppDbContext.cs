using AvoidFloodingApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AvoidFloodingApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Localizacao>? Localizacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Localizacao>().HasKey(c => c.LocalizacaoId);
            mb.Entity<Localizacao>().Property(c => c.Nome)
                                    .HasMaxLength(200)
                                    .IsRequired();
            mb.Entity<Localizacao>().Property(c => c.Latitude)
                                   .HasMaxLength(15)
                                   .IsRequired();
            mb.Entity<Localizacao>().Property(c => c.Longitude)
                                   .HasMaxLength(15)
                                   .IsRequired();
            mb.Entity<Localizacao>().Property(c => c.ProbabilidadeAlagamento)
                                   .HasPrecision(3,2)
                                   .HasMaxLength(15)
                                   .IsRequired();
            mb.Entity<Localizacao>().Property(c => c.DataAtualizacao)
                                  .HasMaxLength(15)
                                  .IsRequired();
        }
    }
}
