using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voyager.Helpers
{
    class TCPInterpreter
    {
        public static void interpret(string input)
        {
            if (input.Contains("<init> "))
            {
                input = input.Replace("<init> ", "");
            }
        }
    }
}
