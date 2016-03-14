using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaptorB.SemanticAnalysis
{
    public class LocalScope
    {
        public List<string>Symbols { get; private set; }

        public LocalScope()
        {
            Symbols = new List<string>();
        }
    }
}