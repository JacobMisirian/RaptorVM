using System;

namespace RaptorB.Parser
{
    public class IdentifierNode: AstNode
    {
        public string Identifier { get; private set; }
        public IdentifierType IdentifierType { get; private set; }
        public IdentifierNode(string identifier, IdentifierType identifierType = IdentifierType.Normal)
        {
            Identifier = identifier;
            IdentifierType = identifierType;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
        public override void VisitChildren(IVisitor visitor)
        {
            foreach (AstNode child in Children)
                child.Visit(visitor);
        }
    }

    public enum IdentifierType
    {
        Normal,
        Reference,
        Dereference
    }
}

