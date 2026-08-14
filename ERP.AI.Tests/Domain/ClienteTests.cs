using ERP.AI.Domain.Entities;

namespace ERP.AI.Tests.Domain;

public sealed class ClienteTests
{
    [Fact]
    public void Criar_NormalizaCampos()
    {
        var cliente = new Cliente(" cli-10 ", "Cliente Teste", "Teste", "12.345.678/0001-90",
            " CONTATO@TESTE.COM ", "", "Vitória", "es", "Ouro", 1_000);
        Assert.Equal("CLI-10", cliente.Codigo);
        Assert.Equal("12345678000190", cliente.Documento);
        Assert.Equal("contato@teste.com", cliente.Email);
        Assert.Equal("ES", cliente.Estado);
    }

    [Fact]
    public void Criar_ComLimiteNegativo_LancaExcecao() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => new Cliente("CLI-10", "Cliente Teste", "Teste", "12345678000190", "", "", "Vitória", "ES", "Regular", -1));

    [Theory]
    [InlineData("")]
    [InlineData("123")]
    [InlineData("123456789012")]
    public void Criar_ComDocumentoInvalido_LancaExcecao(string documento) =>
        Assert.Throws<ArgumentException>(() => new Cliente("CLI-10", "Cliente Teste", "Teste", documento, "", "", "Vitória", "ES", "Regular", 0));
}
