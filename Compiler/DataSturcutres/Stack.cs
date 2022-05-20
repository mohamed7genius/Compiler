using System.Diagnostics;

namespace Compiler.DataSturcutres
{
    public class Stack
    {
        private int Index;
        private string[] Array;
        public Stack(){
            Array = new string[1000];
            Index = 1;
            Array[0] = "#";
        }

        public string Pop()
        {
            if (Index == 0) {
                return "#";
            }

            return Array[--Index];
        }

        public void Push(string element)
        {
            if(Index + 1 >= Array.Length)
            {
                Debug.WriteLine("Out of index!");
                return;
            }
            Array[Index++] = element;
        }

        public string GetLastElement()
        {
            if(Index == 0)
            {
                return "#";
            }

            return Array[Index - 1];
        }
    }
}
