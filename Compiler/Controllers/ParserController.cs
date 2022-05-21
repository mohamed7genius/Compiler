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
        public List<string> parserOutput = new List<string>();
        [HttpPost]
        public IActionResult Index([FromForm] string code)
        {
            
            //code = "Worthless ID ( ) \n { Iow ID = INT_NUM ; \n Loopwhen ( ID < ID ) { }";
                       // code = "Iow ID ( Chlo ID , Iow ID ) \n { Iteratewhen ( ID = INT_NUM ; ID < = INT_NUM ; ID = ID + INT_NUM ) { \n Iow ID ; \n } }";
            code = code + " #";
            int LineNumber=1;

            int IP = 0;
            Stack stack = new Stack();
            //Stack <String>stack = new Stack<String>();
            stack.Push("program");
            string[] token = X(code);
            Debug.WriteLine(code);
            string Production = stack.Peek();
             Stack s =new Stack();
            while (stack.Peek() != "#")
			{   
				if (IsTerminal(stack.Peek()))
				{
                    String h = stack.Peek();

                    if (stack.Peek() == "#" && token[IP] == "#")
                    {
                        Debug.WriteLine("Line " + LineNumber + " Match" + " Rule : " + Production);
                        parserOutput.Add("Line " + LineNumber + " Match" + " Rule : "+s.Pop());
                        LineNumber++;
                        IP++;
                    }
                    if (stack.Peek() == token[IP])
					{
                        Debug.WriteLine("Match", token[IP]);
                        stack.Pop();
                        IP++;
					}

					else
					{
                        Debug.WriteLine("Error terminal");
                        parserOutput.Add("Line " + LineNumber + " Unmatch" + " Rule : ");
                        break;
					}
                    if (token[IP]=="") { IP++; }
                    if (token[IP] == "\n" || token[IP] =="\r\n" || (stack.Peek() =="#" && token[IP] =="#"))
                    {
                        Debug.WriteLine("Line " + LineNumber + " Match"+" Rule : " +Production);
                        parserOutput.Add("Line " + LineNumber + " Match" + " Rule : ");
                        LineNumber++;
                        IP++;
                    }

                }
				else
                {
                    String j = stack.Peek();
                    var row = GetNonTerminalIndex(stack.Peek());
                    var column = GetTerminalIndex(token[IP]);
                    if(column == -1)
                    {
                        Debug.WriteLine("Erorr");
                        parserOutput.Add("Line " + LineNumber + " Unmatch" + " Rule : ");
                        break;
                    }
					if (table[row, column]==null)
					{
                        Debug.WriteLine(stack.Peek()+ "Error");
                        break;
					}
					else if (table[row, column] != null)
                    {
                       Production = stack.Pop();
                        s.Push(Production);
                       string [] ProductionArray= X(table[row, column]);
						for (int i = ProductionArray.Length-1; i >= 0; i--)
						{
                            if (ProductionArray[i] == "" || ProductionArray[i]==null)
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
            return Json(new { status = 200, output = parserOutput });
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
		
		}

    }
}
