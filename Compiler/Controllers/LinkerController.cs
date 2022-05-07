using Compiler.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Compiler.Models
{
    public static class LinkerController
    {
        private static Dictionary<string, string> identifierDict = new Dictionary<string, string>();

        public static bool LinkFiles(string Code, ref int charIndex)
        {
            string newCode;
            StringBuilder filePath = new StringBuilder();

            int numQuotes = 0;

            while (numQuotes < 2)
            {
                if (Code[charIndex] != '\"')
                {
                    filePath.Append(Code[charIndex]);
                }
                else
                {
                    numQuotes++;
                }

                charIndex++;
            }

            try
            {
                newCode = File.ReadAllText(filePath.ToString());
            }
            catch (Exception)
            {
                return false;
            }

            ScannerController.Instance.scannerOutput.Add("--->" + filePath);
            ScannerController.Instance.scanCode(newCode, filePath.ToString());
            ScannerController.Instance.scannerOutput.Add("<---" + filePath);

            return true;
        }

        public static bool AddIdentifier(string identifierName, string path)
        {
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
                    return(File.ReadAllText(filePath.ToString()));
                }
                catch (Exception)
                {
                    return "404";
                }
            }
        }
    }
}