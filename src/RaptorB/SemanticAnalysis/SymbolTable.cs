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
            private Dictionary<string, int> symbols = new Dictionary<string, int>();
            public bool Contains(string symbol)
            {
                return symbols.ContainsKey(symbol);
            }

            public void Add(string symbol, int index)
            {
                symbols.Add(symbol, index);
            }

            public int GetIndex(string symbol)
            {
                return symbols[symbol];
            }
        }

        public Stack<Scope> Scopes { get; private set; }
        private int currentIndex = 0;

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
            if (Scopes.Count == 1)
                currentIndex = 0;
        }

        public void AddSymbol(string symbol)
        {
            Scopes.Peek().Add(symbol, currentIndex++);
        }

        public int GetIndex(string symbol)
        {
            foreach (Scope scope in Scopes)
                if (scope.Contains(symbol))
                    return scope.GetIndex(symbol);
            return -1;
        }
    }
}
