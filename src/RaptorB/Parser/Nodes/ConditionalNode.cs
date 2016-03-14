using System;

using RaptorB.Lexer;

namespace RaptorB.Parser
{
    public class ConditionalNode: AstNode
    {
        public AstNode Predicate { get { return Children[0]; } }
        public AstNode Body { get { return Children[1]; } }
        public AstNode ElseBody { get { return Children[2]; } }

        public ConditionalNode(AstNode predicate, AstNode body)
        {
            Children.Add(predicate);
            Children.Add(body);
        }
        public ConditionalNode(AstNode predicate, AstNode body, AstNode elseBody)
        {
            Children.Add(predicate);
            Children.Add(body);
            Children.Add(elseBody);
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

        public static ConditionalNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "if");
            AstNode predicate = ExpressionNode.Parse(parser);
            AstNode body = StatementNode.Parse(parser);

            if (parser.AcceptToken(TokenType.Identifier, "else"))
                return new ConditionalNode(predicate, body, StatementNode.Parse(parser));
            return new ConditionalNode(predicate, body);
        }
    }
}

