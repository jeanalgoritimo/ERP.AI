using Microsoft.AspNetCore.Mvc;

namespace ERP.AI.Controllers;

public sealed class ComercialController : Controller
{
    public IActionResult Cotacoes() => View();
}
