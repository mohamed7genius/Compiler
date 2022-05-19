using Microsoft.AspNetCore.Mvc;

namespace Compiler.Controllers
{
    public class ParserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
