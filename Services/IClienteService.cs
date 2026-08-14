using ERP.AI.Models.Clientes;

namespace ERP.AI.Services;

public interface IClienteService
{
    Task<ClienteListViewModel> ListarAsync(string? busca, bool? ativo, CancellationToken cancellationToken);
    Task<ClienteFormViewModel?> ObterAsync(Guid id, CancellationToken cancellationToken);
    Task<(bool Success, string? Error)> CriarAsync(ClienteFormViewModel model, CancellationToken cancellationToken);
    Task<(bool Success, string? Error)> AtualizarAsync(Guid id, ClienteFormViewModel model, CancellationToken cancellationToken);
    Task<bool> ExcluirAsync(Guid id, CancellationToken cancellationToken);
}
