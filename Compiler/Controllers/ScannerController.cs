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
        bool acceptedState = false;
        bool canBeConstant = true;
        public List<string> scannerOutput = new List<string>();
        public void scanCode(string code, string filePath)
        {
            initValues();

            // DIRTY SOULTION AHEAD TO FIX HAVING TO WRITE A WHITESPACE AT THE END OF (code) ARRAY
            if (code[code.Length - 1] != ' ')
                code += ' ';
            for (int i = 0; i < code.Length; i++)
            {
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
                if (isComment) continue;
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
                 if ((code[i]==' '||code[i]=='\t')||(i==code.Length-1))
                {
                    while (i < code.Length-1 &&code[i+1]==' ')
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

                        initValues();
                        continue;

                    }
                    else if (currentsState==-1&& code[i] != ' ')
                    {
                        checkIfIdentifierOrErrorValue(filePath);
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
                    checkIfIdentifierOrErrorValue(filePath);
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

        public void checkIfIdentifierOrErrorValue(string filePath)
        {
            if(token.Length > 0)
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
            }
        }
        public bool IsDigit(char token)
        {
            foreach ( char number in numbers)
            {
                if (token == number) return true;
            }
            return false;
        }
        public bool IsSpecialCharacter(char token)
        {
            foreach (char character in specialCharacters)
            {
                if (token == character) return true;
            }
            return false;
        }
        public bool checkIdentifier(string token)
        {
            bool flag = false;
            for (int i = 1; i <= token.Length; i++)
            {
                if (!IsDigit(token[0])&&!IsSpecialCharacter(token[0])) flag=true;
                if (!IsSpecialCharacter(token[i-1]) && i < token.Length - 1)
                {
                    continue;
                }
                else if (!IsSpecialCharacter(token[i-1])&&flag==true) return true;
                return false;

            }
            return false;
        }

        public ActionResult Index()
        {
            Instance = this;
            //State state = (State)83; // CF
            scanCode("12i Include \"C:\\Users\\Mohammed Khalid\\Desktop\\test.txt\" 4444i444554 534223 ID 5342i23 ", null);
            foreach (String line in scannerOutput)
            {
                Debug.WriteLine(line);
            }

            Debug.WriteLine(LinkerController.GetIdentifierCode("ID3"));

            return View();
        }
    }
}