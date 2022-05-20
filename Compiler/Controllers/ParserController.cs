using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Compiler.Models;
namespace Compiler.Controllers
{
    public class ParserController : Controller
    {
        String[,] table = ParsingTable.init();
        public String[] nonterminals = ParsingTable.GetNonTerminal();
        String[] terminals = ParsingTable.GetTerminal();

        [HttpPost]
        public IActionResult Index([FromForm] string code)
        {
            Debug.WriteLine(code);
            return Json(new { status = 200, output = "Hello World!" });
        }
        private int GetNonTerminalIndex(String nonTerminal)
        {

            for(int i = 0; i < nonterminals.Length; i++)
            {
                if (nonTerminal == nonterminals[i])
                {
                    return i;
                }
                
            }
            return -1;
        }  
        private int GetTerminalIndex(String Terminal)
        {

            for(int i = 0; i < terminals.Length; i++)
            {
                if (Terminal == terminals[i])
                {
                    return i;
                }
                
            }
            return -1;
        }
    }
}
