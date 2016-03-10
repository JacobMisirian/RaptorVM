using System;
using System.IO;

namespace RaptorASM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var tokens = new Lexer(File.ReadAllText(args[0])).Scan();
            new Assembler(tokens).Assemble(args[1]);
        }
    }
}