using Compiler.Helper_Classes;
using Compiler.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

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
            '!','-','%','*','#','\'','^','+','/','(',')','=','{','}','[',']','|',':',';','<','>',',',',','.','\"','$','@','~','&','?'

        };
        string token;
        int[,] validTransitions = TransitionTable.init();
        int currentState;
        int lineNumber = 1;
        int totalErrors = 0;
        Comment comment = Comment.NoComment;
        bool acceptedState = false;
        bool canBeConstant = true;
        public List<string> scannerOutput = new List<string>();
        public List<string> tokensOutput = new List<string>();

        int tokenStartIndex = -1;
        int tokenEndIndex = -1;

        const int CONSTANT_STATE = (int)State.O;
        const int INCLUDE_STATE = (int)State.CF;
        const int INVALID_STATE = -1;

        List<Dictionary<string, string>> Tokens = new List<Dictionary<string, string>>();
        List<Dictionary<string, string>> Errors = new List<Dictionary<string, string>>();

        enum Comment
        {
            NoComment,
            SingleLine,
            MultiLine,
        }

        public void ScanCode(string code, string? filePath)
        {
            code=replace('\r',' ',code);
            InitValues();


            for (int i = 0; i < code.Length; i++)
            {
                //  handling comments
                if (comment != Comment.NoComment)
                {
                    if ((comment == Comment.MultiLine && CheckMultilineCommentEnd(code, ref i)) || (comment == Comment.SingleLine && CheckSingleLineCommentEnd(code, i)))
                    {
                        comment = Comment.NoComment;
                    }

                    continue;
                }
                else
                {
                    if(ChecktMultilineCommentStart(code, i))
                    {
                        comment = Comment.MultiLine;
                    }

                    if (CheckSingleLineCommentStart(code, i))
                    {
                        i += 2;
                        comment = Comment.SingleLine;
                    }

                    if (comment != Comment.NoComment)
                        continue;
                }

                //--------------------------------
                //handeling the end of the line
                if (EndOfLine(code, i))
                {
                    continue;
                }

                //--------------------------------
                //setting index of the beginning of each token
                if (!IsWhiteSpace(code[i]) && code[i] != '\t' && code[i] != ',' && code[i] != ';')
                {

                    SetTokenStartIndex(i);
                }

                //--------------------------------
                //checking the constant
                if (canBeConstant && token.Length != 0)
                {
                    CheckConstant(code, token, i);
                }
                //--------------------------------
                //handeling spaces
                if (IsWhiteSpace(code[i]) || code[i] == '\t' || code[i] == ',' || (i == code.Length - 1))
                {
                    SkipSpaces(code, ref i);

                    CheckAcceptedState();
                    // Handeling Include
                    if (currentState == INCLUDE_STATE)
                    {
                        Include(code, ref i);
                    }
                    else if (acceptedState)
                    {
                        if (currentState == CONSTANT_STATE)
                        {
                            AddToTokens(token);
                            AddMessageToOutput("Token Text: " + token + "      " + KeyWordsDictionary.keyWordsAndTokens["D"]);

                        }
                        else
                        {
                            AddToTokens(token);
                            AddMessageToOutput("Token Text: " + token + "      " + KeyWordsDictionary.keyWordsAndTokens[token]);

                        }
                        SetTokenEndIndex(i);
                        SetTokenDetails();
                        InitValues();
                    }
                    else if (!CheckValidState() && IsWhiteSpace(code[i]))
                    {
                        CheckIfIdentifierOrErrorValue(filePath, i);
                    }


                }

                if(!IsWhiteSpace(code[i]))
                {
                    if (i > 0 && code[i-1] == '\n')
                    {
                        InitValues();
                    }
                    token += code[i];

                    if (CheckValidState())
                    {
                        if (currentState != CONSTANT_STATE)
                        {
                            currentState = (int)validTransitions[(int)currentState, code[i]];
                        }
                    }
                }
            }

            if (filePath == null)
            {
                PrintNumErrors();
            }

        }


        private string  replace(char oldChar,char newchar ,string? code)
        {
            string? codeReplaced="";
            for (int i = 0; i < code!.Length; i++)
            {
                if (code[i] == oldChar)
                {
                    codeReplaced += newchar;
                }
                else
                {
                    codeReplaced += code[i];
                }          
            }
            return codeReplaced;
        }

        private bool IsWhiteSpace(char c)
        {
            return c == ' ' || c == (char)160;
        }

        private bool CheckSingleLineCommentEnd(string code, int i)
        {
            if (code[i] == '\n')
            {
                comment = Comment.NoComment;
                SetTokenEndIndex(i);
                token = new String(code.ToCharArray(), tokenStartIndex, tokenEndIndex);
                SetTokenDetails();
                InitValues();
                lineNumber++;

                return true;
            }

            return false;
        }

        private bool CheckMultilineCommentEnd(string code, ref int i)
        {
            if (code[i] == '$' && (i <= code.Length - 2) && code[i + 1] == '/')
            {
                //isComment = false;
                i++;
                InitValues();
                return true;
            }

            return false;
        }

        private bool ChecktMultilineCommentStart(string code, int i)
        {
            if (code[i] == '/' && (i <= code.Length - 2) && code[i + 1] == '$')
            {
                return true;
               
            }

            return false;
        }

        private bool CheckSingleLineCommentStart(string code, int i)
        {
            if (i <= code.Length - 3 && code[i] == '$' && code[i + 1] == '$' && code[i + 2] == '$')
            {
                SetTokenStartIndex(i);
                return true;
                
            }

            return false;
        }


        private void CheckAcceptedState()
        {
            foreach (State state in States.acceptedStates)
            {
                if ((State)currentState == state)
                {
                    acceptedState = true;
                }
            }
        }

        private bool CheckValidState()
        {
            return currentState != INVALID_STATE;
        }

        private void SkipSpaces(string code, ref int i)
        {
            while (i < code.Length - 1 && IsWhiteSpace(code[i + 1]))
            {
                i++;
            }
        }

        private bool EndOfLine(string code, int i)
        {
            if (code[i] == ';')
            {
                AddToTokens(";");
                return true;
            }
 
            else if (code[i] == '\n')
            {
                lineNumber++;
                return true;
            }

            return false;
        }

        private void Include(string code, ref int i)
        {
            if (Linker.LinkFiles(code, ref i) == false)
            {
                InitValues();
            }
        }

        private void CheckConstant(string code, string token, int currentCodeIndex)
        {
            char firstCharInToken = token[0];
            char currentChar = code[currentCodeIndex - 1];

            if (IsDigit(firstCharInToken) && currentChar != ' ')
            {
                if (IsDigit(currentChar))
                {
                    currentState = (int)State.O;
                }
                else
                {
                    currentState = INVALID_STATE;
                    canBeConstant = false;
                }
            }
        }

        public void PrintNumErrors()
        {
            scannerOutput.Add("Total NO of errors: " + totalErrors);
        }

        private void InitValues()
        {
            token = "";
            currentState = (int)State.A;
            acceptedState = false;
            comment = Comment.NoComment;
            canBeConstant = true;
        }

        public void CheckIfIdentifierOrErrorValue(string? filePath, int i)
        {
            if (token.Length > 0)
            {
                if (CheckIdentifier(token))
                {
                    AddToTokens(token);
                    AddMessageToOutput("Token Text: " + token + "      " + "IDENTIFIER");
                    //scannerOutput.Add("Line :" + lineNumber + " Token Text:" + token + "      " + "IDENTIFIER");
                    Linker.AddIdentifier(token, filePath);
                }
                else
                {
                    AddToTokens(token);
                    AddMessageToOutput("Error in Token :" + token);

                    //scannerOutput.Add("Line :" + lineNumber + " Error in Token :" + token);
                    SetTokenEndIndex(i);
                    SetErrorDetails();
                    totalErrors++;
                }

                SetTokenEndIndex(i);
                SetTokenDetails();
                InitValues();

            }
        }
        
        private void AddMessageToOutput(string message)
        {
            scannerOutput.Add("Line :" + lineNumber + " " + message);
        }

        private void AddToTokens(string token)
        {
            tokensOutput.Add(token);
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

        public void SetTokenStartIndex(int i)
        {
            if (tokenStartIndex == -1)
            {
                tokenStartIndex = i;
            }
            else if (tokenEndIndex != -1)
            {
                tokenStartIndex = i;
                tokenEndIndex = -1;

            }

        }

        public void SetTokenEndIndex(int i)
        {
            tokenEndIndex = i;
        }

        public void SetTokenDetails()
        {
            Tokens.Add(new Dictionary<string, string>()
            {
                { "Name", token },
                { "Start", tokenStartIndex.ToString() },
                { "End", tokenEndIndex.ToString() }
            });
        }
        
        public void SetErrorDetails()
        {
            Errors.Add(new Dictionary<string, string>()
            {
                { "Name", token },
                { "Start", tokenStartIndex.ToString() },
                { "End", tokenEndIndex.ToString() }
            });
        }

        [HttpPost]
        public IActionResult Index([FromForm]string code, [FromForm]string? filePath)
        {
            Instance = this;

            Debug.WriteLine(Request.Body);

            Linker.identifierDict.Clear();

            ScanCode(code, filePath);

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

            Debug.WriteLine("ERRRRRORRRRRRR");

            foreach (var error in Errors)
            {
                foreach (var item in error)
                {
                    Debug.WriteLine(item.Key + " = " + item.Value);
                }
            }
            return Json(new { status = 200, output = scannerOutput, tokens = tokensOutput, indexes = Tokens });
        }

        [HttpPost]
        public IActionResult ScanHiddenFile(IFormFile file)
        {
            string text;

            if (file == null)
                return Json(new { status = 404 });
            try
            {
                text = ReadFormFile(file).ToString();

                if(text.Length <= 0)
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
