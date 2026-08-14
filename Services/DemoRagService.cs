namespace ERP.AI.Services;

public sealed class DemoRagService : IRagService
{
    public Task<RagResponse> PerguntarAsync(string pergunta, CancellationToken cancellationToken)
    {
        var resposta = string.IsNullOrWhiteSpace(pergunta)
            ? "Informe uma pergunta sobre clientes, pedidos ou regras do ERP."
            : "Esta é a resposta demonstrativa. Na próxima etapa, os trechos recuperados da base de conhecimento serão enviados ao provedor de IA configurado.";

        IReadOnlyList<RagSource> fontes =
        [
            new("Política Comercial 2026", "COM-2026 § 4.2", 0.94),
            new("Manual do ERP.AI", "Clientes e limite de crédito", 0.87)
        ];
        return Task.FromResult(new RagResponse(resposta, fontes, true));
    }
}
