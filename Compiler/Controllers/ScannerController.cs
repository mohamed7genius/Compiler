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
        char[] numbers =
        {
            '0','1','2','3','4','5','6','7','8','9'
        };
        char[] specialCharacters =
        {
            '!','-','%','*','#','\'','^','+','/','(',')','=','{','}','[',']','|',':',';','<','>',',',',','.','\"','$','@','~','&'
        };
        char[] restrictedValuesForToken =
            {
            ' ','\n',';'
        };
        string token;
        int[,] validTransitions = transitionTable.init();
        int currentsState;  
        int lineNumber = 1;  
        int totalErrors = 0;
        bool isComment = false;
        bool acceptedState = false;
        bool canBeConstant = true;
        List<String> scannerOutput = new List<string>();
        public void scanCode(String code)
        {
            token = "";
            currentsState = (int)State.A;
            isComment = false;


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
                    if (acceptedState)
                    {
                        if (currentsState == (int)State.O)
                        {
                            scannerOutput.Add("Line :" + lineNumber + " Token Text:" + token + "      " + KeyWordsDictionary.keyWordsAndTokens["D"]);
                        }
                        else
                        {
                            scannerOutput.Add("Line :" + lineNumber + " Token Text:" + token + "      " + KeyWordsDictionary.keyWordsAndTokens[token]);
                        }

                        token = "";
                        canBeConstant = true;
                        acceptedState = false;
                        currentsState = (int)State.A;
                        continue;

                    }
                    else if (currentsState==-1&& code[i] != ' ')
                    {
                        checkIfIdentifierOrErrorValue();
                    }

                }

                //translating throught transition table
                if (currentsState != -1 && code[i] != ' ')
                {
                    if (currentsState != (int)State.O)
                    {
                        currentsState = (int)validTransitions[(int)currentsState, code[i]];
                    }
                    
                    //if(IsDigit(code[i]))currentsState=(int)State.O;
                    token += code[i];
                }
                else if (currentsState == -1 && code[i] != ' ')
                {
                    token += code[i];
                    continue;
                }
                else
                {
                    checkIfIdentifierOrErrorValue();
                }
                

            }
            scannerOutput.Add("Total NO of errors: " + totalErrors);

        }


        public void checkIfIdentifierOrErrorValue()
        {
            if(token.Length > 0)
            {
                if (checkIdentifier(token))
                {
                    scannerOutput.Add("Line :" + lineNumber + " Token Text:" + token + "      " + "IDENTIFIER");
                }
                else
                {

                    scannerOutput.Add("Line :" + lineNumber + " Error in Token :" + token);
                    totalErrors++;
                }
                token = "";
                canBeConstant = true;
                currentsState = (int)State.A;
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
        public bool IsRestrictedValue(string token)
        {
            foreach (char charater in restrictedValuesForToken)
            {
                foreach (char tokenChar in token)
                {
                    if (charater == tokenChar) return true;
                }

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
            scanCode("6557i465  4444i444554 int 4444i444554 534223 5342i23 ");
            foreach (String line in scannerOutput)
            {
                Debug.WriteLine(line);
            }





            return View();
        }




    }
}