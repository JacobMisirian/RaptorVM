using System;

namespace RaptorB.Args
{
    public class ConfigGenerator
    {
        private string[] args;
        private int position;
        public ConfigGenerator(string[] args)
        {
            this.args = args;
        }

        public RaptorConfig Generate()
        {
            RaptorConfig config = new RaptorConfig();
            for (position = 0; position < args.Length; position++)
            {
                switch (args[position].ToLower())
                {
                    case "-h":
                    case "--help":
                        DisplayHelp();
                        break;
                    case "-i":
                    case "--input-file":
                        config.InputFile = expectData("input file");
                        break;
                    case "-o":
                    case "--output-file":
                        config.OutputFile = expectData("output file");
                        break;
                    case "-r":
                    case "--generate-raptor":
                        config.GenerateAssembly = true;
                        break;
                    default:
                        Console.WriteLine("Unknown flag {0}", args[position]);
                        break;
                }
            }
            return config;
        }

        private string expectData(string type)
        {
            if (args[++position].StartsWith("-"))
                throw new Exception(string.Format("Expected {0}, instead got flag {1}!", type, args[position]));
            return args[position];
        }

        public static void DisplayHelp()
        {
            Console.WriteLine("RaptorB.exe [Args]");
            Console.WriteLine("Args:");
            Console.WriteLine("-h --help\tDisplays this help and exits.");
            Console.WriteLine("-i --input-file [Path]\tSpecifies the input file of B source code.");
            Console.WriteLine("-o --output-file [Path]\tSpecifies the output binary file.");
            Console.WriteLine("-r --generate-raptor\tHas the compiler emit RaptorASM source instead of a binary.");
            Environment.Exit(0);
        }
    }
}

