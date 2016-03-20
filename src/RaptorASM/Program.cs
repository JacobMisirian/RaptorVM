using System;
using System.IO;

namespace RaptorASM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //foreach (string str in OpCodes.instructions)
              //  Console.WriteLine(str + "\t" + ((byte)OpCodes.instructions.IndexOf(str) + 1));
            var tokens = new Lexer(File.ReadAllText(args[0])).Scan();
                new Assembler(tokens).Assemble(args[1]);
        }
    }
}