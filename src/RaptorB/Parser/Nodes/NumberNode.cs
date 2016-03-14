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

