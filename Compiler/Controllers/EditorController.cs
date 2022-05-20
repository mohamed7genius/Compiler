using Compiler.DataSturcutres;
using Compiler.Helper_Classes;
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

        public IActionResult GetAutoCompleteID()
        {
            return Json(new { status = 200, data = Linker.identifierDict });
        }

    }
}