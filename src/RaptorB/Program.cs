using System;

using RaptorB.Lexer;

namespace RaptorB
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            while (true)
                foreach (Token token in new Lexer.Lexer(Console.ReadLine()).Scan())
                    Console.WriteLine(token.ToString());
        }
    }
}
