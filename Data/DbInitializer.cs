using ERP.AI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ERP.AI.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        await using var scope = services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await db.Database.EnsureCreatedAsync();

        if (await db.Clientes.AnyAsync()) return;

        db.Clientes.AddRange(
            new Cliente("CLI-0001", "Alpha Comércio Ltda.", "Alpha Comércio",
                "12345678000190", "contato@alpha.demo", "(31) 3333-1000",
                "Belo Horizonte", "MG", "Ouro", 150_000),
            new Cliente("CLI-0002", "Beta Indústria S.A.", "Beta Indústria",
                "98765432000110", "financeiro@beta.demo", "(11) 3333-2000",
                "São Paulo", "SP", "Prata", 300_000),
            new Cliente("CLI-0003", "Gamma Serviços Ltda.", "Gamma Serviços",
                "11222333000144", "contato@gamma.demo", "(32) 3333-3000",
                "Juiz de Fora", "MG", "Regular", 80_000));

        await db.SaveChangesAsync();
    }
}
