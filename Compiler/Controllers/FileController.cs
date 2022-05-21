using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Compiler.Controllers
{
    public class FileController : Controller
    {
        [HttpPost]
        public IActionResult ReadHiddenFile(IFormFile file)
        {
            string text;

            if (file == null)
                return Json(new { status = 404 });
            try
            {
                text = ReadFormFile(file).ToString();

                if (text.Length <= 0)
                    return Json(new { status = 400 });

                return Json(new { status = 200, data = text });
            }
            catch (Exception)
            {
                return Json(new { status = 500 });
            }
        }

        private StringBuilder ReadFormFile(IFormFile file)
        {
            var result = new StringBuilder();
            try
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                        result.AppendLine(reader.ReadLine());
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }


            return result;
        }
    }
}
