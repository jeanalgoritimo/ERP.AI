using Microsoft.AspNetCore.Mvc;

namespace ERP.AI.Controllers;

public sealed class FinanceiroController : Controller
{
    public IActionResult Receber() => View();
    public IActionResult Pagar() => View();
}
