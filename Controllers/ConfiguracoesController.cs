using Microsoft.AspNetCore.Mvc;

namespace ERP.AI.Controllers;

public sealed class ConfiguracoesController : Controller
{
    public IActionResult Index() => View();
}
