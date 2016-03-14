using System;

namespace RaptorB.Parser
{
    public class BinaryOperationNode: AstNode
    {
        public BinaryOperation BinaryOperation { get; private set; }
        public AstNode Left { get; private set; }
        public AstNode Right { get; private set; }

        public BinaryOperationNode(BinaryOperation binaryOperation, AstNode left, AstNode right)
        {
            BinaryOperation = binaryOperation;
            Left = left;
            Right = right;
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

    public enum BinaryOperation
    {
        Addition,
        Subtraction,
        Multiplication,
        Division,
        Modulus,
        Assignment,
        EqualTo,
        NotEqualTo,
        LessThan,
        LessThanOrEqual,
        GreaterThan,
        GreaterThanOrEqual
    }
}

