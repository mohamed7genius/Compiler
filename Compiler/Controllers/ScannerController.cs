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
        int currentsState;
        int lineNumber = 1;
        int totalErrors = 0;
        bool isComment = false;
        bool acceptedState = false;
        List<String> scannerOutput = new List<string>();
        public void scanCode(String code)
        {
            token = "";
            currentsState = (int)State.A;
            isComment = false;

            int[,] validTransitions = transitionTable.init();

            for (int i=0;i<code.Length;i++)
            {

                //  handling comments
                if (code[i] == '/'&&code[i + 1] == '$')
                {
                        isComment = true;
                        continue;                   

                }
                if (isComment&&code[i] == '$' && (i <= code.Length - 2)&& code[i + 1] == '/')
                {
                        isComment = false;
                        continue;
                    
                }
                if (isComment) continue;
                //--------------------------------
                //handeling the end of the line
                if (code[i] == '\n' || code[i] == ';')
                {
                    lineNumber++;
                    continue;
                }
                //--------------------------------
                //checking the constant
                if (IsDigit(code[i]))
                {
                    currentsState = (int)State.O;
                }
                //--------------------------------
                //handeling spaces
                else if (code[i]==' '||code[i]=='\t')
                {
                    while (i < code.Length-1 &&code[i+1]==' ')
                    {
                        i++;
                    }
                    foreach (State state in states.acceptedStates)
                    {
                        if (state == (State)currentsState) acceptedState = true;
                    }
                    if (acceptedState&&!IsRestrictedValue(token))
                    {
                        if (currentsState == (int)State.O||currentsState == (int)State.AJ)
                        {
                            scannerOutput.Add("Line :" + lineNumber + " Token Text:" + token + "      " + KeyWordsDictionary.keyWordsAndTokens["D"]);
                        }
                        else
                        {
                            scannerOutput.Add("Line :" + lineNumber + " Token Text:" + token + "      " + KeyWordsDictionary.keyWordsAndTokens[token]);
                        }

                        token = "";
                        acceptedState = false;
                        currentsState = (int)State.A;
                        continue;

                    }
                    else if (currentsState==-1&&!acceptedState&& code[i] != ' ')
                    {
                        if (checkIdentifier(token))
                        {
                            scannerOutput.Add("Line :" + lineNumber + " Token Text:" + token + "      " + "IDENTIFIER");
                            token = "";
                        }
                        else
                        {
                            if (!IsRestrictedValue(token))
                            {
                                scannerOutput.Add("Line :" + lineNumber + " Error in Token :" + token);
                                totalErrors++;
                                token = "";
                            }

                        }
                    }

                }

                //translating throught transition table
                if (currentsState != -1 && code[i] != ' ')
                {
                    currentsState = (int)validTransitions[(int)currentsState, code[i]];
                    if(IsDigit(code[i])&&currentsState!=-1)currentsState=(int)State.O;
                    token += code[i];
                }
                else if (currentsState == -1 && code[i] != ' ')
                {
                    token += code[i];
                    continue;
                }
                else
                {
                    if (checkIdentifier(token))
                    {
                        scannerOutput.Add("Line :" + lineNumber + " Token Text:" + token + "      " + "IDENTIFIER");
                    }
                    else
                    {
                        if (!IsRestrictedValue(token))
                        {
                            scannerOutput.Add("Line :" + lineNumber + " Error in Token :" + token);
                            totalErrors++;
                        }

                    }
                    token = "";
                    currentsState = (int)State.A;
                }

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

            scanCode("544 ");
            foreach (String line in scannerOutput)
            {
                Debug.WriteLine(line);
            }





            return View();
        }




    }
}