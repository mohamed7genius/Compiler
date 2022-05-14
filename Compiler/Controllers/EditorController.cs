using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Compiler.Controllers
{
    public class EditorController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}