using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Compiler.Models;
using Compiler.DataSturcutres;
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
            //code = "Iow ID ( ) { If ( ID < FLOAT_NUM ) { Iow ID ; } }";
            code = code + " #";
            int IP = 0;
            Stack stack = new Stack();
            stack.Push("program");
            string[] token = X(code);
            Debug.WriteLine(code);
			while (stack.Peek() != "#")
			{   
				if (IsTerminal(stack.Peek()))
				{
                    String h = stack.Peek();

                    if (stack.Peek() == token[IP])
					{
                        Debug.WriteLine("Match", token[IP]);
                        stack.Pop();
                        IP++;
					}
					else
					{
                        Debug.WriteLine("Error terminal");
                        //stack.Pop();
					}
                
				}
				else
                {
                    String j = stack.Peek();
                    var row = GetNonTerminalIndex(stack.Peek());
                    var column = GetTerminalIndex(token[IP]);
					if (table[row, column]==null)
					{
                        Debug.WriteLine(stack.Peek()+ "Error");
                        //stack.Pop();
					}
					else if (table[row, column] != null)
                    {
                       string Production = stack.Pop();
                       string [] ProductionArray= X(table[row, column]);
						for (int i = ProductionArray.Length-1; i >= 0; i--)
						{
                            if (ProductionArray[i] == "")
                            {

                            }
                            else
                            {
                                stack.Push(ProductionArray[i]);
                            }
						}
					}
				}
			}
            return Json(new { status = 200, output = "Hello World!" });
        }
        private bool IsTerminal(string terminalCode)
		{
             if(GetTerminalIndex(terminalCode) == -1)
			{
                return false;
			}
            return true;
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
        private string []  X (String Code)
		{
            string[] Token = Code.Split(" ");
            return Token;
			/*for (int i=0;i<Code.Length;i++)
			{
				if (Code[i] ==" ")
				{

				}
			}*/
		}
    }
}
