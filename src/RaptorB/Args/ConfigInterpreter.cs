using System;
using System.IO;

using RaptorB.Lexer;
using RaptorB.Parser;
using RaptorB.SemanticAnalysis;
using RaptorB.CodeGen;

namespace RaptorB.Args
{
    public class ConfigInterpreter
    {
        private RaptorConfig config;
        public ConfigInterpreter(RaptorConfig config)
        {
            this.config = config;
        }

        public void Interpret()
        {
            if (config.InputFile == null || config.InputFile == string.Empty)
            {
                Console.WriteLine("Unspecified input file. See --help for help!");
                Environment.Exit(0);
            }
            if (!File.Exists(config.InputFile))
            {
                Console.WriteLine("Input file {0} does not exist!", config.InputFile);
                Environment.Exit(0);
            }
            if (File.Exists(config.OutputFile))
                File.Delete(config.OutputFile);

            string source = File.ReadAllText(config.InputFile);
            new Preprocessors().ScanPreprocessors(source);
            source = Preprocessors.ScannedSource.ToString();
            var tokens = new Lexer.Lexer(source).Scan();
            var ast = new Parser.Parser(tokens).Parse();
            var symbolTable = new SemanticAnalyzer(ast).Analyze();
            string asm = new CodeGenerator(ast, symbolTable).Generate();

            if (config.GenerateAssembly)
            {
                File.WriteAllText(config.OutputFile, asm);
                Environment.Exit(0);
            }

            RaptorASM.Program.ExecuteFromSource(asm, config.OutputFile);
        }
    }
}

