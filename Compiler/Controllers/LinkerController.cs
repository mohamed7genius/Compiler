using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Compiler.Models
{
    public static class LinkerController
    {
        public static Dictionary<string, string> IdentifierDict = new Dictionary<string, string>();

        public static void LinkFiles(ref String Code, int charIndex)
        {
            StringBuilder outputString = new StringBuilder();
            StringBuilder filePath = new StringBuilder();
            int startIndex = charIndex - 7;
            int endIndex;
            
            int numQuotes = 0;

            while (numQuotes < 2)
            {
                if (numQuotes > 0)
                {
                    filePath.Append(Code[charIndex]);
                }

                if (Code[charIndex++] == '\"')
                {
                    numQuotes++;
                }
            }

            endIndex = charIndex;
            filePath.Replace("\"", "");

            var lines = System.IO.File.ReadLines(filePath.ToString());

            for (int i = 0; i < Code.Length; i++)
            {
                if (i < startIndex || i > endIndex)
                {
                    outputString.Append(Code[i]);
                }
                else if (i == startIndex)
                {
                    outputString.Append("       ");
                    foreach (var line in lines)
                    {
                        outputString.Append(line + " \n ");
                    }
                }
            }

            foreach (string word in outputString.ToString().Split())
            {
                if(word == "Include")
                {
                    outputString.Remove(outputString.Length - 1, 1);
                }
            }

            //Debug.WriteLine(outputString.ToString());

            Code = outputString.ToString();
        }

        public static int AddIdentifier(string identifierName, string path)
        {
            try
            {
                IdentifierDict.Add(identifierName, path);
                return 0;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public static string GetIdentifierFilePath(string identifierName)
        {
            try
            {
                return (IdentifierDict[identifierName]);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}