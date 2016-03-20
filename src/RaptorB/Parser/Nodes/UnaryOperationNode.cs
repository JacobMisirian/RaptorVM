using System;

namespace RaptorB.Parser
{
    public class UnaryOperationNode: AstNode
    {
        public UnaryOperation UnaryOperation { get; private set; }
        public AstNode Body { get; private set; }
        public UnaryOperationNode(UnaryOperation unaryOperation, AstNode body)
        {
            UnaryOperation = unaryOperation;
            Body = body;
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

    public enum UnaryOperation
    {
        Not,
        Reference,
        Dereference
    }
}

