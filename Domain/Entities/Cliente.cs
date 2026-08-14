using System.ComponentModel.DataAnnotations;

namespace ERP.AI.Domain.Entities;

public sealed class Cliente
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    [MaxLength(20)]
    public string Codigo { get; private set; } = string.Empty;

    [MaxLength(160)]
    public string RazaoSocial { get; private set; } = string.Empty;

    [MaxLength(120)]
    public string NomeFantasia { get; private set; } = string.Empty;

    [MaxLength(14)]
    public string Documento { get; private set; } = string.Empty;

    [MaxLength(160)]
    public string Email { get; private set; } = string.Empty;

    [MaxLength(20)]
    public string Telefone { get; private set; } = string.Empty;

    [MaxLength(120)]
    public string Cidade { get; private set; } = string.Empty;

    [MaxLength(2)]
    public string Estado { get; private set; } = string.Empty;

    [MaxLength(30)]
    public string Categoria { get; private set; } = "Regular";

    public decimal LimiteCredito { get; private set; }
    public bool Ativo { get; private set; } = true;
    public DateTime CriadoEmUtc { get; private set; } = DateTime.UtcNow;
    public DateTime? AtualizadoEmUtc { get; private set; }

    private Cliente() { }

    public Cliente(string codigo, string razaoSocial, string nomeFantasia, string documento,
        string email, string telefone, string cidade, string estado, string categoria,
        decimal limiteCredito, bool ativo = true)
    {
        Atualizar(codigo, razaoSocial, nomeFantasia, documento, email, telefone, cidade,
            estado, categoria, limiteCredito, ativo);
        CriadoEmUtc = DateTime.UtcNow;
        AtualizadoEmUtc = null;
    }

    public void Atualizar(string codigo, string razaoSocial, string nomeFantasia,
        string documento, string email, string telefone, string cidade, string estado,
        string categoria, decimal limiteCredito, bool ativo)
    {
        if (string.IsNullOrWhiteSpace(codigo))
            throw new ArgumentException("O código do cliente é obrigatório.", nameof(codigo));
        if (string.IsNullOrWhiteSpace(razaoSocial))
            throw new ArgumentException("A razão social é obrigatória.", nameof(razaoSocial));
        if (limiteCredito < 0)
            throw new ArgumentOutOfRangeException(nameof(limiteCredito), "O limite não pode ser negativo.");

        var documentoNormalizado = SomenteDigitos(documento);
        if (documentoNormalizado.Length is not (11 or 14))
            throw new ArgumentException("Informe um CPF ou CNPJ válido.", nameof(documento));

        Codigo = codigo.Trim().ToUpperInvariant();
        RazaoSocial = razaoSocial.Trim();
        NomeFantasia = nomeFantasia.Trim();
        Documento = documentoNormalizado;
        Email = email.Trim().ToLowerInvariant();
        Telefone = telefone.Trim();
        Cidade = cidade.Trim();
        Estado = estado.Trim().ToUpperInvariant();
        Categoria = string.IsNullOrWhiteSpace(categoria) ? "Regular" : categoria.Trim();
        LimiteCredito = limiteCredito;
        Ativo = ativo;
        AtualizadoEmUtc = DateTime.UtcNow;
    }

    private static string SomenteDigitos(string valor) =>
        new(valor.Where(char.IsDigit).ToArray());
}
