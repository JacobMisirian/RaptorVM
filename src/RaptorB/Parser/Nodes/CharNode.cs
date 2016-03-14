using System;

namespace RaptorB.Parser
{
    public class CharNode: AstNode 
    {
        public Char Char { get; private set; }
        public CharNode(char c)
        {
            Char = c;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
    }
}

