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

        int startToken = -1;
        int endToken = -1;

        List<Dictionary<string, string>> Tokens = new List<Dictionary<string, string>>();

        public void ScanCode(string code, string filePath)
        {
            InitValues();

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
                    SetStart(i);

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
                    SetEnd(i);
                    token = new String(code.ToCharArray(), startToken, endToken);
                    SetDetails();
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
                            InitValues();
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
                        SetEnd(i);
                        SetDetails();
                        InitValues();
                        continue;

                    }
                    else if (currentsState == -1 && code[i] != ' ')
                    {
                        CheckIfIdentifierOrErrorValue(filePath,i);
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
                    CheckIfIdentifierOrErrorValue(filePath,i);
                }

            }

            if (filePath == null)
            {
                PrintNumErrors();
            }
            
        }

        public void PrintNumErrors()
        {
            scannerOutput.Add("Total NO of errors: " + totalErrors);
        }

        private void InitValues()
        {
            token = "";
            currentsState = (int)State.A;
            acceptedState = false;
            isComment = false;
            canBeConstant = true;
        }

        public void CheckIfIdentifierOrErrorValue(string filePath,int i)
        {
            if (token.Length > 0)
            {
                if (CheckIdentifier(token))
                {
                    scannerOutput.Add("Line :" + lineNumber + " Token Text:" + token + "      " + "IDENTIFIER");
                    LinkerController.AddIdentifier(token, filePath);
                }
                else
                {
                    scannerOutput.Add("Line :" + lineNumber + " Error in Token :" + token);
                    totalErrors++;
                }

                InitValues();
                SetEnd(i);
                SetDetails();
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

        public bool CheckIdentifier(string token)
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

        public void SetStart (int i)
        {
            if(startToken == -1)
            {
                startToken = i ;
            }
            else if (endToken != -1)
            {
                startToken = i;
                endToken = -1;
                //Debug.WriteLine("---------end = " + end);

            }
            //Debug.WriteLine("IIstart = " +i);
            //Debug.WriteLine("start = " + start);
        }

        public void SetEnd (int i)
        {
            endToken = i;
            //Debug.WriteLine("IIend = "+i);
            //Debug.WriteLine("end = " + end);
        }

        public void SetDetails ()
        {
            Tokens.Add(new Dictionary<string, string>()
            {
                { "Name", token },
                { "Start", startToken.ToString() },
                { "End", endToken.ToString() }
            });
        }

        [HttpPost]
        public ActionResult Index(string code)
        {
            Instance = this;

            ScanCode(code, null);

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


            return Json(new { status = 200, data = scannerOutput });
        }
    }
}