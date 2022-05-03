using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Compiler.Models
{
    public  static class KeyWordsDictionary
    {
        public static Dictionary<String, String> keyWordsAndTokens = new Dictionary<string, string>
        {
            { "If","Condition" },
            { "Else","Condition" },
            { "Iow","Integer" },
            { "SIow","SInteger" },
            { "Chlo","Character" },
            { "Chain","String" },
            { "Iowf","Float" },
            { "SIowf","SFloat" },
            { "Worthless","Void" },
            { "Loopwhen","Loop" },
            { "Iteratewhen","Loop" },
            { "Turnback","Return" },
            { "Stop","Break" },
            { "Loli","Struct" },
            { "+","Arithmetic Operation" },
            { "-","Arithmetic Operation" },
            { "/","Arithmetic Operation" },
            { "*","Arithmetic Operation" },
            {"&&", "Logic operators" },
            {"||", "Logic operators" },
            {"~", "Logic operators" },
            {"==", "relational operators" },
            {"<", "relational operators" },
            {">", "relational operators" },
            {"!=", "relational operators" },
            {"<=", "relational operators" },
            {">=", "relational operators" },
            {"=", "Assignment operator" },
            {"->", "Access Operator" },
            {"{", "Braces" },
            {"}", "Braces" },
            {"[", "Braces" },
            {"]", "Braces" },
            {"(", "Braces" },
            {")", "Braces" },
            {"D","constant" },
            {"\'","Quotation Mark" },
            {"\"","Quotation Mark" },
            {"Include","Inclusion" },


        };




    }
}