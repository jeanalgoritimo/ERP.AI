using ERP.AI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ERP.AI.Controllers;

public sealed class InteligenciaController(IRagService ragService) : Controller
{
    [HttpGet]
    public IActionResult Assistente() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Assistente(string pergunta, CancellationToken cancellationToken)
    {
        ViewData["Pergunta"] = pergunta;
        ViewData["Resposta"] = await ragService.PerguntarAsync(pergunta, cancellationToken);
        return View();
    }

    public IActionResult BaseConhecimento() => View();
}
