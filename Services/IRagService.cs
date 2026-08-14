namespace ERP.AI.Services;

public interface IRagService
{
    Task<RagResponse> PerguntarAsync(string pergunta, CancellationToken cancellationToken);
}

public sealed record RagSource(string Titulo, string Referencia, double Relevancia);
public sealed record RagResponse(string Resposta, IReadOnlyList<RagSource> Fontes, bool IsDemo);
