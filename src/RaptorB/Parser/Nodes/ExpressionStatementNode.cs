using System;

namespace RaptorB.Parser
{
    public class ExpressionStatementNode: AstNode
    {
        public AstNode Body { get { return Children[0]; } }
        public ExpressionStatementNode(AstNode body)
        {
            Children.Add(body);
        }

        public static AstNode Parse(Parser parser)
        {
            AstNode expression = ExpressionNode.Parse(parser);
            if (expression is FunctionCallNode)
                return new ExpressionStatementNode(expression);
            return expression;
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

