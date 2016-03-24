using System;

using RaptorB.Lexer;

namespace RaptorB.Parser
{
    public class ForNode: AstNode
    {
        public AstNode StartStatement { get { return Children[0]; } }
        public AstNode Predicate { get { return Children[1]; } }
        public AstNode EndStatement { get { return Children[2]; } }
        public AstNode Body { get { return Children[3]; } }
        public ForNode(AstNode startStatement, AstNode predicate, AstNode endStatement, AstNode body)
        {
            Children.Add(startStatement);
            Children.Add(predicate);
            Children.Add(endStatement);
            Children.Add(body);
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

        public static ForNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "for");
            parser.ExpectToken(TokenType.Parentheses, "(");
            AstNode startStatement = StatementNode.Parse(parser);
            parser.AcceptToken(TokenType.Semicolon);
            AstNode predicate = ExpressionNode.Parse(parser);
            parser.AcceptToken(TokenType.Semicolon);
            AstNode endStatement = StatementNode.Parse(parser);
            parser.ExpectToken(TokenType.Parentheses, ")");
            AstNode body = StatementNode.Parse(parser);
            return new ForNode(startStatement, predicate, endStatement, body);
        }
    }
}

