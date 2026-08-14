namespace ERP.AI.Models.Clientes;

public sealed class ClienteListViewModel
{
    public string Busca { get; init; } = string.Empty;
    public bool? Ativo { get; init; }
    public IReadOnlyList<ClienteListItemViewModel> Clientes { get; init; } = [];
    public int Total => Clientes.Count;
    public int TotalAtivos => Clientes.Count(x => x.Ativo);
    public decimal LimiteTotal => Clientes.Sum(x => x.LimiteCredito);
}
