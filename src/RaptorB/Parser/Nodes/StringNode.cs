using System;

namespace RaptorB.Parser
{
    public class StringNode: AstNode
    {
        public string String { get; private set; }
        public StringNode(string str)
        {
            String = str;
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
}

