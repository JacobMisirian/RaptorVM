using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaptorB.SemanticAnalysis
{
    public class SymbolTable
    {
        public class Scope
        {
            private List<string> symbols = new List<string>();
            public bool Contains(string symbol)
            {
                return symbols.Contains(symbol);
            }

            public void Add(string symbol)
            {
                symbols.Add(symbol);
            }
        }

        public Stack<Scope> Scopes { get; private set; }

        public SymbolTable()
        {
            Scopes = new Stack<Scope>();
        }

        public void EnterScope()
        {
            Scopes.Push(new Scope());
        }

        public bool FindSymbol(string symbol)
        {
            foreach (Scope scope in Scopes)
                if (scope.Contains(symbol))
                    return true;
            return false;
        }

        public void PopScope()
        {
            Scopes.Pop();
        }

        public void AddSymbol(string symbol)
        {
            Scopes.Peek().Add(symbol);
        }
    }
}
