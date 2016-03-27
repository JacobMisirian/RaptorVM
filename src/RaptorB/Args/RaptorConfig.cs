using System;

namespace RaptorB.Args
{
    public class RaptorConfig
    {
        public string InputFile { get; set; }
        public string OutputFile { get { return outputFile; } set { outputFile = value; } }
        private string outputFile = "a.out";
        public bool GenerateAssembly { get { return generateAssembly; } set { generateAssembly = value; } }
        private bool generateAssembly = false;
    }
}

