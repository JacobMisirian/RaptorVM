using System;

using RaptorB.Lexer;

namespace RaptorB
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Interpreter.Interpreter interpreter = new Interpreter.Interpreter();

            while (true)
                interpreter.Interpret(new Parser.Parser(new Lexer.Lexer(Console.ReadLine()).Scan()).Parse());
        }
    }
}
