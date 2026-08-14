using ERP.AI.Models.Clientes;
using ERP.AI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ERP.AI.Controllers;

public sealed class ClientesController(IClienteService clientes) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string? busca, bool? ativo, CancellationToken ct) =>
        View(await clientes.ListarAsync(busca, ativo, ct));

    [HttpGet]
    public IActionResult Criar() => View(new ClienteFormViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Criar(ClienteFormViewModel model, CancellationToken ct)
    {
        if (!ModelState.IsValid) return View(model);
        var result = await clientes.CriarAsync(model, ct);
        if (!result.Success) { ModelState.AddModelError(string.Empty, result.Error!); return View(model); }
        TempData["Success"] = "Cliente cadastrado com sucesso.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Editar(Guid id, CancellationToken ct)
    {
        var model = await clientes.ObterAsync(id, ct);
        return model is null ? NotFound() : View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(Guid id, ClienteFormViewModel model, CancellationToken ct)
    {
        if (model.Id != id) return BadRequest();
        if (!ModelState.IsValid) return View(model);
        var result = await clientes.AtualizarAsync(id, model, ct);
        if (!result.Success) { ModelState.AddModelError(string.Empty, result.Error!); return View(model); }
        TempData["Success"] = "Cliente atualizado com sucesso.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Detalhes(Guid id, CancellationToken ct)
    {
        var model = await clientes.ObterAsync(id, ct);
        return model is null ? NotFound() : View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Excluir(Guid id, CancellationToken ct)
    {
        if (!await clientes.ExcluirAsync(id, ct)) return NotFound();
        TempData["Success"] = "Cliente excluído com sucesso.";
        return RedirectToAction(nameof(Index));
    }
}
