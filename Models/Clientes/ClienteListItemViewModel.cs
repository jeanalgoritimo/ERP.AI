namespace ERP.AI.Models.Clientes;

public sealed record ClienteListItemViewModel(
    Guid Id,
    string Codigo,
    string RazaoSocial,
    string NomeFantasia,
    string Documento,
    string Cidade,
    string Estado,
    string Categoria,
    decimal LimiteCredito,
    bool Ativo);
