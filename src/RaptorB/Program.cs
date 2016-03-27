using System;
using System.IO;

using RaptorB.Args;

namespace RaptorB
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            if (args.Length <= 0)
                ConfigGenerator.DisplayHelp();
            new ConfigInterpreter(new ConfigGenerator(args).Generate()).Interpret();
        }
    }
}
