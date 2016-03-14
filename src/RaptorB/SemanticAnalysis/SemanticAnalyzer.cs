using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RaptorB.Parser;

namespace RaptorB.SemanticAnalysis
{
    public class SemanticAnalyzer
    {
        private AstNode code;
        public SemanticAnalyzer(AstNode ast)
        {
            code = ast;
        }
    }
}
