using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaptorB.SemanticAnalysis
{
    public class SymbolTable
    {
        public Stack<LocalScope> Symbols { get; private set; }

        public SymbolTable()
        {
            Symbols = new Stack<LocalScope>();
        }

        public void EnterScope()
        {
            Symbols.Push(new LocalScope());
        }

        public bool FindSymbol(string symbol)
        {
            foreach (LocalScope scope in Symbols)
                if (scope.Symbols.Contains(symbol))
                    return true;
            return false;
        }

        public void PopScope()
        {
            Symbols.Pop();
        }
    }
}
