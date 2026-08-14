using ERP.AI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ERP.AI.Data;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Cliente> Clientes => Set<Cliente>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var cliente = modelBuilder.Entity<Cliente>();
        cliente.ToTable("Clientes");
        cliente.HasKey(x => x.Id);
        cliente.HasIndex(x => x.Codigo).IsUnique();
        cliente.HasIndex(x => x.Documento).IsUnique();
        cliente.Property(x => x.LimiteCredito).HasPrecision(18, 2);
    }
}
