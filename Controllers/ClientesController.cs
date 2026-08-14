using Microsoft.AspNetCore.Mvc;

namespace ERP.AI.Controllers
{
    public class ClientesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
