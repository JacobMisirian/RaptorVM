using System;
using System.IO;

namespace RaptorASM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ExecuteFromSource(File.ReadAllText(args[0]), args[1]);
        }

        public static void ExecuteFromSource(string source, string outputFile)
        {
            var tokens = new Lexer(source).Scan();
            new Assembler(tokens).Assemble(outputFile);
        }
    }
}