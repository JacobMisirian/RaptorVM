using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RaptorB.Lexer
{
    public class Preprocessors
    {
        public static StringBuilder ScannedSource = new StringBuilder();

        public void ScanPreprocessors(string source)
        {
            string[] lines = source.Split('\n');
            foreach (string line in lines)
            {
                string trimLine = line.Trim();
                if (trimLine.StartsWith("#include"))
                    ScanPreprocessors(File.ReadAllText(trimLine.Substring(trimLine.IndexOf(' ') + 1)));
                ScannedSource.AppendLine(line);
            }
        }
    }
}

