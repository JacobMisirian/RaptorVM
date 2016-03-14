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

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
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

