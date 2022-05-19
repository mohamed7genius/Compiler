
namespace Compiler.Models
{
    public class ParsingTable
    {

        static String[,] table = new string[56, 36];
        public static String[,] init()
        {

            for (int row = 0; row < 99; row++)
            {
                for (int column = 0; column < 127; column++)
                {
                    table[row, column] = null;
                }
            }
            string[] NonTerminal =
        {
            "program",
            "declaration-list",
            "declaration-list’",
            "declaration",
            "declaration’",
            "var",
            "var-declaration",
            "type-specifier",
            "fun-declaration",
            "params",
            "param-list",
            "param-list’",
            "param",
            "compound-stmt",
            "compound-stmt’",
            "local-declarations",
            "local-declarations’",
            "statement-list",
            "statement-list’",
            "statement",
            "expression-stmt",
            "selection-stmt",
            "selection-stmt’",
            "iteration-stmt",
            "Loop-statement",
            "Iterate-statement",
            "jump-stmt",
            "expression",
            "expression’",
            "id-assign",
            "simple-expression",
            "simple-expression’",
            "relop",
            "relop’",
            "additive-expression",
            "additive-expression’",
            "addop",
            "term",
            "term’",
            "mulop",
            "factor",
            "factor’",
            "call",
            "args",
            "arg-list",
            "arg-list’",
            "num",
            "Unsigned num",
            "Signed num",
            "pos-num",
            "neg-num",
            "value",
            "comment",
            "include_command",
            "F_name"
        };
            string[] terminal =
        {
            "(",
            "Iow",
            "SIow",
            "Chlo",
            "Chain",
            "Iowf",
            "SIowf",
            "Worthless",
            "include",
            "ID",
            "+",
            "-",
            "*",
            "$(comment)",
            "/",
            "{",
            "#",
            ")",
            ",",
            "}",
            "INT_NUM",
            "FLOAT_NUM",
            ";",
            "if",
            "Loopwhen",
            "Iteratewhen",
            "Turnback",
            "Stop",
            "else",
            "=",
            "<",
            ">",
            "!",
            "&",
            "|",
            "STR",
        };
            table[0, 1] = "declaration-list";
            table[0, 2] = "declaration-list";
            table[0, 3] = "declaration-list";
            table[0, 4] = "declaration-list";
            table[0, 5] = "declaration-list";
            table[0, 6] = "declaration-list";
            table[0, 7] = "declaration-list";
            table[0, 8] = " include_command";

            table[0, 13] = "comment";
            table[0, 14] = "comment";
   
            table[1, 1] = "declaration declaration-list’";
            table[1, 2] = "declaration declaration-list’";
            table[1, 3] = "declaration declaration-list’";
            table[1, 4] = "declaration declaration-list’";
            table[1, 5] = "declaration declaration-list’";
            table[1, 6] = "declaration declaration-list’";
            table[1, 7] = "declaration declaration-list’";
            table[1, 8] = "declaration declaration-list’";

            table[1, 13] = "declaration declaration-list’";
            table[1, 14] = "declaration declaration-list’";

            table[2, 1] = "declaration declaration-list’";
            table[2, 2] = "declaration declaration-list’";
            table[2, 3] = "declaration declaration-list’";
            table[2, 4] = "declaration declaration-list’";
            table[2, 5] = "declaration declaration-list’";
            table[2, 6] = "declaration declaration-list’";
            table[2, 7] = "declaration declaration-list’";

            table[2, 13] = "declaration declaration-list’";
            table[2, 14] = "declaration declaration-list’";

            table[2, 16] = "";

            table[3, 1] = "type-specifier ID declaration’";
            table[3, 2] = "type-specifier ID declaration’";
            table[3, 3] = "type-specifier ID declaration’";
            table[3, 4] = "type-specifier ID declaration’";
            table[3, 5] = "type-specifier ID declaration’";
            table[3, 6] = "type-specifier ID declaration’";
            table[3, 7] = "type-specifier ID declaration’";


            table[3, 13] = "type-specifier ID declaration’";
            table[3, 14] = "type-specifier ID declaration’";
          
            table[4, 0] = "fun-declaration";

            table[4, 22] = "var";



            table[5, 22] = ";";

 

            table[6, 1] = "type-specifier ID ;";
            table[6, 2] = "type-specifier ID ;";
            table[6, 3] = "type-specifier ID ;";
            table[6, 4] = "type-specifier ID ;";
            table[6, 5] = "type-specifier ID ;";
            table[6, 6] = "type-specifier ID ;";
            table[6, 7] = "type-specifier ID ;";


            table[7, 1] = "Iow";
            table[7, 2] = "SIow";
            table[7, 3] = "Chlo";
            table[7, 4] = "Chain";
            table[7, 5] = "Iowf";
            table[7, 6] = "SIowf";
            table[7, 7] = "Worthless";
 
            table[8, 0] = "( params ) compound-stmt";
   

            table[9, 1] = " param-list";
            table[9, 2] = " param-list";
            table[9, 3] = " param-list";
            table[9, 4] = " param-list";
            table[9, 5] = " param-list";
            table[9, 6] = " param-list";
            table[9, 7] = " param-list";

            table[9, 17] = "";
 

            table[10, 1] = "param param-list’";
            table[10, 2] = "param param-list’";
            table[10, 3] = "param param-list’";
            table[10, 4] = "param param-list’";
            table[10, 5] = "param param-list’";
            table[10, 6] = "param param-list’";
            table[10, 7] = "param param-list’";
   

            table[11, 17] = "";
            table[11, 18] = ", param param-list’ ";
 

            table[12, 1] = "type-specifier ID";
            table[12, 2] = "type-specifier ID";
            table[12, 3] = "type-specifier ID";
            table[12, 4] = "type-specifier ID";
            table[12, 5] = "type-specifier ID";
            table[12, 6] = "type-specifier ID";
            table[12, 7] = "type-specifier ID";

            table[13, 15] = "{ compound-stmt’";
        
            table[14, 0] = "local-declarations statement-list }";
            table[14, 1] = "local-declarations statement-list }";
            table[14, 2] = "local-declarations statement-list }";
            table[14, 3] = "local-declarations statement-list }";
            table[14, 4] = "local-declarations statement-list }";
            table[14, 5] = "local-declarations statement-list }";
            table[14, 6] = "local-declarations statement-list }";
            table[14, 7] = "local-declarations statement-list }";

            table[14, 9] = "local-declarations statement-list }";
            table[14, 10] = "local-declarations statement-list }";
            table[14, 11] = "local-declarations statement-list }";
   
            table[14, 13] = "comment local-declarations statement-list";
            table[14, 14] = "comment local-declarations statement-list";
            table[14, 15] = "local-declarations statement-list }";
     
            table[14, 19] = "local-declarations statement-list }";
            table[14, 20] = "local-declarations statement-list }";
            table[14, 21] = "local-declarations statement-list }";
            table[14, 22] = "local-declarations statement-list }";
            table[14, 23] = "local-declarations statement-list }";
            table[14, 24] = "local-declarations statement-list }";
            table[14, 25] = "local-declarations statement-list }";
            table[14, 26] = "local-declarations statement-list }";
            table[14, 27] = "local-declarations statement-list }";
       
            table[15, 0] = "local-declarations’";
            table[15, 1] = "local-declarations’";
            table[15, 2] = "local-declarations’";
            table[15, 3] = "local-declarations’";
            table[15, 4] = "local-declarations’";
            table[15, 5] = "local-declarations’";
            table[15, 6] = "local-declarations’";
            table[15, 7] = "local-declarations’";

            table[15, 9] = "local-declarations’";
            table[15, 10] = "local-declarations’";
            table[15, 11] = "local-declarations’";

            table[15, 13] = "local-declarations’";
            table[15, 14] = "local-declarations’";
            table[15, 15] = "local-declarations’";
            table[15, 16] = "local-declarations’";

            table[15, 19] = "local-declarations’";
            table[15, 20] = "local-declarations’";
            table[15, 21] = "local-declarations’";
            table[15, 22] = "local-declarations’";
            table[15, 23] = "local-declarations’";
            table[15, 24] = "local-declarations’";
            table[15, 25] = "local-declarations’";
            table[15, 26] = "local-declarations’";
            table[15, 27] = "local-declarations’";
            table[15, 28] = "local-declarations’";
       
            table[16, 0] = "";
            table[16, 1] = "var-declaration local-declarations’";
            table[16, 2] = "var-declaration local-declarations’";
            table[16, 3] = "var-declaration local-declarations’";
            table[16, 4] = "var-declaration local-declarations’";
            table[16, 5] = "var-declaration local-declarations’";
            table[16, 6] = "var-declaration local-declarations’";
            table[16, 7] = "var-declaration local-declarations’";
            
            table[16, 9] = "";
            table[16, 10] = "";
            table[16, 11] = "";
            
            table[16, 13] = "";
            table[16, 14] = "";
            table[16, 15] = "";
            table[16, 16] = "";
  

            table[16, 19] = "";
            table[16, 20] = "";
            table[16, 21] = "";
            table[16, 22] = "";
            table[16, 23] = "";
            table[16, 24] = "";
            table[16, 25] = "";
            table[16, 26] = "";
            table[16, 28] = "";
          
            table[17, 0] = "statement-list’";

            table[17, 9] = "statement-list’";
            table[17, 10] = "statement-list’";
            table[17, 11] = "statement-list’";

            table[17, 15] = "statement-list’";

            table[17, 19] = "statement-list’";
            table[17, 20] = "statement-list’";
            table[17, 21] = "statement-list’";
            table[17, 22] = "statement-list’";
            table[17, 23] = "statement-list’";
            table[17, 24] = "statement-list’";
            table[17, 25] = "statement-list’";
            table[17, 26] = "statement-list’";
            table[17, 27] = "statement-list’";
        
            table[18, 0] = "statement statement-list’";

            table[18, 9] = "statement statement-list’";
            table[18, 10] = "statement statement-list’";
            table[18, 11] = "statement statement-list’";

            table[18, 15] = "statement statement-list’ ";

            table[18, 19] = "";
            table[18, 20] = "statement statement-list’ ";
            table[18, 21] = "statement statement-list’ ";
            table[18, 22] = "statement statement-list’ ";
            table[18, 23] = "statement statement-list’ ";
            table[18, 24] = "statement statement-list’ ";
            table[18, 25] = "statement statement-list’ ";
            table[18, 26] = "statement statement-list’ ";
            table[18, 27] = "statement statement-list’ ";
                 
            table[19, 0] = "expression-stmt";

            table[19, 9] = "expression-stmt";
            table[19, 10] = "expression-stmt";
            table[19, 11] = "expression-stmt";
         
            table[19, 15] = "compound-stmt";

            table[19, 20] = "compound-stmt";
            table[19, 21] = "compound-stmt";
            table[19, 22] = "compound-stmt";
            table[19, 23] = "selection-stmt";
            table[19, 24] = "iteration-stmt";
            table[19, 25] = "iteration-stmt";
            table[19, 26] = "jump-stmt";
            table[19, 27] = "jump-stmt";
         
            table[20, 0] = "expression ;";

            table[20, 9] = "expression ;";
            table[20, 10] = "expression ;";
            table[20, 11] = "expression ;";


            table[20, 20] = "expression ;";
            table[20, 21] = "expression ;";
            table[20, 22] = "expression ;";
          

      
            table[21, 23] = "if ( expression ) statement selection-stmt’";



            table[22, 0] = "";

            table[22, 9] = "";
            table[22, 10] = "";
            table[22, 11] = "";
       
            table[22, 15] = "";
    
            table[22, 19] = "";
            table[22, 20] = "";
            table[22, 21] = "";
            table[22, 22] = "";
            table[22, 23] = "";
            table[22, 24] = "";
            table[22, 25] = "";
            table[22, 26] = "";
            table[22, 27] = "";
            table[22, 28] = "else statement";



            table[23, 24] = "Loop-statement";
            table[23, 25] = "Iterate-statement";



            table[24, 25] = "Loopwhen ( expression ) statement";

            table[25, 25] = "Iteratewhen ( expression ; expression ; expression ) statement";


            table[26, 26] = "Turnback expression ;";
            table[26, 27] = "Turnback expression ;";


            table[27, 0] = "simple-expression";
 
            table[27, 9] = "id-assign expression’";
            table[27, 10] = "id-assign expression’";
            table[27, 11] = "id-assign expression’";
      
            table[27, 20] = "simple-expression";
            table[27, 21] = "simple-expression";



            table[28, 17] = "";
            table[28, 18] = "";

            table[28, 22] = "";

            table[28, 29] = "= expression";


            table[29, 9] = "ID";
       

            table[30, 0] = "additive-expression simple-expression’";


            table[30, 9] = "additive-expression simple-expression’";
            table[30, 10] = "additive-expression simple-expression’";
            table[30, 11] = "additive-expression simple-expression’";

            table[30, 21] = "additive-expression simple-expression’";
            table[30, 20] = "additive-expression simple-expression’";



            table[31, 17] = "";
            table[31, 18] = "";

            table[31, 22] = "";
   
            table[31, 29] = "relop additive-expression";
            table[31, 30] = "relop additive-expression";
            table[31, 31] = "relop additive-expression";
            table[31, 32] = "relop additive-expression";
            table[31, 33] = "relop additive-expression";
            table[31, 34] = "relop additive-expression";


            table[32, 29] = "= relop’";
            table[32, 30] = " < relop’";
            table[32, 31] = "> relop’";
            table[32, 32] = "!relop’";
            table[32, 33] = "&&";
            table[32, 34] = "||";

            table[33, 0] = "";


            table[33, 9] = "";
            table[33, 10] = "";
            table[33, 11] = "";

            table[33, 20] = "";
            table[33, 21] = "";

            table[33, 29] = "=";


            table[34, 0] = "term additive-expression’";

            table[34, 9] = "term additive-expression’";
            table[34, 10] = "term additive-expression’";
            table[34, 11] = "term additive-expression’";

            table[34, 20] = "term additive-expression’";
            table[34, 21] = "term additive-expression’";

            table[35, 0] = "";
                  
            table[35, 9] = "";
            table[35, 10] = "addop term additive-expression’";
            table[35, 11] = "addop term additive-expression’";
                  
            table[35, 17] = "";
            table[35, 18] = "";
            table[35, 20] = "";
            table[35, 21] = "";
         
            table[36, 10] = "+";
            table[36, 11] = "-";


            table[37, 0] = "factor term’";
 
            table[37, 9] = "factor term’";
            table[37, 10] = "factor term’";
            table[37, 11] = "factor term’";

            table[37, 20] = "factor term’";
            table[37, 21] = "factor term’";

            table[38, 0] = "";

            table[38, 9] = "";
            table[38, 10] = "";
            table[38, 11] = "";
            table[38, 12] = "mulop";
      
            table[38, 14] = "mulop";
 
            table[38, 17] = "";
            table[38, 18] = "";
   
            table[38, 20] = "";
            table[38, 21] = "";
            table[38, 22] = "";

            table[38, 35] = "";


            table[39, 12] = "*";
            
            table[39, 14] = "/";
            

            table[40, 0] = "( expression )";

            table[40, 9] = "ID factor’";
            table[40, 10] = "num";
            table[40, 11] = "num";

            table[40, 20] = "num";
            table[40, 21] = "num";


            table[41, 0] = "call";

            table[41, 9] = "";
            table[41, 10] = "";
            table[41, 11] = "";
            table[41, 12] = "";

            table[41, 17] = "";
            table[41, 18] = "";

            table[41, 20] = "";
            table[41, 21] = "";
            table[41, 22] = "";



            table[42, 0] = "(argas)";


            table[43, 0] = "arg-list";

            table[43, 9] = "arg-list";
            table[43, 10] = "arg-list";
            table[43, 11] = "arg-list";

            table[43, 17] = "";

            table[43, 20] = "arg-list";
            table[43, 21] = "arg-list";
   

            table[44, 0] = "expression arg-list’";
   
            table[44, 9] = "expression arg-list’";
            table[44, 10] = "expression arg-list’";
            table[44, 11] = "expression arg-list’";

            table[44, 20] = "expression arg-list’";
            table[44, 21] = "expression arg-list’";



            table[45, 17] = "";
            table[45, 18] = ", expression arg-list’";


            table[46, 10] = "Signed num";
            table[46, 11] = "Signed num";

            table[46, 20] = "Unsigned num";
            table[46, 21] = "Unsigned num";


            table[47, 20] = "value";
            table[47, 21] = "value";


            table[48, 10] = "pos-num";
            table[48, 11] = "neg-num";

            table[49, 10] = "+ value";


            table[50, 11] = "- value";


            table[51, 20] = "INT_NUM";
            table[51, 21] = "FLOAT_NUM";

            table[52, 15] = "$$$ STR";
            table[52, 16] = "/$ STR $/";

            table[53, 8] = "include ( F_name.txt ) ;";

            table[54, 35] = "STR";

            return table;
        }
    }
}
