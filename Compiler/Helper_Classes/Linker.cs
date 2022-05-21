using Compiler.Controllers;
using System.Text;

namespace Compiler.Helper_Classes
{
    public static class Linker
    {
        public static Dictionary<string, string> identifierDict = new Dictionary<string, string>();

        public static int LinkFiles(string Code, ref int charIndex)
        {
            string newCode;
            
            StringBuilder filePath = new StringBuilder();

            int numQuotes = 0;

            while (numQuotes < 2 && Code[charIndex] != '\n' && Code[charIndex] != ';' && charIndex < Code.Length)
            {
                if (numQuotes == 0 && ScannerController.Instance.IsWhiteSpace(Code[charIndex]))
                {
                    if(charIndex < Code.Length -  1)
                    {
                        charIndex++;
                        continue;
                    }
                    else
                    {
                        return 2;
                    }
                }
                if (numQuotes > 0 && Code[charIndex] != '\"')
                {
                    filePath.Append(Code[charIndex]);
                }
                else if(Code[charIndex] == '\"')
                {
                    numQuotes++;
                }
                else
                {
                    return 2;
                }

                charIndex++;
            }

            if (numQuotes < 2 || filePath == null)
            {
                if(Code[charIndex] == ';')
                    charIndex++;
                return 2;
            }

            try
            {
                newCode = File.ReadAllText(filePath.ToString());
                //newCode = newCode.Replace("\r\n", " \r\n ");
                addSpaceBeforeLineEnd(ref newCode);

            }
            catch (Exception)
            {
                return 1;
            }

            ScannerController.Instance.scannerOutput.Add("--->" + filePath);
            ScannerController.Instance.ScanCode(newCode, filePath.ToString());
            ScannerController.Instance.scannerOutput.Add("<---" + filePath);

            return 0;
        }

        public static bool AddIdentifier(string identifierName, string? path)
        {
            if(path == "hidden")
                return false;
            try
            {
                identifierDict.Add(identifierName, path);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string GetIdentifierFilePath(string identifierName)
        {
            try
            {
                return (identifierDict[identifierName]);
            }
            catch (Exception)
            {
                return "404";
            }
        }

        public static string GetIdentifierCode(string identifierName)
        {
            string filePath = GetIdentifierFilePath(identifierName);

            if (filePath == null || filePath == "404")
            {
                return filePath;
            }
            else
            {
                try
                {
                    return (File.ReadAllText(filePath.ToString()));
                }
                catch (Exception)
                {
                    return "404";
                }
            }
        }

        private static void addSpaceBeforeLineEnd(ref string code)
        {
            StringBuilder newStr = new StringBuilder();
            bool newLineFlag = false;
            for (int i = 0; i < code.Length; i++)
            {
                if (code[i] == '\n' || code[i] == '\r')
                {
                    newLineFlag = true;
                    continue;
                }

                if (newLineFlag)
                {
                    newStr.Append(" \r\n");
                    newLineFlag = false;
                }

                newStr.Append(code[i]);
            }

            code = newStr.ToString();
        }
    }
}
