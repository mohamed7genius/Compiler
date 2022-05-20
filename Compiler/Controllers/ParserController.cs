using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Compiler.Controllers
{
    public class ParserController : Controller
    {

        [HttpPost]
        public IActionResult Index([FromForm] string code)
        {
            Debug.WriteLine(code);
            return Json(new { status = 200, output = "Hello World!" });
        }
    }
}
