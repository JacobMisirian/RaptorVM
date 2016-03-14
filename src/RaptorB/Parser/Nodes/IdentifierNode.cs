using System;

namespace RaptorB.Parser
{
    public class IdentifierNode: AstNode
    {
        public string Identifier { get; private set; }
        public IdentifierNode(string identifier)
        {
            Identifier = identifier;
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}

