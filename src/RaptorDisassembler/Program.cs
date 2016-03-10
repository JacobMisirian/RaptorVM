using System;
using System.IO;

namespace RaptorDisassembler
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(new Disassembler(args[0]).Disassemble());
        }
    }
}