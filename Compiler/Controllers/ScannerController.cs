using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compiler.Models;

namespace Compiler.Controllers
{
    public class ScannerController : Controller
    {
        public static ScannerController Instance;

        char[] numbers =
        {
            '0','1','2','3','4','5','6','7','8','9'
        };
        char[] specialCharacters =
        {
            '!','-','%','*','#','\'','^','+','/','(',')','=','{','}','[',']','|',':',';','<','>',',',',','.','\"','$','@','~','&'
        };
        string token;
        int[,] validTransitions = transitionTable.init();
        int currentsState;
        int lineNumber = 1;
        int totalErrors = 0;
        bool isComment = false;
        bool LineComment = false;
        bool acceptedState = false;
        bool canBeConstant = true;
        public List<string> scannerOutput = new List<string>();
        public void scanCode(string code, string filePath)
        int start = -1;
        int end = -1;

        List<Dictionary<string, string>> Tokens = new List<Dictionary<string, string>>();

        List<String> scannerOutput = new List<string>();
        public void scanCode(String code)
        {
            initValues();

            // DIRTY SOULTION AHEAD TO FIX HAVING TO WRITE A WHITESPACE AT THE END OF (code) ARRAY
            if (code[code.Length - 1] != ' ')
                code += ' ';
            for (int i = 0; i < code.Length; i++)
            {
                
                if (code[i] == '\r')
                {
                    continue;
                }
                if ((code[i] != ' ' && code[i] != '\t' && code[i] != ',' && code[i] != ';' && code[i] != '\n' && !isComment && !LineComment))
                {
                    //Debug.WriteLine(i + "char is : " + code[i]);
                    setStart(i);

                }
                //  handling comments
                if (code[i] == '/' && (i <= code.Length - 2) && code[i + 1] == '$')
                {
                    isComment = true;
                    continue;
                }
                if (isComment && code[i] == '$' && (i <= code.Length - 2) && code[i + 1] == '/')
                {
                    isComment = false;
                    i++;
                    continue;
                }
                if (code[i] == '$' && code[i+1] == '$' && code[i+2] == '$' )
                {
                    LineComment = true;
                    i += 2;
                    continue;
                }
                if(LineComment && code[i]=='\n')
                {
                    LineComment = false;
                    setEnd(i);
                    token = new String(code.ToCharArray(), start, end);
                    setDetails();
                    token = "";
                    lineNumber++;
                    continue;
                }
                if (isComment||LineComment) continue;
                //--------------------------------
                //handeling the end of the line
                if (code[i] == '\n')
                {
                    lineNumber++;
                    continue;
                }
                //--------------------------------
                //checking the constant
                if (canBeConstant && token.Length != 0 && IsDigit(token[0]) && code[i - 1] != ' ')
                {
                    if (!IsDigit(code[i - 1]))
                    {
                        currentsState = -1;
                        canBeConstant = false;
                    }
                    else
                    {
                        currentsState = (int)State.O;
                    }
                }
                //--------------------------------
                //handeling spaces
                if ((code[i] == ' ' || code[i] == '\t') || code[i] == ',' || code[i] == ';' || (i == code.Length - 1))
                {
                    
                    while (i < code.Length - 1 && code[i + 1] == ' ')
                    {
                        i++;
                    }
                    foreach (State state in states.acceptedStates)
                    {
                        if (state == (State)currentsState) acceptedState = true;
                    }
                    // Handeling Include
                    if(currentsState == (int)State.CF)
                    {
                        if (LinkerController.LinkFiles(code, ref i) == false)
                        {
                            initValues();
                        }
                    }
                    else if (acceptedState)
                    {
                        if (currentsState == (int)State.O)
                        {
                            scannerOutput.Add("Line :" + lineNumber + " Token Text:" + token + "      " + KeyWordsDictionary.keyWordsAndTokens["D"]);
                        }
                        else
                        {
                            scannerOutput.Add("Line :" + lineNumber + " Token Text:" + token + "      " + KeyWordsDictionary.keyWordsAndTokens[token]);
                        }
                        setEnd(i);
                        setDetails();

                        initValues();
                        continue;

                    }
                    else if (currentsState == -1 && code[i] != ' ')
                    {
                        checkIfIdentifierOrErrorValue(filePath,i);
                    }

                }

                //translating throught transition table
                if (currentsState != -1 && code[i] != ' ')
                {
                    if (currentsState != (int)State.O)
                    {
                        currentsState = (int)validTransitions[(int)currentsState, code[i]];
                    }

                    token += code[i];
                }
                else if (currentsState == -1 && code[i] != ' ')
                {
                    token += code[i];
                    continue;
                }
                else
                {
                    checkIfIdentifierOrErrorValue(filePath,i);
                }

            }

            PrintNumErrors();
        }

        public void PrintNumErrors()
        {
            scannerOutput.Add("Total NO of errors: " + totalErrors);
        }

        private void initValues()
        {
            token = "";
            currentsState = (int)State.A;
            acceptedState = false;
            isComment = false;
            canBeConstant = true;
        }

        public void checkIfIdentifierOrErrorValue(string filePath,int i)
        {
            if (token.Length > 0)
            {
                if (checkIdentifier(token))
                {
                    scannerOutput.Add("Line :" + lineNumber + " Token Text:" + token + "      " + "IDENTIFIER");
                    LinkerController.AddIdentifier(token, filePath);
                }
                else
                {
                    scannerOutput.Add("Line :" + lineNumber + " Error in Token :" + token);
                    totalErrors++;
                }

                initValues();
                setEnd(i);
                setDetails();
            }
        }
        public bool IsDigit(char token)
        {
            return numbers.Contains(token);
        }
        public bool IsSpecialCharacter(char token)
        {
            return specialCharacters.Contains(token);
        }

        public bool checkIdentifier(string token)
        {
            bool flag = false;
            for (int i = 1; i <= token.Length; i++)
            {
                if (!IsDigit(token[0]) && !IsSpecialCharacter(token[0])) flag = true;
                if (!IsSpecialCharacter(token[i - 1]) && i <= token.Length - 1)
                {
                    continue;
                }
                else if (!IsSpecialCharacter(token[i - 1]) && flag == true) return true;
                return false;

            }
            return false;
        }

        public void setStart (int i)
        {
            if(start == -1)
            {
                start = i ;
            }
            else if (end != -1)
            {
                start = i;
                end = -1;
                //Debug.WriteLine("---------end = " + end);

            }
            //Debug.WriteLine("IIstart = " +i);
            //Debug.WriteLine("start = " + start);
        }

        public void setEnd (int i)
        {
            end = i;
            //Debug.WriteLine("IIend = "+i);
            //Debug.WriteLine("end = " + end);
        }

        public void setDetails ()
        {
            Tokens.Add(new Dictionary<string, string>()
            {
                { "Name", token },
                { "Start", start.ToString() },
                { "End", end.ToString() }
            });
        }

        public ActionResult Index()
        {
            scanCode("$$$ Iow " +"\n"+"fer");

            foreach (String line in scannerOutput)
            {
                Debug.WriteLine(line);
            }

            foreach (var Tok in Tokens)
            {
                foreach (var item in Tok)
                {
                    Debug.WriteLine(item.Key + " = " + item.Value);
                }
            }


            return View();
        }
    }
}