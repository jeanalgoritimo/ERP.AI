using ERP.AI.Data;
using ERP.AI.Domain.Entities;
using ERP.AI.Models.Clientes;
using Microsoft.EntityFrameworkCore;

namespace ERP.AI.Services;

public sealed class ClienteService(AppDbContext db) : IClienteService
{
    public async Task<ClienteListViewModel> ListarAsync(string? busca, bool? ativo,
        CancellationToken cancellationToken)
    {
        var query = db.Clientes.AsNoTracking();
        var termo = busca?.Trim();

        if (!string.IsNullOrWhiteSpace(termo))
            query = query.Where(x => x.Codigo.Contains(termo) ||
                                     x.RazaoSocial.Contains(termo) ||
                                     x.NomeFantasia.Contains(termo) ||
                                     x.Documento.Contains(termo));

        if (ativo.HasValue)
            query = query.Where(x => x.Ativo == ativo.Value);

        var itens = await query
            .OrderBy(x => x.RazaoSocial)
            .Select(x => new ClienteListItemViewModel(x.Id, x.Codigo, x.RazaoSocial,
                x.NomeFantasia, x.Documento, x.Cidade, x.Estado, x.Categoria,
                x.LimiteCredito, x.Ativo))
            .ToListAsync(cancellationToken);

        return new ClienteListViewModel { Busca = termo ?? string.Empty, Ativo = ativo, Clientes = itens };
    }

    public async Task<ClienteFormViewModel?> ObterAsync(Guid id, CancellationToken cancellationToken) =>
        await db.Clientes.AsNoTracking().Where(x => x.Id == id)
            .Select(x => new ClienteFormViewModel
            {
                Id = x.Id, Codigo = x.Codigo, RazaoSocial = x.RazaoSocial,
                NomeFantasia = x.NomeFantasia, Documento = x.Documento, Email = x.Email,
                Telefone = x.Telefone, Cidade = x.Cidade, Estado = x.Estado,
                Categoria = x.Categoria, LimiteCredito = x.LimiteCredito, Ativo = x.Ativo
            }).SingleOrDefaultAsync(cancellationToken);

    public async Task<(bool Success, string? Error)> CriarAsync(ClienteFormViewModel model,
        CancellationToken cancellationToken)
    {
        if (await ExisteDuplicidadeAsync(model.Codigo, model.Documento, null, cancellationToken))
            return (false, "Já existe um cliente com o mesmo código ou documento.");

        try
        {
            db.Clientes.Add(CriarEntidade(model));
            await db.SaveChangesAsync(cancellationToken);
            return (true, null);
        }
        catch (ArgumentException ex) { return (false, ex.Message); }
    }

    public async Task<(bool Success, string? Error)> AtualizarAsync(Guid id, ClienteFormViewModel model,
        CancellationToken cancellationToken)
    {
        var cliente = await db.Clientes.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (cliente is null) return (false, "Cliente não encontrado.");

        if (await ExisteDuplicidadeAsync(model.Codigo, model.Documento, id, cancellationToken))
            return (false, "Já existe outro cliente com o mesmo código ou documento.");

        try
        {
            cliente.Atualizar(model.Codigo, model.RazaoSocial, model.NomeFantasia,
                model.Documento, model.Email, model.Telefone, model.Cidade, model.Estado,
                model.Categoria, model.LimiteCredito, model.Ativo);
            await db.SaveChangesAsync(cancellationToken);
            return (true, null);
        }
        catch (ArgumentException ex) { return (false, ex.Message); }
    }

    public async Task<bool> ExcluirAsync(Guid id, CancellationToken cancellationToken)
    {
        var cliente = await db.Clientes.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (cliente is null) return false;
        db.Clientes.Remove(cliente);
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }

    private async Task<bool> ExisteDuplicidadeAsync(string codigo, string documento, Guid? ignorarId,
        CancellationToken cancellationToken)
    {
        var doc = new string(documento.Where(char.IsDigit).ToArray());
        var code = codigo.Trim().ToUpperInvariant();
        return await db.Clientes.AnyAsync(x => (!ignorarId.HasValue || x.Id != ignorarId) &&
            (x.Codigo == code || x.Documento == doc), cancellationToken);
    }

    private static Cliente CriarEntidade(ClienteFormViewModel m) =>
        new(m.Codigo, m.RazaoSocial, m.NomeFantasia, m.Documento, m.Email, m.Telefone,
            m.Cidade, m.Estado, m.Categoria, m.LimiteCredito, m.Ativo);
}
