using System;

namespace RaptorB.Parser
{
    public class NumberNode: AstNode 
    {
        public Int16 Number { get; private set; }
        public NumberNode(Int16 number)
        {
            Number = number;
        }
    }
}

